import type { TransacaoRequestDto, TransacaoResponseDto } from "../types";

const API_URL = "http://localhost:5077/api/Transacoes";

export async function listarTransacoes(): Promise<TransacaoResponseDto[]> {
    const resposta = await fetch (`${API_URL}`);
    const dados = await resposta.json();
    return dados;
}

export async function criarTransacao (transacaoRequest: TransacaoRequestDto) : Promise<TransacaoResponseDto> {
    const resposta = await fetch (`${API_URL}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(transacaoRequest)
    });
  return await resposta.json();
}