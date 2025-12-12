import "./App.css";
import { InvoicePage } from "./components/invoices/invoicePage.tsx";

function App() {
  return (
    <div className="app">
      <h1>Document Ingestion – Invoices</h1>
      <InvoicePage />
    </div>
  );
}

export default App;
