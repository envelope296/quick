export interface GroupResponse {
    id: string;
    name: string;
    description?: string | null;
    ownerId: number | null;
    isUserOwner: boolean;
    isPublic: boolean;
}

export interface SearchGroupsRequest {
  query: string,
  university: string,
  page: number,
  size: number
}

export interface JoinGroupResponse {
  groupId: string,
  isSuccess: boolean,
  errorMessage: string | null
}

export interface AddGroupMemberRequest {
  groupId: string;
  userId: number;
  subgroupId?: string | null;
}

export interface CreateGroupRequest {
  name: string;
  university: string | null;
  subgroups: string[]
}

export interface SubgroupResponse {
  id: string;
  name: string;
}