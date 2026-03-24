import { useAuth } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";

export default function DashboardPage() {

  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/");
  };

  return (
    <div>
      <h1>Dashboard 🚀</h1>

      <p>Bienvenido {user?.username}</p>
      <p>Email: {user?.email}</p>

      <button onClick={handleLogout}>
        Logout
      </button>
    </div>
  );
}