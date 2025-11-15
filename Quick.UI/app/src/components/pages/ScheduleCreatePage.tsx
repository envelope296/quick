import type { GroupResponse } from "@/models/api";
import { createSearchParams, useNavigate, useOutletContext } from "react-router-dom";
import styles from "./ScheduleCreatePage.module.css";
import * as scheduleService from "@/services/schedule";
import * as webAppContext from "@/services/app-context";
import { useBoolean, useNullableState } from "@/hooks";
import { isNullOrEmpty } from "@/services/helpers/common";
import { ScheduleType } from "@/models/api/schedules";
import { useAppRouting } from "@/hooks/use-app-routing";
import { useState } from "react";
import { Switcher } from "../common/Switcher";

interface ScheduleCreatePagePageContext {
    group: GroupResponse;
}

export function ScheduleCreatePage() {
    const toPrevios = useAppRouting(() => `/groups/${group.id}/schedules`);
    const navigate = useNavigate();

    const { group } = useOutletContext<ScheduleCreatePagePageContext>();

    const [isCreateButtonDisabled, {setTrue: disableCreateButton, setFalse: enableCreateButton}] = useBoolean(true);

    const [scheduleName, {set: setScheduleName, clear: clearScheduleName}] = useNullableState<string>();
    const [isBiweekly, setIsBiweekly] = useState(false);

    function onScheduleNameInputChanged(e: React.ChangeEvent<HTMLInputElement>) {
        const newValue = e.target.value;
        
        if(isNullOrEmpty(newValue)) {
            clearScheduleName();
            disableCreateButton();
        }
        else {
            setScheduleName(newValue);
            enableCreateButton();
        }
    }

    async function onCreatePressed() {
        if(isNullOrEmpty(scheduleName)) {
            return;
        }

        const request = {
            groupId: group.id,
            name: scheduleName,
            type: isBiweekly ? ScheduleType.Biweekly : ScheduleType.Weekly,
            lessonTypes: [
                {name: "Лекция", shortName: "ЛК", hexColor: null},
                {name: "Семинар", shortName: "СМ", hexColor: null},
                {name: "Практическое занятие", shortName: "ПЗ", hexColor: null},
                {name: "Лабораторная работа", shortName: "ЛР", hexColor: null},
                {name: "Консультации", shortName: "КН", hexColor: null},
            ]
        }
        const scheduleId = await scheduleService.create(request);

        const params = createSearchParams({ edit: "true" });
        await navigate(`/groups/${group.id}/schedules/view/${scheduleId}?${params}`);
    }

    return (
        <section className={styles.createScheduleModal}>
            <div className={styles.modalCard}>
                <header className={styles.modalHeader}>
                    <h1 className={styles.modalTitle}>Создание расписания</h1>
                </header>
                <main className={styles.modalBody}>
                    <div className={styles.formGroup}>
                        <input
                            type="text"
                            className="input-field"
                            placeholder="Название"
                            onChange={onScheduleNameInputChanged}
                        />
                        <div className={styles.switcherBox}>
                            <p className={styles.switcherLabel}>Раздление на чётные/нёчетные недели</p>
                            <Switcher
                                defaultState={false}
                                trueMessage="Есть"
                                falseMessage="Нет"
                                onChange={async (value) => {
                                    const webApp = webAppContext.getWebApp();
                                    webApp.HapticFeedback.selectionChanged(false);
                                    setIsBiweekly(value);
                                }}
                            />
                        </div>
                    </div>

                    <div className={styles.modalActions}>
                        <button 
                            className={`${styles.btn} ${styles.btnCancel}`}
                            onClick={toPrevios}  
                        >
                            Отмена
                        </button>
                        <button
                            disabled={isCreateButtonDisabled}
                            className={`${styles.btn} ${styles.btnCreate}`}
                            onClick={onCreatePressed}
                        >
                            Создать
                        </button>
                    </div>
                </main>
            </div>
        </section>
    );
}