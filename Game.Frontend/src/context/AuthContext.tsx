import { createContext, useContext, useEffect, useState } from "react";
import { jwtDecode } from "jwt-decode";
import { apiFetch } from "../api/api";

type User = {
  email: string;
  username: string;
};

type AuthContextType = {
  user: User | null;
  token: string | null;
  login: (token: string) => void;
  logout: () => void;
};

const AuthContext = createContext<AuthContextType | null>(null);

export function AuthProvider({ children }: { children: React.ReactNode }) {

  const [user, setUser] = useState<User | null>(null);
  const [token, setToken] = useState<string | null>(null);

  useEffect(() => {

    const validateToken = async () => {
      try {
        const data = await apiFetch("http://localhost:5279/api/auth/me");
  
        setUser({
          email: data.email,
          username: data.username
        });
  
        setToken(localStorage.getItem("token"));
  
      } catch {
        logout();
      }
    };
  
    const storedToken = localStorage.getItem("token");
  
    if (storedToken) {
      validateToken();
    }
  
  }, []);

  const login = (token: string) => {
    const decoded: any = jwtDecode(token);

    setUser({
      email: decoded.email,
      username: decoded.username
    });

    setToken(token);
    localStorage.setItem("token", token);
  };

  const logout = () => {
    setUser(null);
    setToken(null);
    localStorage.removeItem("token");
  };

  return (
    <AuthContext.Provider value={{ user, token, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) throw new Error("useAuth must be used inside AuthProvider");
  return context;
}