import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";
import { useAuth } from "../context/AuthContext";
import { apiFetch } from "../api/api";



export default function LoginPage() {

  const { login } = useAuth();
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  

  const handleLogin = async () => {
    try {
      const data = await apiFetch("http://localhost:5279/api/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password })
      });
  
      if (!data || !data.token) {
        throw new Error("Login failed");
      }
  
      login(data.token);
  
      navigate("/dashboard");
  
    } catch (error) {
      alert("Credenciales incorrectas");
    }
  };

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      navigate("/dashboard");
    }
  }, []);
  
  return (
    <div>
      <h1>Login</h1>

      <input
        placeholder="email"
        onChange={(e) => setEmail(e.target.value)}
      />

      <input
        type="password"
        placeholder="password"
        onChange={(e) => setPassword(e.target.value)}
      />

      <button onClick={handleLogin}>Login</button>

      <p onClick={() => navigate("/register")} style={{ cursor: "pointer" }}>
        No tienes cuenta? Regístrate
      </p>
    </div>
  );
}