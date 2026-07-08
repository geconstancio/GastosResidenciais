import type { RelatorioTotaisDto } from "../types";

const API_URL = "http://localhost:5077/api/Relatorios"

export async function consultarRelatorio(): Promise<RelatorioTotaisDto> {
    const resposta =  await fetch (`${API_URL}`);
    const dados = await resposta.json();
    return dados;
}