import { Metadata } from "next";
import { AuthForm } from "../../components/auth/AuthForm";
import styles from "./page.module.css";
import { AuthGuard } from "@/app/components/authGuard/AuthGuard";

export const metadata: Metadata = {
  title: "Envelope Education: Авторизация",
};

export default function AuthPage(): JSX.Element {
  return (
    <div className={styles.authWrapper}>
      <AuthForm />
    </div>
  );
}
