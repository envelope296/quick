import { useNavigate } from 'react-router-dom';
import styles from './NoGroups.module.css';
import GroupIcon from '@/assets/group.svg?react';

export function NoGroups() {
    const navigate = useNavigate();

    return (
        <section className={styles["no-groups-screen"]}>
            <div className={styles.container}>
                <GroupIcon className={styles["icon"]} />
                <div className={styles["text-content"]}>
                    <h1 className={styles.title}>Мои группы</h1>
                    <p className={styles.subtitle}>Пока что вы не состоите ни в одной группе.</p>
                </div>
                <div className={styles["button-group"]}>
                    <button onClick={async () => await navigate("/create-group")} className="btn btn-primary">Создать группу</button>
                    <button className="btn btn-secondary">Присоединиться к группе</button>
                </div>
            </div>
        </section>
    )
}