export enum ScheduleType {
    Weekly = 0,
    Biweekly = 1
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