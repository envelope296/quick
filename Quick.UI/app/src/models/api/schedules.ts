export enum ScheduleType {
    Weekly = 0,
    Biweekly = 1
}

export enum WeekType {
    Even = 0,
    Odd = 1
}

export interface ScheduleResponse {
    id: string;
    name: string;
    type: ScheduleType;
}

export interface LessonTypeModel {
    name: string;
    shortName: string | null;
    hexColor: string | null;
}

export interface CreateScheduleRequest {
    groupId: string;
    name: string;
    type: ScheduleType;
    lessonTypes: LessonTypeModel[]
}

export interface TimeSlotResponse {
    id: string; 
    from: string | null; 
    to: string | null;       
    lessons: LessonResponse[];
}

export interface LessonResponse {
    id: string;
    subject: SubjectResponse | null;
    teacher: TeacherResponse | null;
    lessonType: LessonTypeResponse | null;
    cabinetNumber: string | null;
    address: string | null;
}

export interface LessonTypeResponse {
    id: string;
    name: string;
    shortName: string | null;
}

export interface SubjectResponse {
    id: string;
    name: string;
}

export interface TeacherResponse {
    id: string;
    fullName: string;
    email: string | null;
    phoneNumber: string | null;
    messagerUserId: number | null;
}

export enum DayOfWeek {
    Monday = 0,
    Tuesday = 1,
    Wednesday = 2,
    Thursday = 3,
    Friday = 4,
    Saturday = 5,
    Sunday = 6
}

export interface AddLessonRequest {
    dayOfWeek: DayOfWeek;
    weekType: WeekType | null;
    subgroupId: string | null;
    subjectId: string;
    timeSlotId: string;
    teacherId: string | null;
    lessonTypeId: string | null;
    cabinetNumber: string | null;
    address: string | null;
}

export interface AddTimeSlotRequest {
    scheduleId: string;
    name: string;
    from: string;
    to: string;
}

interface GetTimeSlotsPageRequestBase {
    page: number;
    size: number;
    scheduleId: string;
    subgroupId: string | null;
}

export interface GetTimeSlotsPageForDateRequest extends GetTimeSlotsPageRequestBase {
    date: string;
}

export interface GetTimeSlotsPageForDayOfWeekRequest extends GetTimeSlotsPageRequestBase {
    dayOfWeek: DayOfWeek;
    weekType: WeekType | null;
}
