import styles from "./Preloader.module.css";

export const Preloader = (): JSX.Element => {
  return (
    <div className={styles.wrapper}>
      <div className={styles.preloader}></div>
    </div>
  );
};
