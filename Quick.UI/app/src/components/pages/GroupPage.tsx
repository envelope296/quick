import { useEffect } from "react";
import { Outlet, useParams } from "react-router-dom";
import * as groupService from "@/services/group"

export function GroupPage() {
    const params = useParams();
    const groupId = params.id;

    useEffect(() => {
        
      }, []);

    <div>
        <header>
            <h1></h1>
        </header>
        <Outlet />
    </div>
}