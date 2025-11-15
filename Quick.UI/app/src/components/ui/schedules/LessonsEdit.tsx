import styles from "./LessonsEdit.module.css";
import ArrorLeftIcon from "@/assets/arrow-left.svg?react";
import ArrorRightIcon from "@/assets/arrow-right.svg?react";
import PlusIcon from "@/assets/plus.svg?react";
import { DayOfWeek, type TimeSlotResponse } from "@/models/api/schedules";
import { Lesson } from "./Lesson";

interface LessonsEditProps {
  dayOfWeek: DayOfWeek;
  timeSlots: TimeSlotResponse[];
  onAddClick(timeSlotId: string): void;
  onAddTsClick(): void;
  onDayOfWeekChanged(dayOfWeek: DayOfWeek): Promise<void>;
}

function getTimeRange(timeSlot: TimeSlotResponse) {
  if (timeSlot.from === null || timeSlot.to === null) {
    return "";
  }

  return `${timeSlot.from.slice(0, 5)} - ${timeSlot.to.slice(0, 5)}`;
}

export function LessonsEdit({dayOfWeek, timeSlots, onDayOfWeekChanged, onAddClick, onAddTsClick}: LessonsEditProps) {
  async function changeDayOfWeek(diff: number) {
    const result = ((dayOfWeek + diff) % 7 + 7) % 7;
    onDayOfWeekChanged(result as DayOfWeek);
  }

  function getDayOfWeek(): string {
    switch(dayOfWeek) {
        case DayOfWeek.Monday:
            return "Понедельник";
        case DayOfWeek.Tuesday:
            return "Вторник";
        case DayOfWeek.Wednesday:
            return "Среда";    
        case DayOfWeek.Thursday:
            return "Четверг";    
        case DayOfWeek.Friday:
            return "Пятница";  
        case DayOfWeek.Saturday:
            return "Суббота";  
        case DayOfWeek.Sunday:
            return "Воскресенье";  
    }
  }

  return (
    <div className={styles.container}>
      <div className={styles.header}>
        <button 
          onClick={() => changeDayOfWeek(-1)}
        ><ArrorLeftIcon /></button>
        <div className={styles.headerLabel}>
          <h1>{getDayOfWeek()}</h1>
          <p></p>
        </div>
        <button
          onClick={() => changeDayOfWeek(1)}
        ><ArrorRightIcon /></button>
      </div>
      <div className={styles.lessonsContainer}>
        {timeSlots.map((ts, i) =>
        <>
          <div className={styles.time}>
            <h1>{`${i + 1} пара`}</h1>
            <p>{getTimeRange(ts)}</p>
          </div>
          <div>
            {ts.lessons.map((l) => <Lesson lesson={l} />)}
            <div className={styles.lessonAddWrapper}>
              <button onClick={() => onAddClick(ts.id)} className={styles.lessonAdd}><PlusIcon /></button>
            </div>
          </div>
        </>
        )}
      </div>
      <button onClick={onAddTsClick} className={styles.tsButton}>
        Добавить пару
      </button>
    </div>
  );
}