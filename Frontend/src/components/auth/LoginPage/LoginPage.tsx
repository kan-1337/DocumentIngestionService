import { useState } from "react";
import { useAuth } from "../../../contexts/AuthContext";
import "./LoginPage.css";

export function LoginPage() {
  const { login } = useAuth();
  const [selectedUser, setSelectedUser] = useState<"user" | "admin">("user");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const users = {
    user: { username: "user", password: "user123", role: "User" },
    admin: { username: "admin", password: "admin123", role: "Admin" },
  };

  const handleLogin = async () => {
    setLoading(true);
    setError(null);

    const credentials = users[selectedUser];

    try {
      await login(credentials.username, credentials.password);
    } catch (e) {
      setError(e instanceof Error ? e.message : "Login failed");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="login-page">
      <div className="login-container">
        <h1>Document Ingestion Demo</h1>
        <p className="login-subtitle">Select a demo user to continue</p>

        <div className="user-selector">
          <button
            className={`user-btn ${selectedUser === "user" ? "active" : ""}`}
            onClick={() => setSelectedUser("user")}
            disabled={loading}
          >
            <div className="user-icon">??</div>
            <div className="user-info">
              <div className="user-name">User</div>
              <div className="user-description">Read-only access</div>
            </div>
          </button>

          <button
            className={`user-btn ${selectedUser === "admin" ? "active" : ""}`}
            onClick={() => setSelectedUser("admin")}
            disabled={loading}
          >
            <div className="user-icon">??</div>
            <div className="user-info">
              <div className="user-name">Admin</div>
              <div className="user-description">Full access (CRUD)</div>
            </div>
          </button>
        </div>

        <div className="credentials-info">
          <p>
            <strong>Username:</strong> {users[selectedUser].username}
          </p>
          <p>
            <strong>Password:</strong> {users[selectedUser].password}
          </p>
          <p className="credentials-note">
            (Credentials are pre-filled for demo purposes)
          </p>
        </div>

        {error && <div className="error-message">{error}</div>}

        <button className="login-btn" onClick={handleLogin} disabled={loading}>
          {loading ? "Logging in..." : `Login as ${users[selectedUser].role}`}
        </button>

        <div className="demo-info">
          <p>
            <strong>Demo Features:</strong>
          </p>
          <ul>
            <li>
              <strong>User</strong> can view and list invoices
            </li>
            <li>
              <strong>Admin</strong> can create, export, and delete invoices
            </li>
          </ul>
        </div>
      </div>
    </div>
  );
}
