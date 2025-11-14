import styles from "./LessonsView.module.css";
import "./index.css";
import ArrorLeftIcon from "@/assets/arrow-left.svg?react";
import ArrorRightIcon from "@/assets/arrow-right.svg?react";

export function LessonsView() {
  return (
    <div className={styles.container}>
      <div className={styles.header}>
        <button><ArrorLeftIcon /></button>
        <div className={styles.headerLabel}>
          <h1>Понедельник</h1>
          <p>10.11.2025</p>
        </div>
        <button><ArrorRightIcon /></button>
      </div>
      <div className={styles.lessonsContainer}>
        <div className={styles.time}>
          <h1>1 пара</h1>
          <p>8:00 - 9:30</p>
        </div>
        <div className={styles.lesson}>
          <div className={styles.lessonInfo}>
            <h1>Высшая математика</h1>
            <p>Пурхин М. Ю.</p>
            <p className={styles.cabinet}>236 каб</p>
          </div>
          <div className={styles.lessonType}>
            <p>ЛК</p>
          </div>
        </div>
      </div>
    </div>
  );
}