import type { AxiosInstance } from "axios";
import { buildAxiosInstanse } from "../builders";
import type { PageResponse, SubgroupResponse } from "@/models/api";

function api(): AxiosInstance {
    const api = buildAxiosInstanse('/v1/subgroups', true);
    return api;
}

export async function getPage(groupId: string, page: number, size: number): Promise<PageResponse<SubgroupResponse>> {
    const response = await api().get<PageResponse<SubgroupResponse>>(
        '/page', { 
            params: {
                groupId,
                page,
                size
            }
        });
    return response.data;
}