import { useState, useEffect } from "react";
import type { PessoaResponseDto } from "../types";
import { TipoTransacao } from "../types";
import { criarTransacao } from "../services/transacaoService";
import { listarPessoas } from "../services/pessoaService";

interface TransacaoFormProps {
  refreshTrigger: number; // mantém o <select> de pessoas sincronizado
  onTransacaoCriada: () => void;
}

function TransacaoForm({ refreshTrigger, onTransacaoCriada }: TransacaoFormProps) {
  const [pessoas, setPessoas] = useState<PessoaResponseDto[]>([]);

  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState(0);
  const [tipo, setTipo] = useState<TipoTransacao>(TipoTransacao.Despesa);
  const [pessoaId, setPessoaId] = useState("");
  const [erro, setErro] = useState<string | null>(null);

  // Carrega a lista de pessoas para preencher o <select>, e recarrega
  // sempre que refreshTrigger mudar (ex: pessoa nova cadastrada em outro form).
  useEffect(() => {
    async function carregar() {
      const dados = await listarPessoas();
      setPessoas(dados);
    }
    carregar();
  }, [refreshTrigger]);

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setErro(null);

    if (!pessoaId) {
      setErro("Selecione uma pessoa.");
      return;
    }

    try {
      await criarTransacao({ descricao, valor, tipo, pessoaId });
      setDescricao("");
      setValor(0);
      setTipo(TipoTransacao.Despesa);
      setPessoaId("");
      onTransacaoCriada();
    } catch {
      // O back-end devolve 400 (menor de idade só despesa) ou 404 (pessoa não existe).
      setErro("Não foi possível cadastrar a transação. Verifique a pessoa selecionada e o tipo escolhido.");
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <h3>Nova Transação</h3>

      <select value={pessoaId} onChange={(e) => setPessoaId(e.target.value)} required>
        <option value="">Selecione uma pessoa</option>
        {pessoas.map((pessoa) => (
          <option key={pessoa.id} value={pessoa.id}>
            {pessoa.nome} ({pessoa.idade} anos)
          </option>
        ))}
      </select>

      <input
        type="text"
        placeholder="Descrição"
        value={descricao}
        onChange={(e) => setDescricao(e.target.value)}
        required
      />

      <input
        type="number"
        placeholder="Valor"
        value={valor}
        onChange={(e) => setValor(Number(e.target.value))}
        required
        min={0}
        step="0.01"
      />

      <select
        value={tipo}
        onChange={(e) => setTipo(Number(e.target.value) as TipoTransacao)}
      >
        <option value={TipoTransacao.Despesa}>Despesa</option>
        <option value={TipoTransacao.Receita}>Receita</option>
      </select>

      <button type="submit">Cadastrar</button>

      {erro && <p style={{ color: "red" }}>{erro}</p>}
    </form>
  );
}

export default TransacaoForm;
