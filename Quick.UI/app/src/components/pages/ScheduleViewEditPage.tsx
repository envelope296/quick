import type { GroupResponse } from "@/models/api";
import { useOutletContext, useParams, useSearchParams } from "react-router-dom";
import styles from "./ScheduleViewEditPage.module.css";
import * as scheduleServise from "@/services/schedule"
import * as subgroupService from "@/services/subgroup";
import { Loading } from "../common/Loading";
import { useNullableState } from "@/hooks";
import { ScheduleType, WeekType, type ScheduleResponse } from "@/models/api/schedules";
import { useEffect, useState } from "react";
import { Switcher } from "../common/Switcher";
import { createEntityOption, type EntityOption } from "@/types/common";
import Select from "react-select";
import { useAppRouting } from "@/hooks/use-app-routing";

interface ScheduleViewEditPageContext {
    group: GroupResponse;
}

interface WeekTypeOption {
    label: string;
    value: WeekType;
}

export function ScheduleViewEditPage() {
    useAppRouting(() => `/groups/${group.id}`);

    const { group } = useOutletContext<ScheduleViewEditPageContext>();
    const [schedule, {set: setSchedule}] = useNullableState<ScheduleResponse>();
    
    const [subgroupsOptions, {set: setSubgroupsOptions}] = useNullableState<EntityOption[]>();
    const weekTypeOptions = [
        {label: "Чётная", value: WeekType.Even}, 
        {label: "Нечётная", value: WeekType.Odd}
    ];

    const [selectedSubgroup, {set: setSelectedSubgroup, clear: clearSelectedSubgroup}] = useNullableState<EntityOption>();
    const [selectedWeekType, {set: setSelectedWeekType, clear: clearSelectedWeekType}] = useNullableState<WeekTypeOption>();
    
    const params = useParams();
    const scheduleId = params.scheduleId;

    const [searchParams] = useSearchParams();
    const defaultIsEdit = searchParams.get("edit") === "true";
    const [isEdit, setIsEdit] = useState(defaultIsEdit);

    useEffect(() => {
        async function initialize() {
            if (scheduleId === undefined) {
                throw new Error("Не передан ID расписания");
            }

            const scheduleResult = await scheduleServise.get(scheduleId);
            setSchedule(scheduleResult);

            const subgroupsPage = await  subgroupService.getPage(group.id, 1, 100);
            setSubgroupsOptions(subgroupsPage.items.map(s => createEntityOption(s.name, s.id)));
        }

        initialize();
    }, []);

    if (schedule === null || subgroupsOptions === null) {
        return <Loading />
    }

    return <section className={styles.screen}>
        {group.isUserOwner &&
            <Switcher 
                defaultState={!defaultIsEdit}
                trueMessage="Просмотр"
                falseMessage="Редактирование"
                onChange={(v) => setIsEdit(!v)}
            />
        }
        <div className={styles.container}>
            <header className={styles.modalHeader}>
                <h1 className={styles.modalTitle}>{schedule.name}</h1>
            </header>
            <div className={styles.selectContainer}>
                <Select
                    isSearchable
                    isClearable
                    options={subgroupsOptions}
                    classNames={{
                        control: () => "input-select"
                    }}
                    placeholder="Подгруппа"
                    onChange={(newValue, _) => {
                        if (newValue == null) {
                            clearSelectedSubgroup();
                        }
                        else {
                            setSelectedSubgroup(newValue);
                        }
                    }}
                />
                {isEdit && schedule.type == ScheduleType.Biweekly &&
                    <Select
                        isClearable
                        options={weekTypeOptions}
                        classNames={{
                            control: () => "input-select"
                        }}
                        placeholder="Неделя"
                        onChange={(newValue, _) => {
                            if (newValue == null) {
                                clearSelectedWeekType();
                            }
                            else {
                                setSelectedWeekType(newValue);
                            }
                        }}
                    />
                }
            </div>
        </div>
    </section>
}