import React from "react";
import { Link } from "react-router-dom";

export default function LandingPage() {
  return (
    <div style={{ textAlign: "center", padding: "2rem" }}>
      <h1>Welcome to Galactic Game</h1>
      <p>Please choose an option to continue:</p>
      <div style={{ marginTop: "1rem" }}>
        <Link
          to="/login"
          style={{
            marginRight: "1rem",
            padding: "0.5rem 1rem",
            textDecoration: "none",
            backgroundColor: "#007BFF",
            color: "white",
            borderRadius: "5px",
          }}
        >
          Login
        </Link>
        <Link
          to="/register"
          style={{
            padding: "0.5rem 1rem",
            textDecoration: "none",
            backgroundColor: "#28A745",
            color: "white",
            borderRadius: "5px",
          }}
        >
          Register
        </Link>
      </div>
    </div>
  );
}