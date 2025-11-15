import type { GroupResponse } from "@/models/api";
import { useOutletContext, useParams, useSearchParams } from "react-router-dom";
import styles from "./ScheduleViewEditPage.module.css";
import * as scheduleServise from "@/services/schedule"
import * as subgroupService from "@/services/subgroup";
import { Loading } from "../common/Loading";
import { useNullableState } from "@/hooks";
import { ScheduleType, WeekType, type ScheduleResponse, type TimeSlotResponse } from "@/models/api/schedules";
import { useEffect, useState } from "react";
import { Switcher } from "../common/Switcher";
import { createEntityOption, type EntityOption } from "@/types/common";
import Select from "react-select";
import { useAppRouting } from "@/hooks/use-app-routing";
import { LessonsView } from "../ui/schedules/LessonsView";

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
    const scheduleId = params.scheduleId!;

    const [searchParams] = useSearchParams();
    const defaultIsEdit = searchParams.get("edit") === "true";
    const [isEdit, setIsEdit] = useState(defaultIsEdit);

    const [timeSlots, {set: setTimeSlots, clear: clearTimeSlots}] = useNullableState<TimeSlotResponse[]>()

    async function fetchTimeSlotsForDate(date: Date): Promise<void> {
        const request = {
            page: 1,
            size: 100,
            date: date.toISOString(),
            scheduleId: scheduleId,
            subgroupId: selectedSubgroup === null ? null : selectedSubgroup.id
        }
        var timeSlotsPage = await scheduleServise.getTimeSlotsPageForDate(request);
        setTimeSlots(timeSlotsPage.items);
    }

    async function fetchTimeSlotsForDayOfWeek(): Promise<void> {
        
    }

    useEffect(() => {
        async function initialize() {
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
        {isEdit 
            ? <p>edit</p> 
            : <LessonsView date={new Date()} timeSlots={timeSlots || []} onDateChanged={fetchTimeSlotsForDate}
        />}
    </section>
}