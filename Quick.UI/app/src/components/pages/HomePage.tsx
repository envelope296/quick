import { useNullableState } from '@/hooks';
import * as groupService from '@/services/group';
import { ErrorView, type ErrorViewProps } from '../common/error-view';
import { useEffect } from "react";
import type { GroupResponse } from '@/models/api';
import { Button } from '@maxhub/max-ui';

export function HomePage() {
    const [error, {set: setError}] = useNullableState<ErrorViewProps>();
    const [groups, { set: setGroups }] = useNullableState<GroupResponse[]>();

    useEffect(() => {
        async function initialize() {
            try {
                const groupsPage = await groupService.getPage(1, 100);
                setGroups(groupsPage.items);
            }
            catch {
                setError({
                    title: "Ошибка соединения",
                    description: "Пожалуйста проверьте подключение к интернету и перезапустите мини-приложение"
                });
            }
        }

        initialize();
    }, [])

    if (error != null) {
        return <ErrorView {...error}></ErrorView>
    }

    if (groups == null) {
        return <></>
    }

    if (groups.length == 0) {
        return <div>
            <div>
                <p></p>
                <Button>Создать группу</Button>
                <Button>Присоединиться к группе</Button>
            </div>
        </div>
    }

    return <></>;
}