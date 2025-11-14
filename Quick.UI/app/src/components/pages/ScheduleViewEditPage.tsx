import type { GroupResponse } from "@/models/api";
import { useOutletContext, useParams, useSearchParams } from "react-router-dom";
import styles from "./ScheduleViewEditPage.module.css";
import * as scheduleServise from "@/services/schedule"
import { Loading } from "../common/Loading";
import { useNullableState } from "@/hooks";
import type { ScheduleResponse } from "@/models/api/schedules";
import { useEffect, useState } from "react";
import { Switcher } from "../common/Switcher";

interface ScheduleViewEditPageContext {
    group: GroupResponse;
}

export function ScheduleViewEditPage() {
    const { group } = useOutletContext<ScheduleViewEditPageContext>();
    const [schedule, {set: setSchedule}] = useNullableState<ScheduleResponse>();
    
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

            const result = await scheduleServise.get(scheduleId);
            setSchedule(result);
        }

        initialize();
    }, []);

    if (schedule === null) {
        return <Loading />
    }

    return <section className={styles.screen}>
        {group.isUserOwner &&
            <Switcher 
                defaultState={!defaultIsEdit}
                trueMessage="Просмотр"
                falseMessage="Редактирование"
                onChange={setIsEdit}
            />
        }
        <div className={styles.container}>
            {schedule.name}
        </div>
    </section>
}