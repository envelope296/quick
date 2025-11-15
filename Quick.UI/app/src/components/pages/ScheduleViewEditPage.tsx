import type { GroupResponse } from "@/models/api";
import { useOutletContext, useParams, useSearchParams } from "react-router-dom";
import styles from "./ScheduleViewEditPage.module.css";
import * as scheduleServise from "@/services/schedule"
import * as subgroupService from "@/services/subgroup";
import { Loading } from "../common/Loading";
import { useNullableState } from "@/hooks";
import { DayOfWeek, ScheduleType, WeekType, type ScheduleResponse, type TimeSlotResponse } from "@/models/api/schedules";
import { useEffect, useState } from "react";
import { Switcher } from "../common/Switcher";
import { createEntityOption, type EntityOption } from "@/types/common";
import Select from "react-select";
import { useAppRouting } from "@/hooks/use-app-routing";
import { LessonsView } from "../ui/schedules/LessonsView";
import { LessonsEdit } from "../ui/schedules/LessonsEdit";

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
    
    const [selectedDate, setSelectedDate] = useState(new Date());
    const [selectedDayOfWeek, setSelectedDayOfWeek] = useState(DayOfWeek.Monday);

    const params = useParams();
    const scheduleId = params.scheduleId!;

    const [searchParams] = useSearchParams();
    const defaultIsEdit = searchParams.get("edit") === "true";
    const [isEdit, setIsEdit] = useState(defaultIsEdit);

    const [timeSlots, {set: setTimeSlots, clear: clearTimeSlots}] = useNullableState<TimeSlotResponse[]>()
    
    async function fetchTimeSlots() {
        if (isEdit) {
            await fetchTimeSlotsForDayOfWeek(selectedDayOfWeek);
        }
        else {
            await fetchTimeSlotsForDate(selectedDate);
        }
    }

    async function onDateChanged(date: Date): Promise<void> {
        setSelectedDate(date);
        await fetchTimeSlotsForDate(date);
    }

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

    async function onDayOfWeekChanged(dayOfWeek: DayOfWeek): Promise<void> {
        setSelectedDayOfWeek(dayOfWeek);
        await fetchTimeSlotsForDayOfWeek(dayOfWeek);
    }

    async function fetchTimeSlotsForDayOfWeek(dayOfWeek: DayOfWeek): Promise<void> {
        const request = {
            page: 1,
            size: 100,
            dayOfWeek: dayOfWeek,
            weekType: selectedWeekType === null ? null : selectedWeekType.value,
            scheduleId: scheduleId,
            subgroupId: selectedSubgroup === null ? null : selectedSubgroup.id
        }
        var timeSlotsPage = await scheduleServise.getTimeSlotsPageForDayOfWeek(request);
        setTimeSlots(timeSlotsPage.items);
    }

    useEffect(() => {
        async function initialize() {
            const scheduleResult = await scheduleServise.get(scheduleId);
            setSchedule(scheduleResult);

            const subgroupsPage = await  subgroupService.getPage(group.id, 1, 100);
            setSubgroupsOptions(subgroupsPage.items.map(s => createEntityOption(s.name, s.id)));

            await fetchTimeSlots();
        }

        initialize();
    }, []);

    if (schedule === null || subgroupsOptions === null || timeSlots === null) {
        return <Loading />
    }

    return <section className={styles.screen}>
        {group.isUserOwner &&
            <Switcher 
                defaultState={!defaultIsEdit}
                trueMessage="Просмотр"
                falseMessage="Редактирование"
                onChange={async (v) => {
                    setIsEdit(!v);
                    await fetchTimeSlots();
                }}
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
                    onChange={async (newValue, _) => {
                        if (newValue == null) {
                            clearSelectedSubgroup();
                        }
                        else {
                            setSelectedSubgroup(newValue);
                        }
                        await fetchTimeSlots();
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
                        onChange={async (newValue, _) => {
                            if (newValue == null) {
                                clearSelectedWeekType();
                            }
                            else {
                                setSelectedWeekType(newValue);
                            }
                            await fetchTimeSlots();
                        }}
                    />
                }
            </div>
        </div>
        {isEdit 
            ? <LessonsEdit 
                dayOfWeek={selectedDayOfWeek} 
                timeSlots={timeSlots} 
                onDayOfWeekChanged={onDayOfWeekChanged} 
                onAddClick={() => {}}
            />
            : <LessonsView date={selectedDate} timeSlots={timeSlots} onDateChanged={onDateChanged}
        />}
    </section>
}