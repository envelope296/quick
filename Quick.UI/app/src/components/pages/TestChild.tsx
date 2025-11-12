import { useAppRouting } from "@/hooks/use-app-routing";

export function TestChild() {
    useAppRouting("/"); 

    return (
        <div>
            <h1>Test child</h1>
        </div>
    )
}