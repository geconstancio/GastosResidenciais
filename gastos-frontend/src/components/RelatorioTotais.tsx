import { useState, useEffect } from "react";
import type { RelatorioTotaisDto } from "../types";
import { consultarRelatorio } from "../services/relatorioService";

interface RelatorioTotaisProps {
  refreshTrigger: number;
}

function RelatorioTotais({ refreshTrigger }: RelatorioTotaisProps) {
  const [relatorio, setRelatorio] = useState<RelatorioTotaisDto | null>(null);

  useEffect(() => {
    async function carregar() {
      const dados = await consultarRelatorio();
      setRelatorio(dados);
    }
    carregar();
  }, [refreshTrigger]);

  // Evita acessar relatorio.pessoas antes da resposta da API chegar.
  if (!relatorio) {
    return <p>Carregando relatório...</p>;
  }

  return (
    <div>
      <h3>Relatório de Totais</h3>
      <table border={1} cellPadding={6}>
        <thead>
          <tr>
            <th>Pessoa</th>
            <th>Receitas</th>
            <th>Despesas</th>
            <th>Saldo</th>
          </tr>
        </thead>
        <tbody>
          {relatorio.pessoas.map((pessoa) => (
            <tr key={pessoa.pessoaId}>
              <td>{pessoa.nome}</td>
              <td>R$ {pessoa.totalReceitas.toFixed(2)}</td>
              <td>R$ {pessoa.totalDespesas.toFixed(2)}</td>
              <td>R$ {pessoa.saldo.toFixed(2)}</td>
            </tr>
          ))}
        </tbody>
        <tfoot>
          <tr>
            <td><strong>Total Geral</strong></td>
            <td><strong>R$ {relatorio.totalGeral.totalReceitas.toFixed(2)}</strong></td>
            <td><strong>R$ {relatorio.totalGeral.totalDespesas.toFixed(2)}</strong></td>
            <td><strong>R$ {relatorio.totalGeral.saldo.toFixed(2)}</strong></td>
          </tr>
        </tfoot>
      </table>
    </div>
  );
}

export default RelatorioTotais;
