import type { ReactNode } from "react";
import styles from "./Popap.module.css";

interface PopupProps {
    isOpen: boolean;
    children: ReactNode;
}

export function Popup({isOpen, children}: PopupProps) {
    if (!isOpen) return null;

    return (
        <div className={styles.popupOverlay}>
            <div className={styles.popupContent}>
                {children}
            </div>
        </div>
    );
}