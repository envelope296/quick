import { useNullableState } from "@/hooks";
import { useAppRouting } from "@/hooks/use-app-routing";
import type { GroupResponse } from "@/models/api";
import { useEffect } from "react";
import { useOutletContext } from "react-router-dom";
import * as scheduleServise from "@/services/schedule"
import type { ScheduleResponse } from "@/models/api/schedules";
import { NoSchedules } from "../ui/schedules/NoSchedules";
import { SchedulesView } from "../ui/schedules/SchedulesView";
import { BeatLoader } from "react-spinners";

interface SchedulesPageContext {
    group: GroupResponse;
}

export function SchedulesPage() {
    useAppRouting(() => "/");
    
    const { group } = useOutletContext<SchedulesPageContext>();
    const [schedules, { set: setSchedules }] = useNullableState<ScheduleResponse[]>();

    useEffect(() => {
        async function initialize() {
            const schedulesPage = await scheduleServise.getPage(group.id, 1, 100);
            setSchedules(schedulesPage.items);
        }

        initialize();
    }, []);

    if (schedules == null) {
        return <BeatLoader size={10} margin={3} color="#757575" speedMultiplier={0.8} />
    }

    if (schedules.length == 0) {
        return <NoSchedules group={group} />
    }

    return <SchedulesView group={group} schedules={schedules} />
}