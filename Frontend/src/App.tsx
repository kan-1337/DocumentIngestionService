import "./App.css";
import { InvoicePage } from "./components/invoices/InvoicePage/InvoicePage.tsx";

function App() {
  return (
    <div className="app">
      <h1>Document Ingestion – Invoices</h1>
      <InvoicePage />
    </div>
  );
}

export default App;
