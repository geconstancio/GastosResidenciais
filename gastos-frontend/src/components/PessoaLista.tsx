import { useState, useEffect } from "react";
import type { PessoaResponseDto } from "../types";
import { listarPessoas, removerPessoa } from "../services/pessoaService";

// refreshTrigger força o recarregamento quando o App avisa que algo mudou.
// onPessoaRemovida propaga o evento (a remoção afeta a lista de Transações via cascade delete).
interface PessoaListaProps {
  refreshTrigger: number;
  onPessoaRemovida: () => void;
}

function PessoaLista({ refreshTrigger, onPessoaRemovida }: PessoaListaProps) {
  const [pessoas, setPessoas] = useState<PessoaResponseDto[]>([]);

  useEffect(() => {
    async function carregar() {
      const dados = await listarPessoas();
      setPessoas(dados);
    }
    carregar();
  }, [refreshTrigger]);

  async function handleRemover(id: string) {
    await removerPessoa(id);
    onPessoaRemovida();
  }

  return (
    <div>
      <h3>Pessoas Cadastradas</h3>
      <ul>
        {pessoas.map((pessoa) => (
          <li key={pessoa.id}>
            {pessoa.nome} — {pessoa.idade} anos
            <button onClick={() => handleRemover(pessoa.id)}>Remover</button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default PessoaLista;
