import { useNavigate } from 'react-router-dom';
import styles from './NoSchedules.module.css';
import ScheduleIcon from '@/assets/schedule.svg?react';
import type { GroupResponse } from '@/models/api';

interface NoSchedulesParams {
    group: GroupResponse;
}

export function NoSchedules({group}: NoSchedulesParams) {
    const navigate = useNavigate();

    if (group.isUserOwner) {
        return (
            <section className={styles["no-schedules-screen"]}>
                <div className={styles.container}>
                    <ScheduleIcon className={styles["icon"]} />
                    <div className={styles["text-content"]}>
                        <h1 className={styles.title}>Расписания группы</h1>
                        <p className={styles.subtitle}>
                            Пока у группы нет расписний. 
                            Вы можете создать расписание вручную или отправить файл, из которого приложение считает расписание.
                        </p>
                    </div>
                    <div className={styles["button-group"]}>
                        <button 
                            onClick={() => navigate(`/groups/${group.id}/create-schedule`)} 
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
    else {
        return (
            <section className={styles["no-schedules-screen"]}>
                <div className={styles.container}>
                    <ScheduleIcon className={styles["icon"]} />
                    <div className={styles["text-content"]}>
                        <h1 className={styles.title}>Расписания группы</h1>
                        <p className={styles.subtitle}>
                            Пока у группы нет расписний.
                        </p>
                    </div>
                </div>
            </section>
        );
    }
}