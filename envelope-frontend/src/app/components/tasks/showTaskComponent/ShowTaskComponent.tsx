import { ShowTaskComponentProps } from "./ShowTaskComponent.props";
import styles from "./ShowTaskComponent.module.css";
import cn from "classnames";
import { Difficult } from "@/app/enums/dificulity";
import { difficultToString } from "@/app/infarstracture/difficultToString";
import { FormulaOutput } from "../../formula/formulaOutput/FormulaOutput";
import { TagCollection } from "../../tags/tagCollection/TagCollection";

export const ShowTaskComponent = ({
  task,
}: ShowTaskComponentProps): JSX.Element => {
  return (
    <div className={styles.wrapper}>
      <div className={styles.info}>
        <div className={styles.nameWithDifficult}>
          <h1 className={styles.taskItem}>{task.name}</h1>
          <div
            className={cn(styles.difficult, {
              [styles.difficultEasy]: task.difficult === Difficult.Easy,
              [styles.difficultNormal]: task.difficult === Difficult.Normal,
              [styles.difficultHard]: task.difficult === Difficult.Hard,
            })}
          >
            {difficultToString(task.difficult)}
          </div>
        </div>

        <TagCollection tags={task.tags} />
        <p className={styles.time}>
          <span className={styles.bold}>Предполагаемое время выполнения:</span>{" "}
          {task.executionTime} минут
        </p>
        <p className={styles.time}>
          <span className={styles.bold}>Автор задачи:</span> {task.authorName}
        </p>
        <h1 className={styles.taskItem}>Содержание задачи</h1>
        <FormulaOutput id="description" value={task.description} height={500} />
        <h1 className={styles.taskItem}>Ответ</h1>
        <FormulaOutput id="answer" value={task.answer} height={250} />
      </div>
    </div>
  );
};
