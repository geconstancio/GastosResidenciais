import { useState } from "react";
import PessoaForm from "./components/PessoaForm";
import PessoaLista from "./components/PessoaLista";
import TransacaoForm from "./components/TransacaoForm";
import TransacaoLista from "./components/TransacaoLista";
import RelatorioTotais from "./components/RelatorioTotais";
import "./App.css";

function App() {
  // Contador incrementado a cada criação/remoção; passado como dependência
  // do useEffect nos componentes de listagem, forçando-os a recarregar.
  const [refreshTrigger, setRefreshTrigger] = useState(0);

  function atualizar() {
    setRefreshTrigger((valorAtual) => valorAtual + 1);
  }

  return (
    <div style={{ maxWidth: 900, margin: "0 auto", padding: 24 }}>
      <h1>Controle de Gastos Residenciais</h1>

      <section>
        <PessoaForm onPessoaCriada={atualizar} />
        <PessoaLista refreshTrigger={refreshTrigger} onPessoaRemovida={atualizar} />
      </section>

      <hr />

      <section>
        <TransacaoForm refreshTrigger={refreshTrigger} onTransacaoCriada={atualizar} />
        <TransacaoLista refreshTrigger={refreshTrigger} />
      </section>

      <hr />

      <section>
        <RelatorioTotais refreshTrigger={refreshTrigger} />
      </section>
    </div>
  );
}

export default App;
