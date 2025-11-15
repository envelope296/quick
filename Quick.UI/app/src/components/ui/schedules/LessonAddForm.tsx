import AsyncSelect from 'react-select/async';
import styles from './LessonAddForm.module.css';
import AsyncCreatableSelect from 'react-select/async-creatable';

interface LessonAddForm {
    onCancel(): void;
    onCreate(): Promise<void>
}

export function LessonAddForm({
    onCancel,
    onCreate
}: LessonAddForm) {
    return <>
        <header className={styles.modalHeader}>
            <h1 className={styles.modalTitle}>Добавление занятия</h1>
        </header>
        <main className={styles.modalBody}>
          <div className={styles.formGroup}>
            <AsyncCreatableSelect
              placeholder="Предмет"
              onChange={() => {}}
              classNames={{
                control: () => "input-select"
              }}
              noOptionsMessage={() => "Введите название предмета"}
              formatCreateLabel={(value) => value}
              onCreateOption={() => {}}
              loadingMessage={() => "Поиск..."}
            />

            <AsyncCreatableSelect
              placeholder="Преподаватель"
              onChange={() => {}}
              classNames={{
                control: () => "input-select"
              }}
              noOptionsMessage={() => "Введите имя преподавателя"}
              formatCreateLabel={(value) => value}
              onCreateOption={() => {}}
              loadingMessage={() => "Поиск..."}
            />

            <AsyncSelect
              placeholder="Тип занятия"
              onChange={() => {}}
              classNames={{
                control: () => "input-select"
              }}
              noOptionsMessage={() => "Тип занятия не найден"}
              loadingMessage={() => "Поиск..."}
            />

            <input
              type="text"
              className="input-field"
              placeholder="Кабинет"
              onChange={() =>{}}
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
              disabled={true}
              className={`${styles.btn} ${styles.btnCreate}`}
              onClick={onCreate}
            >
              Добавить
            </button>
          </div>
        </main>
    </>;
}