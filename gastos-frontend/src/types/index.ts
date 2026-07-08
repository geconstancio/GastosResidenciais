// Espelham os DTOs do back-end (.NET). Guid vira string; decimal/int vira number.

export interface PessoaResponseDto {
    id: string;
    nome: string;
    idade: number;
}

export interface PessoaRequestDto {
    nome: string;
    idade: number;
}

// Mesma codificação numérica do TipoTransacao em C# (Receita = 0, Despesa = 1).
export const TipoTransacao = {
  Receita: 0,
  Despesa: 1,
} as const;

export type TipoTransacao = (typeof TipoTransacao)[keyof typeof TipoTransacao];

export interface TransacaoRequestDto {
    descricao: string;
    valor: number;
    tipo: TipoTransacao;
    pessoaId: string;
}

export interface TransacaoResponseDto {
    id: string;
    descricao: string;
    valor: number;
    tipo: TipoTransacao;
    pessoaId: string;
}

export interface TotalPorPessoaDto {
    pessoaId: string;
    nome: string;
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
}

export interface TotalGeralDto {
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
}

export interface RelatorioTotaisDto {
    pessoas: TotalPorPessoaDto[];
    totalGeral: TotalGeralDto;
}