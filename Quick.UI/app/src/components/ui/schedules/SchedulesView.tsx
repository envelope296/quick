
import type { GroupResponse } from '@/models/api';
import ScheduleIcon from '@/assets/schedule.svg?react';
import styles from './SchedulesView.module.css';
import { useNavigate } from 'react-router-dom';
import type { ScheduleResponse } from '@/models/api/schedules';

interface SchedulesViewProps {
    group: GroupResponse;
    schedules: ScheduleResponse[];
}

export function SchedulesView({group, schedules}: SchedulesViewProps) {
    const navigate = useNavigate();

    return (
        <section className={styles["schedules-screen"]}>
            <div className={styles.container}>
                <ScheduleIcon className={styles["icon"]} />
                <div className={styles["text-content"]}>
                    <h1 className={styles.title}>Расписания группы</h1>
                </div>
                {group.isUserOwner &&
                    <div className={styles.modalActions}>
                        <button 
                            className={`${styles.btn} ${styles.btnAction}`}
                            onClick={() => navigate(`/groups/${group.id}/create-schedule`)}
                        >
                            Добавить
                        </button>
                    </div>
                }
            </div>
            <div className={`${styles.container} ${styles.groupsContainer}`}>
                {schedules.map(s => 
                    <button 
                        className={styles.groupItem} 
                        onClick={() => navigate(`/groups/${group.id}/schedules/view/${s.id}`)}
                    >
                        {s.name}
                    </button>
                )}
            </div>
        </section>
    );
}