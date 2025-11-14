import { BeatLoader } from "react-spinners";
import styles from './Loading.module.css';

export function Loading() {
    return <div className={styles.wrapper}>
        <BeatLoader size={20} margin={3} color="#757575" speedMultiplier={0.8} />
    </div>
}