import { useState } from 'react';
import { TimeField } from '@mui/x-date-pickers';
import dayjs, { Dayjs } from 'dayjs';
import styles from './TSAddForm.module.css';

interface TSAddFormoPS {
    onCreate(from: Dayjs | null, to: Dayjs | null): Promise<void>
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

                <div className={styles.modalActions}>
                    <button
                        className={`${styles.btn} ${styles.btnCancel}`}
                        onClick={onCancel}
                    >
                        Отмена
                    </button>

                    <button
                        disabled={!from || !to}
                        className={`${styles.btn} ${styles.btnCreate}`}
                        onClick={() => onCreate(from, to)}
                    >
                        Добавить
                    </button>
                </div>
            </main>
        </>
    );
}