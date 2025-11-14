import { useNullableState } from '@/hooks';
import * as groupService from '@/services/group';
import { useEffect } from "react";
import type { GroupResponse } from '@/models/api';
import { useAppRouting } from '@/hooks/use-app-routing';
import { NoGroups } from '../ui/groups/NoGroups';
import { GroupsView } from '../ui/groups/GroupsView';
import { Loading } from '../common/Loading';

export function HomePage() {
    useAppRouting();

    const [groups, { set: setGroups }] = useNullableState<GroupResponse[]>();

    useEffect(() => {
        async function initialize() {
            const groupsPage = await groupService.getPage(1, 100);
            setGroups(groupsPage.items);
        }

        initialize();
    }, [])

    if (groups == null) {
        return <Loading />
    }

    if (groups.length == 0) {
        return <NoGroups />
    }

    return <GroupsView groups={groups} />
}