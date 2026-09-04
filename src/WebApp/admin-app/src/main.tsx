import React, { useEffect, useState } from "react";
import { createRoot } from "react-dom/client";
import axios from "axios";
import "./style.css";

const API = "http://localhost:5001/api";

type Company = { id: string; name: string; email?: string };
type Account = { id: string; code: string; name: string; accountType: string };

function App() {
  const [companies, setCompanies] = useState<Company[]>([]);
  const [companyName, setCompanyName] = useState("");
  const [companyId, setCompanyId] = useState("");
  const [accounts, setAccounts] = useState<Account[]>([]);
  const [code, setCode] = useState("");
  const [name, setName] = useState("");

  const loadCompanies = async () => {
    const r = await axios.get<Company[]>(`${API}/Companies`);
    setCompanies(r.data);
  };

  useEffect(() => { loadCompanies(); }, []);

  useEffect(() => {
    if (companyId) {
      axios.get<Account[]>(`${API}/Accounts/${companyId}`)
        .then(r => setAccounts(r.data));
    }
  }, [companyId]);

  const addCompany = async () => {
    await axios.post(`${API}/Companies`, { name: companyName, email: null });
    setCompanyName("");
    await loadCompanies();
  };

  const addAccount = async () => {
    if (!companyId) return;
    await axios.post(`${API}/Accounts`, {
      companyId, code, name, accountType: "Asset"
    });
    setCode(""); setName("");
    const r = await axios.get<Account[]>(`${API}/Accounts/${companyId}`);
    setAccounts(r.data);
  };

  return (
    <main>
      <h1>Accounting System</h1>

      <section>
        <h2>Companies</h2>
        <input value={companyName} onChange={e => setCompanyName(e.target.value)} placeholder="Company name" />
        <button onClick={addCompany}>Add Company</button>
        <select value={companyId} onChange={e => setCompanyId(e.target.value)}>
          <option value="">Select Company</option>
          {companies.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
        </select>
      </section>

      <section>
        <h2>Chart of Accounts</h2>
        <input value={code} onChange={e => setCode(e.target.value)} placeholder="Code" />
        <input value={name} onChange={e => setName(e.target.value)} placeholder="Account name" />
        <button onClick={addAccount}>Add Account</button>
        <table>
          <thead><tr><th>Code</th><th>Name</th><th>Type</th></tr></thead>
          <tbody>{accounts.map(a => <tr key={a.id}><td>{a.code}</td><td>{a.name}</td><td>{a.accountType}</td></tr>)}</tbody>
        </table>
      </section>
    </main>
  );
}

createRoot(document.getElementById("root")!).render(<App />);
