import "./App.css";
import { AuthProvider, useAuth } from "./contexts/AuthContext";
import { LoginPage } from "./components/auth/LoginPage/LoginPage";
import { InvoicePage } from "./components/invoices/InvoicePage/InvoicePage";

function AppContent() {
  const { isAuthenticated, logout } = useAuth();

  if (!isAuthenticated) {
    return <LoginPage />;
  }

  return (
    <div className="app">
      <header className="app-header">
        <h1>Document Ingestion – Invoices</h1>
        <button onClick={logout} className="logout-btn">
          Logout
        </button>
      </header>
      <InvoicePage />
    </div>
  );
}

function App() {
  return (
    <AuthProvider>
      <AppContent />
    </AuthProvider>
  );
}

export default App;
