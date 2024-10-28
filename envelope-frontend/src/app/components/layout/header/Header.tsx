import Link from 'next/link';
import styles from './Header.module.css';

export const Header = (): JSX.Element => {
    return (
        <header className={styles.header}>
            <div className={styles.headerContent}>
                <Link href='/'>
                    <h1 className={styles.projectName}>
                        ENVELOPE
                    </h1>
                </Link>
                <nav className={styles.navigation}>
                    <Link href='/tasks' className={styles.navigationLink}>Задачи</Link>
                    <Link href='/courses' className={styles.navigationLink}>Курсы</Link>
                    <Link href='/join' className={styles.navigationLink}>Присоединиться</Link>
                </nav>
                <button className={styles.enterBtn}>
                    ВХОД <span className="material-symbols-outlined">login</span>
                </button>
            </div>
        </header>
    )
}