import styles from "./Switcher.module.css";
import { useState } from "react";

interface SwitcherOptions {
    defaultState: boolean,
    trueMessage: string,
    falseMessage: string,
    onChange(callback: boolean): Promise<void>;
}

export function Switcher({defaultState, trueMessage, falseMessage, onChange}: SwitcherOptions) {
    const [state, setState] = useState(defaultState);

    async function changeState(newState: boolean) {
        setState(newState);
        await onChange(newState);
    }

    return (
        <div className={styles.switcher}>
            <button 
                className={styles.switcherBtn}
                disabled={state}
                onClick={() => changeState(true)}
            >
                {trueMessage}
            </button>
            <button 
                className={styles.switcherBtn}
                disabled={!state}
                onClick={() => changeState(false)}
            >
                {falseMessage}
            </button>
        </div>
    );
}