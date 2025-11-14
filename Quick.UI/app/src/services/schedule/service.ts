import type { PageResponse } from "@/models/api";
import type { CreateScheduleRequest, ScheduleResponse } from "@/models/api/schedules";
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