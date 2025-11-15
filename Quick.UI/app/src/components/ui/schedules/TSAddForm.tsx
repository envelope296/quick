import { TimeField } from '@mui/x-date-pickers';
import styles from './TSAddForm.module.css';

interface TSAddFormoPS {
    onCreate(): Promise<void>
    onCancel(): void
}

export function TSAddForm({onCreate, onCancel}: TSAddFormoPS) {
    
    
    return <>
        <header className={styles.modalHeader}>
            <h1 className={styles.modalTitle}>Добавить пару</h1>
        </header>
        <main className={styles.modalBody}>
          <div className={styles.formGroup}>
            <TimeField
                label="Начало"
                format="HH:mm"
                onChange={}
            />
            <TimeField
                label="Конец"
                format="HH:mm"
                onChange={}
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
              disabled={false}
              className={`${styles.btn} ${styles.btnCreate}`}
              onClick={() => onCreate()}
            >
              Добавить
            </button>
          </div>
        </main>
    </>;
}