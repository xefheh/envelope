"use client";
import { JSX } from "react";
import { TaskComponent } from "@/app/components/tasks/taskComponent/TaskComponent";
import { postTask } from "@/app/services/taskService";
import { Task } from "@/app/types/task";
import { AuthGuard } from "@/app/components/authGuard/AuthGuard";

export default function createTaskPage(): JSX.Element {
  const onSubmit = (task: Task) => {
    postTask(task)
      .then(() => alert("Всё норм"))
      .catch((e) => alert(e));
  };

  return (
    <AuthGuard>
      <>
        <TaskComponent mode="create" onSubmit={(task) => onSubmit(task)} />
      </>
    </AuthGuard>
  );
}
