namespace GastosResidenciais.Api.DTOs;

/// <summary>Dados de Pessoa devolvidos pela API.</summary>
public record PessoaResponseDto(Guid Id, string Nome, int Idade);