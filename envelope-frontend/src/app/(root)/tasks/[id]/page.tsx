"use client";

import styles from "./page.module.css";
import { use, useEffect, useState } from "react";
import { Preloader } from "@/app/components/preloader/Preloader";
import { Task } from "@/app/types/task";
import { ShowTaskComponent } from "@/app/components/tasks/showTaskComponent/ShowTaskComponent";
import { getTask } from "@/app/services/taskService";

export default function ShowTaskPage({
  params,
}: {
  params: Promise<{ id: string }>;
}): JSX.Element {
  const [task, setTask] = useState<Task>();
  const [isLoading, setIsLoading] = useState(true);

  const { id } = use(params);

  useEffect(() => {
    getTask(id).then((task) => {
      setTask(task);
      setIsLoading(false);
    });
  }, [id]);

  return (
    <>
      {isLoading ? (
        <Preloader />
      ) : (
        <div className={styles.wrapper}>
          <ShowTaskComponent task={task!} />
        </div>
      )}
    </>
  );
}
