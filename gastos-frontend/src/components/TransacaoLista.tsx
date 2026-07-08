import { useState, useEffect } from "react";
import type { TransacaoResponseDto, PessoaResponseDto } from "../types";
import { TipoTransacao } from "../types";
import { listarTransacoes } from "../services/transacaoService";
import { listarPessoas } from "../services/pessoaService";

interface TransacaoListaProps {
  refreshTrigger: number;
}

function TransacaoLista({ refreshTrigger }: TransacaoListaProps) {
  const [transacoes, setTransacoes] = useState<TransacaoResponseDto[]>([]);
  const [pessoas, setPessoas] = useState<PessoaResponseDto[]>([]);

  useEffect(() => {
    async function carregar() {
      const [dadosTransacoes, dadosPessoas] = await Promise.all([
        listarTransacoes(),
        listarPessoas(),
      ]);
      setTransacoes(dadosTransacoes);
      setPessoas(dadosPessoas);
    }
    carregar();
  }, [refreshTrigger]);

  // TransacaoResponseDto só traz o PessoaId (decisão de design para evitar
  // overfetching); o nome é resolvido aqui cruzando com a lista de pessoas.
  function nomeDaPessoa(pessoaId: string): string {
    const pessoa = pessoas.find((p) => p.id === pessoaId);
    return pessoa ? pessoa.nome : "Desconhecida";
  }

  return (
    <div>
      <h3>Transações</h3>
      <ul>
        {transacoes.map((transacao) => (
          <li key={transacao.id}>
            {transacao.descricao} — R$ {transacao.valor.toFixed(2)} —{" "}
            {transacao.tipo === TipoTransacao.Receita ? "Receita" : "Despesa"} —{" "}
            {nomeDaPessoa(transacao.pessoaId)}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default TransacaoLista;
