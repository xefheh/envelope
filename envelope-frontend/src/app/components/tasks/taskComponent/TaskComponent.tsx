"use client";
import { Difficult } from "@/app/enums/dificulity";
import { TaskComponentProps } from "./TaskComponent.props";
import styles from "./TaskComponent.module.css";
import { FormulaInput } from "../../formula/formulaInput/FormulaInput";
import { useRef, useState } from "react";
import cn from "classnames";
import { TagCollection } from "../../tags/tagCollection/TagCollection";
import { postTask } from "@/app/services/taskService";
import { useRouter } from "next/navigation";

export const TaskComponent = ({ mode, task, onSubmit }: TaskComponentProps) => {
  const taskReference = useRef<any | null>(null);
  const answerReference = useRef<any | null>(null);
  const [name, setName] = useState("");
  const [tags, setTags] = useState<string[]>([]);
  const [dificulity, setDificulty] = useState(0);
  const [time, setTime] = useState<number | null>(null);

  const router = useRouter();

  const tagFieldRef = useRef(null);

  return (
    <>
      <form
        onSubmit={(e) => {
          e.preventDefault();
          postTask({
            executionTime: time!,
            name: name,
            answer: answerReference.current!.getContent(),
            description: taskReference.current!.getContent(),
            difficult: dificulity,
            tags: tags,
          }).then((id) => {
            router.push(`/tasks/${id}`);
          });
        }}
        className={styles.createForm}
      >
        <div className={styles.leftColumn}>
          <legend className={styles.formLegend}>СОЗДАНИЕ СВОЕЙ ЗАДАЧИ</legend>
          <label className={styles.createFormLabel}>
            <span className={styles.labelRequired}>Название</span>
            <input
              className={styles.inputField}
              onChange={(e) => setName(e.target.value)}
              type="text"
              name="name"
              placeholder="Название задачи"
              required
            ></input>
          </label>
          <label className={styles.createFormLabel}>
            <span className={styles.labelRequired}>Сложность</span>
            <select
              className={styles.inputField}
              name="dificulity"
              onChange={(e) => setDificulty(Number.parseInt(e.target.value))}
            >
              <option value={Difficult.Easy}>Легкая</option>
              <option value={Difficult.Normal}>Нормальная</option>
              <option value={Difficult.Hard}>Сложная</option>
            </select>
          </label>
          <label className={styles.createFormLabel}>
            <span>Время выполнения задачи (в минутах)</span>
            <input
              type="number"
              onChange={(e) => setTime(e.target.valueAsNumber)}
              className={styles.inputField}
              name="executionTime"
              placeholder="Время выполнения задачи (в минутах)"
            ></input>
          </label>
          <label className={styles.createFormLabel}>
            <span className={styles.labelRequired}>Тэги</span>
            <div className={styles.tagField}>
              <input
                ref={tagFieldRef}
                className={styles.inputField}
                type="text"
                name="name"
                placeholder="Введите тэг"
                required
              ></input>
              <button
                className={styles.addTagBtn}
                onClick={(e) => {
                  e.preventDefault();
                  if (tagFieldRef.current) {
                    if (
                      !tags.includes(tagFieldRef.current.value) &&
                      tagFieldRef.current.value !== ""
                    ) {
                      setTags([tagFieldRef.current.value, ...tags]);
                    }
                  }
                }}
              >
                Добавить
              </button>
            </div>
          </label>
          <TagCollection tags={tags} />
          <button type="submit" className={styles.submitBtn}>
            Создать
          </button>
        </div>
        <div className={styles.rightColumn}>
          <label className={styles.createFormLabel}>
            <span className={cn(styles.labelRequired, styles.labelRightColumn)}>
              Описание и условия
            </span>
            <FormulaInput
              id="input_form"
              height={700}
              placeholder="Описание и условие задачи (минимум 25 слов)"
              reference={taskReference}
            />
          </label>
          <label className={styles.createFormLabel}>
            <span className={cn(styles.labelRequired, styles.labelRightColumn)}>
              Ответ
            </span>
            <FormulaInput
              id="answer_form"
              height={400}
              placeholder="Ответ (минимум 25 слов)"
              reference={answerReference}
            />
          </label>
        </div>
      </form>
    </>
  );
};
