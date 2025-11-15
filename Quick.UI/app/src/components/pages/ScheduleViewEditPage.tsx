import type { GroupResponse } from "@/models/api";
import { useOutletContext, useParams, useSearchParams } from "react-router-dom";
import styles from "./ScheduleViewEditPage.module.css";
import * as scheduleServise from "@/services/schedule"
import * as subgroupService from "@/services/subgroup";
import * as subjectService from "@/services/subjectService";
import * as teacherService from "@/services/teacherService";
import { Loading } from "../common/Loading";
import { useBoolean, useNullableState } from "@/hooks";
import { DayOfWeek, ScheduleType, WeekType, type ScheduleResponse, type TimeSlotResponse } from "@/models/api/schedules";
import { useEffect, useState } from "react";
import { Switcher } from "../common/Switcher";
import { createEntityOption, type EntityOption } from "@/types/common";
import Select from "react-select";
import { useAppRouting } from "@/hooks/use-app-routing";
import { LessonsView } from "../ui/schedules/LessonsView";
import { LessonsEdit } from "../ui/schedules/LessonsEdit";
import { Popup } from "../common/Popup";
import { LessonAddForm } from "../ui/schedules/LessonAddForm";
import { isNullOrEmpty } from "@/services/helpers/common";
import { TSAddForm } from "../ui/schedules/TSAddForm";

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

    const [isAddLessonModalOpen, {setTrue: openAddLessonModal, setFalse: closeAddLessonModal}] = useBoolean();
    const [tsAdd, {setTrue: opentsAdd, setFalse: closetsadd}] = useBoolean();

    const [selectedSubgroup, {set: setSelectedSubgroup, clear: clearSelectedSubgroup}] = useNullableState<EntityOption>();
    const [selectedWeekType, {set: setSelectedWeekType, clear: clearSelectedWeekType}] = useNullableState<WeekTypeOption>();
    const [selectedTimeSlotId, {set: setSelectedTimeSlotId, clear: clearSelectedTimeSlotId}] = useNullableState<string>();

    const [selectedDate, setSelectedDate] = useState(new Date());
    const [selectedDayOfWeek, setSelectedDayOfWeek] = useState(DayOfWeek.Monday);

    const params = useParams();
    const scheduleId = params.scheduleId!;

    const [searchParams] = useSearchParams();
    const defaultIsEdit = searchParams.get("edit") === "true";
    const [isEdit, setIsEdit] = useState(defaultIsEdit);

    const [timeSlots, {set: setTimeSlots, clear: clearTimeSlots}] = useNullableState<TimeSlotResponse[]>()
    
    function onLessonAddClick(timeSlotId: string) {
        setSelectedTimeSlotId(timeSlotId);
        openAddLessonModal();
    }

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

    async function createLesson(
        subjectId: string | null,
        newSubjectName: string | null,
        teacherId: string | null,
        newTeacherName: string | null,
        lessonTypeId: string | null,
        cabinet: string | null
    ) {
        closeAddLessonModal();

        if (subjectId === null && !isNullOrEmpty(newSubjectName)) {
            subjectId = await subjectService.create(group.id, newSubjectName);
        }
        if (teacherId === null && !isNullOrEmpty(newTeacherName)) {
            teacherId = await teacherService.create(group.id, newTeacherName);
        }
        const request = {
            subjectId: subjectId!,
            teacherId,
            lessonTypeId,
            dayOfWeek: selectedDayOfWeek,
            weekType: selectedWeekType === null ? null : selectedWeekType.value,
            subgroupId: selectedSubgroup == null ? null : selectedSubgroup.id,
            timeSlotId: selectedTimeSlotId!,
            cabinetNumber: cabinet,
            address: null
        }
        await scheduleServise.addLesson(request);
        await fetchTimeSlots();        
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

    return <>
        <Popup isOpen={isAddLessonModalOpen}>
            <LessonAddForm 
                onCancel={closeAddLessonModal}
                onCreate={createLesson}
                scheduleId={scheduleId}
                groupId={group.id}
            />
        </Popup>

        <Popup isOpen={tsAdd}>
            <TSAddForm 
                onCancel={closeAddLessonModal}
                onCreate={async () => {}}
            />
        </Popup>
    <section className={styles.screen}>


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
                onAddClick={onLessonAddClick}
            />
            : <LessonsView date={selectedDate} timeSlots={timeSlots} onDateChanged={onDateChanged}
        />}
    </section>
    </>
}