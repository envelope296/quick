import type { FC } from "react";
import GroupIcon from '@/assets/group.svg?react';

export interface ErrorViewProps {
    title: string;
    description: string | null
}

export const ErrorView: FC<ErrorViewProps> = props => {
    return <div>
        <GroupIcon />
        <div>
            <h1>{props.title}</h1>
            <p>{props.description}</p>
        </div>
    </div>
}