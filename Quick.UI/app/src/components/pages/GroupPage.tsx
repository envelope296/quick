import { useEffect } from "react";
import { Outlet, useParams } from "react-router-dom";
import * as groupService from "@/services/group"
import { useNullableState } from "@/hooks";
import type { GroupResponse } from "@/models/api";
import styles from "./GroupPage.module.css";

export function GroupPage() {
    const [group, { set: setGroup }] = useNullableState<GroupResponse>();

    const params = useParams();
    const groupId = params.id;
    
    useEffect(() => {
        async function initialize() {
            if (groupId === undefined) {
                throw new Error("Не передан id группы");
            }

            const group = await groupService.get(groupId);
            setGroup(group);
        }

        initialize();
    }, []);

    if (group === null) {
        return <></>
    }

    return <div>
        <header className={styles.header}>
            <h1>{group.name}</h1>
        </header>
        <Outlet context={{group}} />
    </div>
}