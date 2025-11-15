import type { ReactNode } from "react";
import styles from "./Popap.module.css";

interface PopupProps {
    isOpen: boolean;
    children: ReactNode;
}

export function Popup({isOpen, children}: PopupProps) {
    if (!isOpen) return null;

    return (
        <div className={styles.popapOverlay}>
            <div className={styles.popapContent}>
                {children}
            </div>
        </div>
    );
}