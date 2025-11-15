import type { AxiosInstance } from "axios";
import { buildAxiosInstanse } from "./builders";
import type { PageResponse } from "@/models/api";
import type { SubjectResponse } from "@/models/api/schedules";

function api(): AxiosInstance {
    const api = buildAxiosInstanse('/v1/subjects', true);
    return api;
}

export async function getPage(groupId: string, searchText: string): Promise<PageResponse<SubjectResponse>> {
    const request = {
        groupId,
        searchText,
        page: 1,
        size: 100
    }
    const response = await api().post<PageResponse<SubjectResponse>>('/page', request);
    return response.data;
}

export async function create(groupId: string, name: string): Promise<string> {
    const request = {
        groupId,
        name
    }
    const response = await api().post<string>('/', request);
    return response.data;
}