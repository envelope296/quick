
import type { GroupResponse } from '@/models/api';
import GroupIcon from '@/assets/group.svg?react';
import styles from './GroupsView.module.css';
import { useNavigate } from 'react-router-dom';

interface GroupsViewProps {
    groups: GroupResponse[]
}

export function GroupsView(props: GroupsViewProps) {
    const navigate = useNavigate();

    return (
        <section className={styles["groups-screen"]}>
            <div className={styles.container}>
                <GroupIcon className={styles["icon"]} />
                <div className={styles["text-content"]}>
                    <h1 className={styles.title}>Мои группы</h1>
                </div>
                <div className={styles.modalActions}>
                    <button 
                        className={`${styles.btn} ${styles.btnAction}`}
                        onClick={() => navigate('/create-group')}
                    >
                        Добавить
                    </button>
                    <button
                        className={`${styles.btn} ${styles.btnAction}`}
                    >
                        Присоединиться
                    </button>
                </div>
            </div>
            <div className={`${styles.container} ${styles.groupsContainer}`}>
                {props.groups.map(g => 
                    <button className={styles.groupItem}>
                        {g.name}
                    </button>
                )}
            </div>
        </section>
    );
}