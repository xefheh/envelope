import { TaskItemComponent } from "@/app/components/tasks/taskItemComponent/TaskItemComponent";
import styles from "./TaskItemCollection.module.css";
import { TaskItemCollectionProps } from "./TaskItemCollection.props";
import Link from "next/link";

export const TaskItemCollection = ({
  tasks,
}: TaskItemCollectionProps): JSX.Element => {
  return (
    <div className={styles.courseContainer}>
      {tasks.map((task) => (
        <TaskItemComponent key={task.id} task={task} />
      ))}
    </div>
  );
};
