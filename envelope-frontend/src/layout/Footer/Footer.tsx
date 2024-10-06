import styles from './Footer.module.scss';

export const Footer = (): JSX.Element => {
    return <footer className={styles.footer}>
        <p className={styles.footer__copyright}>&copy; ENVELOPE-TEAM UTMN</p>
    </footer>
}