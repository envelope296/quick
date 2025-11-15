import type { AxiosInstance } from "axios";
import { buildAxiosInstanse } from "./builders";
import type { TeacherResponse } from "@/models/api/schedules";
import type { PageResponse } from "@/models/api";

function api(): AxiosInstance {
    const api = buildAxiosInstanse('/v1/teachers', true);
    return api;
}


async function getPage(groupId: string, searchText: string): Promise<PageResponse<TeacherResponse>> {
    const request = {
        groupId,
        searchText,
        page: 1,
        size: 100
    }
    const response = await api().post<PageResponse<TeacherResponse>>('/page', request);
    return response.data;
}