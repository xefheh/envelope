import styles from './Footer.module.css';

export const Footer = (): JSX.Element => {
    return (
        <footer>
            <div className={styles.footer}>
                <p className={styles.copyright}>&copy; ENVELOPE TEAM</p>
            </div>
        </footer>
    );
}