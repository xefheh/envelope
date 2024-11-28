import Link from "next/link";
import styles from "./CardComponent.module.css";
import { CardComponentProps } from "./CardComponent.props";

export const CardComponent = ({
  title,
  description,
  linkTo,
}: CardComponentProps): JSX.Element => {
  return (
    <>
      <div className={styles.card}>
        <h1 className={styles.header}>{title}</h1>
        <p className={styles.description}>{description}</p>
        <Link className={styles.linkTo} href={linkTo}>
          Создать {title.toLowerCase()}
        </Link>
      </div>
    </>
  );
};
