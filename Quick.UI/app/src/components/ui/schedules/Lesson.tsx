import type { LessonResponse } from "@/models/api/schedules";
import styles from "./Lesson.module.css";

interface LessonProps {
    lesson: LessonResponse;
}

export function Lesson({lesson}: LessonProps) {
    return <div className={styles.lesson}>
        <div className={styles.lessonInfo}>
            <h1>{lesson.subject?.name ?? '-'}</h1>
            <p>{lesson.teacher?.fullName ?? '-'}</p>
            {lesson.cabinetNumber && <p className={styles.cabinet}>{lesson.cabinetNumber}</p>}
        </div>
        {lesson.lessonType &&
            <div className={styles.lessonType}>
                <p>{lesson.lessonType.name}</p>
            </div>
        }
    </div>
}