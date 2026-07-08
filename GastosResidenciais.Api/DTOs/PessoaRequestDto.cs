namespace GastosResidenciais.Api.DTOs;

/// <summary>Dados recebidos para cadastrar uma Pessoa (sem Id — gerado pelo servidor).</summary>
public record PessoaRequestDto(string Nome, int Idade);