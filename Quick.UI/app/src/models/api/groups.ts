export interface GroupResponse {
    id: string;
    name: string;
    description?: string | null;
    ownerId: number | null;
    isPublic: boolean;
}

export interface AddGroupMemberRequest {
  groupId: string;
  userId: number;
  subgroupId?: string | null;
}

export interface CreateGroupRequest {
  name: string;
}