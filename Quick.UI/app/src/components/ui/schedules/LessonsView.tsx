import styles from "./LessonsView.module.css";
import ArrorLeftIcon from "@/assets/arrow-left.svg?react";
import ArrorRightIcon from "@/assets/arrow-right.svg?react";
import type { TimeSlotResponse } from "@/models/api/schedules";
import { Lesson } from "./Lesson";
import { useState } from "react";

interface LessonsViewProps {
  date: Date;
  timeSlots: TimeSlotResponse[];
  onDateChanged(date: Date): Promise<void>;
}

function getTimeRange(timeSlot: TimeSlotResponse) {
  if (timeSlot.from === null || timeSlot.to === null) {
    return "";
  }

  return `${timeSlot.from} - ${timeSlot.to}`;
}

export function LessonsView({date, timeSlots, onDateChanged}: LessonsViewProps) {
  const [currentDate, setCurrentDate] = useState(date);

  async function changeDate(diff: number) {
    const newDate = new Date(currentDate);
    newDate.setDate(currentDate.getDate() + diff);
    setCurrentDate(newDate);
    await onDateChanged(newDate);
  }

  function getDayOfWeek() {
    const dayName = new Intl.DateTimeFormat("ru-RU", { weekday: "long" }).format(currentDate);
    const dayNameCapitalized = dayName.charAt(0).toUpperCase() + dayName.slice(1);
    return dayNameCapitalized;
  }

  return (
    <div className={styles.container}>
      <div className={styles.header}>
        <button 
          onClick={() => changeDate(-1)}
        ><ArrorLeftIcon /></button>
        <div className={styles.headerLabel}>
          <h1>{getDayOfWeek()}</h1>
          <p>{Intl.DateTimeFormat("ru-RU").format(currentDate)}</p>
        </div>
        <button
          onClick={() => changeDate(1)}
        ><ArrorRightIcon /></button>
      </div>
      <div className={styles.lessonsContainer}>
        {timeSlots.map((ts, i) =>
        <>
          <div className={styles.time}>
            <h1>{`${i + 1} пара`}</h1>
            <p>{getTimeRange(ts)}</p>
          </div>
          {ts.lessons.map((l) => <Lesson lesson={l} />)}
        </>
        )}
      </div>
    </div>
  );
}