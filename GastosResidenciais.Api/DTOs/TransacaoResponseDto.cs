using GastosResidenciais.Api.Models;

namespace GastosResidenciais.Api.DTOs;

/// <summary>Dados de Transação devolvidos pela API.</summary>
public record TransacaoResponseDto(Guid Id, string Descricao, decimal Valor, TipoTransacao Tipo, Guid PessoaId);