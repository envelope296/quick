import { useState } from 'react';
import { LocalizationProvider, TimeField } from '@mui/x-date-pickers';
import { Dayjs } from 'dayjs';
import styles from './TSAddForm.module.css';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';

interface TSAddFormoPS {
    onCreate(from: string, to: string): Promise<void>
    onCancel(): void
}

export function TSAddForm({ onCreate, onCancel }: TSAddFormoPS) {

    const [from, setFrom] = useState<Dayjs | null>(null);
    const [to, setTo] = useState<Dayjs | null>(null);

    return (
        <>
            <header className={styles.modalHeader}>
                <h1 className={styles.modalTitle}>Добавить пару</h1>
            </header>

            <main className={styles.modalBody}>
                <LocalizationProvider dateAdapter={AdapterDayjs}>
                <div className={styles.formGroup}>
                    <TimeField
                        label="Начало"
                        format="HH:mm"
                        value={from}
                        onChange={(newValue) => setFrom(newValue)}
                    />
                    <TimeField
                        label="Конец"
                        format="HH:mm"
                        value={to}
                        onChange={(newValue) => setTo(newValue)}
                    />
                </div>
                </LocalizationProvider>

                <div className={styles.modalActions}>
                    <button
                        className={`${styles.btn} ${styles.btnCancel}`}
                        onClick={onCancel}
                    >
                        Отмена
                    </button>

                    <button
                        disabled={!from || !to || !from.isBefore(to)}
                        className={`${styles.btn} ${styles.btnCreate}`}
                        onClick={() => onCreate(from!.format("HH:mm:ss"), to!.format("HH:mm:ss"))}
                    >
                        Добавить
                    </button>
                </div>
            </main>
        </>
    );
}