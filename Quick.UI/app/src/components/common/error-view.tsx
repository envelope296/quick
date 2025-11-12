import type { FC } from "react";

export interface ErrorViewProps {
    title: string;
    description: string | null
}

export const ErrorView: FC<ErrorViewProps> = props => {
    return <div>
        <div>
            <h1>{props.title}</h1>
            <p>{props.description}</p>
        </div>
    </div>
}