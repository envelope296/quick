import AsyncSelect from 'react-select/async';
import styles from './LessonAddForm.module.css';
import AsyncCreatableSelect from 'react-select/async-creatable';
import { useNullableState } from '@/hooks';
import * as scheduleService from "@/services/schedule";
import * as subjectService from "@/services/subjectService";
import * as teacherService from "@/services/teacherService";
import { createEntityOption, type EntityOption } from '@/types/common';
import { isNullOrEmpty } from '@/services/helpers/common';

interface LessonAddForm {
    scheduleId: string;
    groupId: string;
    onCancel(): void;
    onCreate(
      subjectId: string | null,
      newSubjectName: string | null,
      teacherId: string | null,
      newTeacherName: string | null,
      lessonTypeId: string | null,
      cabinet: string | null
    ): Promise<void>
}

export function LessonAddForm({
    scheduleId,
    groupId,
    onCancel,
    onCreate
}: LessonAddForm) {
    const [subjectId, {set: setSubjectId, clear: clearSubjectId}] = useNullableState<string>();
    const [newSubjectName, {set: setNewSubjectName, clear: clearNewSubjectName}] = useNullableState<string>();
    
    const [teacherId, {set: setTeacherId, clear: clearTeacherId}] = useNullableState<string>();
    const [newTeacherName, {set: setNewTeacherName, clear: clearNewTeacherName}] = useNullableState<string>();
    
    const [lessonTypeId, {set: setLessonTypeId, clear: clearLessonTypeId}] = useNullableState<string>();
    const [cabinet, {set: setCabinet, clear: clearCabinet}] = useNullableState<string>();

    async function getSubjectOptions(query: string) {
      const page = await subjectService.getPage(groupId, query);
      return page.items.map(s => createEntityOption(s.name, s.id));
    }

    async function getTeacherOptions(query: string) {
      const page = await teacherService.getPage(groupId, query);
      return page.items.map(s => createEntityOption(s.fullName, s.id));
    }

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
              isClearable
              placeholder="Предмет"
              onChange={(option) => {
                if (!option) {
                    clearSubjectId();
                }
                else {
                    setSubjectId(option.id)
                }
              }}
              classNames={{
                control: () => "input-select"
              }}
              loadOptions={getSubjectOptions}
              noOptionsMessage={() => "Введите название предмета"}
              formatCreateLabel={(value) => value}
              onCreateOption={(opt) => {
                setNewSubjectName(opt);
              }}
              loadingMessage={() => "Поиск..."}
            />

            <AsyncCreatableSelect
              isClearable
              placeholder="Преподаватель"
              onChange={(option) => {
                if (!option) {
                    clearTeacherId();
                }
                else {
                    setTeacherId(option.id)
                }
              }}
              classNames={{
                control: () => "input-select"
              }}
              loadOptions={getTeacherOptions}
              noOptionsMessage={() => "Введите имя преподавателя"}
              formatCreateLabel={(value) => value}
              onCreateOption={(opt) => {
                setNewTeacherName(opt);
              }}
              loadingMessage={() => "Поиск..."}
            />

            <AsyncSelect
              isClearable
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
              disabled={subjectId === null && isNullOrEmpty(newSubjectName) }
              className={`${styles.btn} ${styles.btnCreate}`}
              onClick={() => onCreate(
                subjectId,
                newSubjectName,
                teacherId,
                newTeacherName,
                lessonTypeId,
                cabinet
              )}
            >
              Добавить
            </button>
          </div>
        </main>
    </>;
}