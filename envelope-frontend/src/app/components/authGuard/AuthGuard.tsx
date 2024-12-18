import { checkLoggedState } from "@/app/services/authService";
import { useRouter } from "next/navigation";
import { AuthGuardProps } from "./AuthGuard.props";
import { useEffect, useLayoutEffect } from "react";

export const AuthGuard = ({ children }: AuthGuardProps): JSX.Element => {
  const router = useRouter();

  useEffect(() => {
    if (!checkLoggedState()) {
      router.push("/auth");
    }
  }, []);

  return <>{children}</>;
};
