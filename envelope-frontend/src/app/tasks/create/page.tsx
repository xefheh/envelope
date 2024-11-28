"use client";
import { JSX } from "react";
import { TaskComponent } from "@/app/components/tasks/taskComponent/TaskComponent";
import { postTask } from "@/app/services/taskService";

export default function createTaskPage(): JSX.Element {
  return (
    <>
      <TaskComponent mode="create" onSubmit={(task) => postTask(task)} />
    </>
  );
}
