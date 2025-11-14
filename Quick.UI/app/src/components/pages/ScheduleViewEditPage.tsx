import type { GroupResponse, SubgroupResponse } from "@/models/api";
import { useOutletContext, useParams, useSearchParams } from "react-router-dom";
import styles from "./ScheduleViewEditPage.module.css";
import * as scheduleServise from "@/services/schedule"
import * as subgroupService from "@/services/subgroup";
import { Loading } from "../common/Loading";
import { useNullableState } from "@/hooks";
import type { ScheduleResponse } from "@/models/api/schedules";
import { useEffect, useState } from "react";
import { Switcher } from "../common/Switcher";
import { createEntityOption, type EntityOption } from "@/types/common";
import Select from "react-select";
import type { InputActionMeta } from "react-select";

interface ScheduleViewEditPageContext {
    group: GroupResponse;
}

export function ScheduleViewEditPage() {
    const { group } = useOutletContext<ScheduleViewEditPageContext>();
    const [schedule, {set: setSchedule}] = useNullableState<ScheduleResponse>();
    
    const [subgroupsOptions, {set: setSubgroupsOptions}] = useNullableState<EntityOption[]>();
    const [selectedSubgroup, {set: setSelectedSubgroup, clear: clearSelectedSubgroup}] = useNullableState<EntityOption>();
    
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
        <div className={styles.container}>
            <header className={styles.modalHeader}>
                <h1 className={styles.modalTitle}>{schedule.name}</h1>
            </header>
            <Select
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
        </div>
        {group.isUserOwner &&
            <Switcher 
                defaultState={!defaultIsEdit}
                trueMessage="Просмотр"
                falseMessage="Редактирование"
                onChange={setIsEdit}
            />
        }
    </section>
}