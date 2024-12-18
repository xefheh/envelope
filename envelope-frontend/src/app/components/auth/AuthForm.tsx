"use client";

import { useEffect, useRef, useState } from "react";
import styles from "./AuthForm.module.css";
import Link from "next/link";
import { login, register } from "@/app/services/authService";
import { useRouter } from "next/navigation";

export const AuthForm = (): JSX.Element => {
  const router = useRouter();
  const [isRegistration, setIsRegistration] = useState(false);
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const nameFieldRef = useRef(null);
  const emailFieldRef = useRef(null);
  const passwordFieldRef = useRef(null);

  useEffect(() => {
    let interval = setInterval(() => {
      if (emailFieldRef.current) {
        setEmail(emailFieldRef.current.value);
        clearInterval(interval);
      }
      if (nameFieldRef.current) {
        setName(nameFieldRef.current.value);
        clearInterval(interval);
      }
      if (passwordFieldRef.current) {
        setName(passwordFieldRef.current.value);
        clearInterval(interval);
      }
    }, 100);
  });

  const handleRegistration = () => {
    register(email, name, password)
      .then(() => router.push("/tasks"))
      .catch();
  };

  const handleLogin = () => {
    login(name, password)
      .then(() => router.push("/tasks"))
      .catch();
  };

  const handleRegistrationSwitch = () => {
    setIsRegistration(!isRegistration);
  };

  if (isRegistration) {
    return (
      <form
        method="POST"
        className={styles.authForm}
        onSubmit={(e) => {
          e.preventDefault();
          handleRegistration();
        }}
      >
        <Link className={styles.authHeader} href="/">
          <h1>ENVELOPE</h1>
        </Link>
        <label className={styles.authBlock}>
          Электронная почта
          <input
            ref={emailFieldRef}
            className={styles.authInput}
            type="email"
            name="email"
            placeholder="Электронная почта"
            onInput={(e) => setEmail(e.currentTarget.value)}
          />
        </label>
        <label className={styles.authBlock}>
          Логин
          <input
            ref={nameFieldRef}
            className={styles.authInput}
            type="text"
            name="name"
            placeholder="Логин"
            onInput={(e) => setName(e.currentTarget.value)}
          />
        </label>
        <label className={styles.authBlock}>
          Пароль
          <input
            ref={passwordFieldRef}
            className={styles.authInput}
            type="password"
            name="password"
            placeholder="Пароль"
            onInput={(e) => setPassword(e.currentTarget.value)}
          />
        </label>
        <a
          className={styles.authAnchor}
          href="#"
          onClick={() => handleRegistrationSwitch()}
        >
          Есть аккаунт? <span className={styles.authAnchorColor}>Войти</span>
        </a>
        <button type="submit" className={styles.authButton}>
          Регистрация
        </button>
      </form>
    );
  } else {
    return (
      <form
        method="POST"
        className={styles.authForm}
        onSubmit={(e) => {
          e.preventDefault();
          handleLogin();
        }}
      >
        <Link className={styles.authHeader} href="/">
          <h1>ENVELOPE</h1>
        </Link>
        <label className={styles.authBlock}>
          Логин
          <input
            className={styles.authInput}
            ref={nameFieldRef}
            type="text"
            name="name"
            placeholder="Логин"
            onInput={(e) => setName(e.currentTarget.value)}
          />
        </label>
        <label className={styles.authBlock}>
          Пароль
          <input
            className={styles.authInput}
            ref={passwordFieldRef}
            type="password"
            name="password"
            placeholder="Пароль"
            onInput={(e) => setPassword(e.currentTarget.value)}
          />
        </label>
        <a
          className={styles.authAnchor}
          href="#"
          onClick={() => handleRegistrationSwitch()}
        >
          Нет аккаунта?{" "}
          <span className={styles.authAnchorColor}>Зарегестрироваться</span>
        </a>
        <button type="submit" className={styles.authButton}>
          Войти
        </button>
      </form>
    );
  }
};
