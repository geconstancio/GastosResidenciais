import { useState } from "react";
import { criarPessoa } from "../services/pessoaService";

// onPessoaCriada avisa o App para recarregar as listas dependentes.
interface PessoaFormProps {
  onPessoaCriada: () => void;
}

function PessoaForm({ onPessoaCriada }: PessoaFormProps) {
  const [nome, setNome] = useState("");
  const [idade, setIdade] = useState(0);
  const [erro, setErro] = useState<string | null>(null);

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setErro(null);

    try {
      await criarPessoa({ nome, idade });
      setNome("");
      setIdade(0);
      onPessoaCriada(); // avisa o App que a lista precisa ser atualizada
    } catch {
      setErro("Não foi possível cadastrar a pessoa. Tente novamente.");
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <h3>Nova Pessoa</h3>

      <input
        type="text"
        placeholder="Nome"
        value={nome}
        onChange={(e) => setNome(e.target.value)}
        required
      />
      <input
        type="number"
        placeholder="Idade"
        value={idade}
        onChange={(e) => setIdade(Number(e.target.value))}
        required
        min={0}
      />
      <button type="submit">Cadastrar</button>

      {erro && <p style={{ color: "red" }}>{erro}</p>}
    </form>
  );
}

export default PessoaForm;
