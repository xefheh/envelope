import { LayoutProps } from "./Layout.props";
import styles from './Layout.module.css';
import { Header } from "./header/Header";
import { Footer } from "./footer/Footer";

export const Layout: React.FC<LayoutProps> = ({children}: LayoutProps): JSX.Element => {
    return (
        <div className={styles.mainContainer}>
            <Header />
            <main className={styles.mainContent}>{children}</main>
            <Footer />
        </div>
    );
}