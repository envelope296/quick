import { useAppRouting } from "@/hooks/use-app-routing";
import { Link } from "react-router-dom";

export function TestMain() {
    useAppRouting(); 

    return (
        <div>
            <h1>Test main</h1>
            <Link to="/child">Child</Link>
        </div>
    )
}