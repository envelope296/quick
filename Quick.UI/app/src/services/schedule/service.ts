import type { PageResponse } from "@/models/api";
import type { CreateScheduleRequest, GetTimeSlotsPageForDateRequest, GetTimeSlotsPageForDayOfWeekRequest, LessonTypeResponse, ScheduleResponse, TimeSlotResponse } from "@/models/api/schedules";
import { buildAxiosInstanse } from "../builders";
import type { AxiosInstance } from "axios";

function api(): AxiosInstance {
    const api = buildAxiosInstanse('/v1/schedules', true);
    return api;
}

export async function create(request: CreateScheduleRequest): Promise<string> {
    const response = await api().post<string>('/', request);
    return response.data;
}

export async function getPage(groupId: string, page: number, size: number): Promise<PageResponse<ScheduleResponse>> {
    const response = await api().get<PageResponse<ScheduleResponse>>(
        '/page', {
            params: {
                groupId: groupId,
                page: page,
                size: size
            }
        });
    return response.data;
}

export async function get(scheduleId: string): Promise<ScheduleResponse> {
    const response = await api().get<ScheduleResponse>(`/${scheduleId}`);
    return response.data;
}

export async function getTimeSlotsPageForDayOfWeek(request: GetTimeSlotsPageForDayOfWeekRequest): Promise<PageResponse<TimeSlotResponse>> {
    const response = await api().post<PageResponse<TimeSlotResponse>>("time-slots/page/for-day-of-week", request);
    return response.data;
}

export async function getTimeSlotsPageForDate(request: GetTimeSlotsPageForDateRequest): Promise<PageResponse<TimeSlotResponse>> {
    const response = await api().post<PageResponse<TimeSlotResponse>>("time-slots/page/for-date", request);
    return response.data;
}

export async function getLessonTypesPage(scheduleId: string): Promise<PageResponse<LessonTypeResponse>> {
    const response = await api().get<PageResponse<LessonTypeResponse>>(`/${scheduleId}/lesson-types/page`, {
        params: {
            page: 1,
            size: 100
        }
    });
    return response.data;
}