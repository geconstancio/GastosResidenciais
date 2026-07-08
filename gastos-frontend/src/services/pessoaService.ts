// Encapsula as chamadas HTTP de Pessoa; componentes não lidam com fetch/URL diretamente.
import type { PessoaResponseDto, PessoaRequestDto } from "../types";

const API_URL = "http://localhost:5077/api/Pessoas";

export async function listarPessoas(): Promise<PessoaResponseDto[]> {
    const resposta = await fetch(`${API_URL}`);
    const dados = await resposta.json();
    return dados;
}

export async function criarPessoa(pessoaRequest: PessoaRequestDto): Promise<PessoaResponseDto> {
    const resposta = await fetch(`${API_URL}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(pessoaRequest)
    });
  return await resposta.json();
}

// Sem body: o id já vai na própria URL, como parâmetro de rota.
export async function removerPessoa(id: string): Promise<void> {
    await fetch(`${API_URL}/${id}`, {
        method: "DELETE"
    });
}