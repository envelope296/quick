import { useNavigate } from 'react-router-dom';
import styles from './NoSchedules.module.css';
import ScheduleIcon from '@/assets/schedule.svg?react';

export function NoSchedules() {
    const navigate = useNavigate();

    return (
        <section className={styles["no-schedules-screen"]}>
            <div className={styles.container}>
                <ScheduleIcon className={styles["icon"]} />
                <div className={styles["text-content"]}>
                    <h1 className={styles.title}>Расписания группы</h1>
                    <p className={styles.subtitle}>Пока у группы нет расписний.</p>
                </div>
                <div className={styles["button-group"]}>
                    <button 
                        // onClick={() => navigate('/create-schedule')} 
                        className="btn btn-primary"
                    >
                        Создать вручную
                    </button>
                    <button 
                        className="btn btn-secondary"
                        // onClick={() => navigate('/join-group')}
                    >
                        Считать из файла
                    </button>
                </div>
            </div>
        </section>
    )
}