"use client";

import Link from "next/link";
import styles from "./Header.module.css";
import { checkLoggedState, logout } from "@/app/services/authService";
import { useRouter } from "next/navigation";

export const Header = (): JSX.Element => {
  const router = useRouter();
  return (
    <header className={styles.header}>
      <div className={styles.headerContent}>
        <Link href="/">
          <h1 className={styles.projectName}>ENVELOPE</h1>
        </Link>
        <nav className={styles.navigation}>
          <Link href="/tasks" className={styles.navigationLink}>
            Задачи
          </Link>
          <Link href="/courses" className={styles.navigationLink}>
            Курсы
          </Link>
          <Link href="/join" className={styles.navigationLink}>
            Присоединиться
          </Link>
          <Link href="/teaching" className={styles.navigationLink}>
            Преподавание
          </Link>
        </nav>
        {!checkLoggedState() ? (
          <Link href="/auth">
            <button className={styles.enterBtn}>
              ВХОД <span className="material-symbols-outlined">login</span>
            </button>
          </Link>
        ) : (
          <>
            <div className={styles.loginedButtons}>
              <button className={styles.enterBtn}>Профиль</button>
              <button
                className={styles.enterBtn}
                onClick={() => {
                  logout();
                  router.refresh();
                }}
              >
                Выход
              </button>
            </div>
          </>
        )}
      </div>
    </header>
  );
};
