import { createContext, useContext, useEffect, useState } from "react";
import { jwtDecode } from "jwt-decode";

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
    const storedToken = localStorage.getItem("token");

    if (storedToken) {
      const decoded: any = jwtDecode(storedToken);

      setUser({
        email: decoded.email,
        username: decoded.username
      });

      setToken(storedToken);
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