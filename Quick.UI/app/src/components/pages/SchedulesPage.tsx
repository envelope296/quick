import { useAppRouting } from "@/hooks/use-app-routing";

export function SchedulesPage() {
    useAppRouting(() => "/");

    return <div>
        Расписания
    </div>
}