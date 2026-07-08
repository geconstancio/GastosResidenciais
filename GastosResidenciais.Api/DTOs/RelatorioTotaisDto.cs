using System.Collections.Generic;

namespace GastosResidenciais.Api.DTOs;

/// <summary>Resposta completa da consulta de totais: por pessoa + geral.</summary>
public record RelatorioTotaisDto(List<TotalPorPessoaDto> Pessoas, TotalGeralDto TotalGeral);