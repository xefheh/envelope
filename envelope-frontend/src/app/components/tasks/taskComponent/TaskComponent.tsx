"use client";
import { Difficult } from "@/app/enums/dificulity";
import { TaskComponentProps } from "./TaskComponent.props";
import styles from "./TaskComponent.module.css";
import { FormulaInput } from "../../formula/formulaInput/FormulaInput";
import { useRef, useState } from "react";
import cn from "classnames";

export const TaskComponent = ({ mode, task, onSubmit }: TaskComponentProps) => {
  const taskReference = useRef<any | null>(null);
  const answerReference = useRef<any | null>(null);
  const [name, setName] = useState("");
  const [dificulity, setDificulty] = useState(0);
  const [time, setTime] = useState<number | null>(null);

  return (
    <>
      <form
        onSubmit={(e) => {
          e.preventDefault();
          onSubmit({
            executionTime: time!,
            name: name,
            answer: answerReference.current!.getContent(),
            description: taskReference.current!.getContent(),
            difficult: dificulity,
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
              <option value={Difficult.Easy}>Лёгкая</option>
              <option value={Difficult.Normal}>Нормальная</option>
              <option value={Difficult.Hard}>Средняя</option>
            </select>
          </label>
          <label className={styles.createFormLabel}>
            <span>Время выполнения задачи (если есть)</span>
            <input
              type="number"
              onChange={(e) => setTime(e.target.valueAsNumber)}
              className={styles.inputField}
              name="executionTime"
              placeholder="Время выполнения задачи (в минутах)"
            ></input>
          </label>
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
