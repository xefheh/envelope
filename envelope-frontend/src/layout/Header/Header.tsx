import { Link } from 'react-router-dom';
import styles from './Header.module.scss';
import { Button } from '../../components/Button/Button';

export const Header = (): JSX.Element => {
    return <header className={styles.header}>
            <Link to='/' className={styles.header__name}>ENVELOPE</Link>
            <nav className={styles.navigation}>
                <ul><Link className={styles.navigation__item} to='test1'>Ссылка 1</Link></ul>
                <ul><Link className={styles.navigation__item} to='test2'>Ссылка 2</Link></ul>
                <ul><Link className={styles.navigation__item} to='test3'>Ссылка 3</Link></ul>
                <ul><Link className={styles.navigation__item} to='test4'>Ссылка 4</Link></ul>
            </nav>
            <div className={styles.header__regBtn}>
                <Button>ВХОД <span className="material-symbols-outlined">login</span></Button>
            </div>
        </header>
}