import { CardComponent } from "../../components/teaching/cardComponent/CardComponent";
import styles from "./page.module.css";

export default function TeachingPage(): JSX.Element {
  return (
    <div className={styles.cardsContainer}>
      <CardComponent
        title="Задание"
        description="Создать задание как самостоятельную единицу или для вашего курса"
        linkTo="/tasks/create"
      />
      <CardComponent
        title="Курс"
        description="Создать курс из списка готовых задач, либо из ваших собственных задач для всех или специально для ваших студентов"
        linkTo="/tasks/create"
      />
    </div>
  );
}
