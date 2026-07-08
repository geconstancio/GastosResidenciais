using GastosResidenciais.Api.Models;

namespace GastosResidenciais.Api.DTOs;

/// <summary>Dados recebidos para cadastrar uma Transação.</summary>
public record TransacaoRequestDto(string Descricao, decimal Valor, TipoTransacao Tipo, Guid PessoaId);