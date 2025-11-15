import AsyncSelect from 'react-select/async';
import styles from './LessonAddForm.module.css';
import AsyncCreatableSelect from 'react-select/async-creatable';
import { useState } from 'react';
import { useBoolean, useNullableState } from '@/hooks';
import * as scheduleService from "@/services/schedule";
import { createEntityOption, type EntityOption } from '@/types/common';
import { isNullOrEmpty } from '@/services/helpers/common';

interface LessonAddForm {
    scheduleId: string;
    groupId: string;
    onCancel(): void;
    onCreate(): Promise<void>
}

export function LessonAddForm({
    scheduleId,
    groupId,
    onCancel,
    onCreate
}: LessonAddForm) {
    const [disabled, {setTrue: disable, setFalse: enable}] = useBoolean(true);

    const [lessonType, {set: setLessonTypeId, clear: clearLessonTypeId}] = useNullableState<string>();
    const [cabinet, {set: setCabinet, clear: clearCabinet}] = useNullableState<string>();

    async function getLessonTypeOptions(): Promise<EntityOption[]> {
        const page = await scheduleService.getLessonTypesPage(scheduleId);
        return page.items.map(lt => createEntityOption(lt.name, lt.id));
    }
    
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
              isSearchable={false}
              placeholder="Тип занятия"
              onChange={(option) => {
                if (!option) {
                    clearLessonTypeId();
                }
                else {
                    setLessonTypeId(option.id)
                }
              }}
              classNames={{
                control: () => "input-select"
              }}
              loadOptions={getLessonTypeOptions}
              noOptionsMessage={() => "Тип занятия не найден"}
              loadingMessage={() => "Поиск..."}
            />

            <input
              type="text"
              className="input-field"
              placeholder="Кабинет"
              onChange={(e) =>{
                const v = e.target.value;
                if (isNullOrEmpty(v)) {
                    clearCabinet();
                }
                else {
                    setCabinet(v);
                }
              }}
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
              disabled={disabled}
              className={`${styles.btn} ${styles.btnCreate}`}
              onClick={onCreate}
            >
              Добавить
            </button>
          </div>
        </main>
    </>;
}