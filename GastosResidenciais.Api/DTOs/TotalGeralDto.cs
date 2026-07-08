namespace GastosResidenciais.Api.DTOs;

/// <summary>Totais consolidados de todas as pessoas.</summary>
public record TotalGeralDto(decimal TotalReceitas, decimal TotalDespesas, decimal Saldo);