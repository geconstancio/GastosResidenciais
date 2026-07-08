namespace GastosResidenciais.Api.DTOs;

/// <summary>Totais de receita, despesa e saldo de uma única pessoa.</summary>
public record TotalPorPessoaDto(Guid PessoaId, string Nome, decimal TotalReceitas, decimal TotalDespesas, decimal Saldo);