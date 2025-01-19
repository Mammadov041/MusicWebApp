import { useState } from "react";
import LoginForm from "../components/LoginForm";
import RegisterForm from "../components/RegisterForm";

export default function LoginRegisterPage() {
  const [isLogin, setIsLogin] = useState<boolean>(false);
  const [isRegister, setIsRegister] = useState<boolean>(false);
  return (
    <>
      <h1 style={{ display: "flex", justifyContent: "center" }}>
        Welcome To our Music Web App .
      </h1>
      <div style={{ display: "flex", justifyContent: "center" }}>
        <button
          onClick={() => {
            setIsLogin(true);
            setIsRegister(false);
          }}
        >
          Go to login
        </button>
        <button
          onClick={() => {
            setIsRegister(true);
            setIsLogin(false);
          }}
        >
          Go to register
        </button>
      </div>
      {isLogin && <LoginForm />}
      {isRegister && <RegisterForm />}
    </>
  );
}
