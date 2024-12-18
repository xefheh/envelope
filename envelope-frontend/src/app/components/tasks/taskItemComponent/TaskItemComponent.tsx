import { TaskItemComponentProps } from "./TaskItemComponent.props";
import styles from "./TaskItemComponent.module.css";
import { TagCollection } from "@/app/components/tags/tagCollection/TagCollection";
import cn from "classnames";
import { difficultToString } from "@/app/infarstracture/difficultToString";
import { Difficult } from "@/app/enums/dificulity";
import { useRouter } from "next/navigation";

export const TaskItemComponent = ({
  task,
}: TaskItemComponentProps): JSX.Element => {
  const router = useRouter();

  return (
    <a
      onClick={() => {
        router.push(`/tasks/${task.id}`);
      }}
    >
      <div
        className={cn(styles.taskCard, {
          [styles.isOurChoose]: task.isOurChoose,
        })}
      >
        <div className={styles.leftColumn}>
          <div className={styles.taskInfo}>
            <h1 className={styles.taskTitle}>{task.name}</h1>
            <div
              className={cn(styles.taskDifficult, {
                [styles.difficultEasy]: task.difficult === Difficult.Easy,
                [styles.difficultNormal]: task.difficult === Difficult.Normal,
                [styles.difficultHard]: task.difficult === Difficult.Hard,
              })}
            >
              {difficultToString(task.difficult)}
            </div>
          </div>
          <TagCollection tags={task.tags} />
        </div>
        <div className={cn(styles.rightColumn)}></div>
      </div>
    </a>
  );
};
