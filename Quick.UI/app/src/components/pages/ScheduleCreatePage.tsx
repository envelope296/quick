import type { GroupResponse } from "@/models/api";
import { useOutletContext } from "react-router-dom";
import styles from "./ScheduleCreatePage.module.css";
import * as scheduleService from "@/services/schedule";
import { useBoolean, useNullableState } from "@/hooks";
import { isNullOrEmpty } from "@/services/helpers/common";
import { ScheduleType } from "@/models/api/schedules";
import { useAppRouting } from "@/hooks/use-app-routing";
import { Switch } from "@maxhub/max-ui";
import { useState } from "react";

interface ScheduleCreatePagePageContext {
    group: GroupResponse;
}

export function ScheduleCreatePage() {
    const toPrevios = useAppRouting(() => `/groups/${group.id}/schedules`)

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

    function onIsBiweeklyInputChanged(e: React.ChangeEvent<HTMLInputElement>) {
        setIsBiweekly(e.target.checked);
    }

    async function onCreatePressed() {
        if(isNullOrEmpty(scheduleName)) {
            return;
        }

        const request = {
            groupId: group.id,
            name: scheduleName,
            type: isBiweekly ? ScheduleType.Biweekly : ScheduleType.Weekly,
            lessonTypes: []
        }
        const scheduleId = await scheduleService.create(request);
    }

    return (
        <section className={styles.createGroupModal}>
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
                        <Switch onChange={onIsBiweeklyInputChanged} />
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