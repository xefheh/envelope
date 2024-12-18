"use client";

import { Preloader } from "@/app/components/preloader/Preloader";
import { TaskItemCollection } from "@/app/components/tasks/taskItemCollection/TaskItemCollection";
import { getTasks } from "@/app/services/taskService";
import { TaskSearchModel } from "@/app/types/taskSearchModel";
import { useEffect, useState } from "react";

export default function TaskCollectionPage(): JSX.Element {
  const [tasks, setTasks] = useState<TaskSearchModel[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    getTasks().then((tasks) => {
      setTasks(tasks);
      setIsLoading(false);
    });
  }, []);

  return (
    <>{isLoading ? <Preloader /> : <TaskItemCollection tasks={tasks} />}</>
  );
}
