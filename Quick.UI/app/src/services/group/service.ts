import { type AxiosInstance } from "axios";
import { buildAxiosInstanse } from "../builders";
import type { AddGroupMemberRequest, CreateGroupRequest, GroupResponse, PageResponse } from "@/models/api";

function api(): AxiosInstance {
    const api = buildAxiosInstanse('/v1/groups', true);
    return api;
}

export async function get(id: string) {
    const response = await api().get<GroupResponse>(`/${id}`);
    return response.data;
}

export async function getPage(page: number, size: number): Promise<PageResponse<GroupResponse>> {
    const response = await api().get<PageResponse<GroupResponse>>(
        '/page', {
            params: {
                page: page,
                size: size
            }
        });
    return response.data;
}

export async function createGroup(request: CreateGroupRequest): Promise<string> {
    const response = await api().post<string>('/', request);
    return response.data;
}

export async function addMember(request: AddGroupMemberRequest): Promise<void> {
    await api().post('/members', request);
}

export async function deleteMember(groupId: string, userId: number): Promise<void> {
    await api().delete(`/${groupId}/members/${userId}`);
}