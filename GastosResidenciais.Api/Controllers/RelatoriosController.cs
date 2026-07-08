using GastosResidenciais.Api.DTOs;
using GastosResidenciais.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Api.Controllers;

/// <summary>Consulta: totais por pessoa + total geral.</summary>
[ApiController]
[Route("api/[controller]")]

public class RelatoriosController : ControllerBase
{
    private readonly IRelatorioService _relatorioService;
    public RelatoriosController (IRelatorioService relatorioService)
    {
        _relatorioService = relatorioService;
    }

[HttpGet]
public async Task<ActionResult<RelatorioTotaisDto>> Consultar() 
{
    var relatorios = await _relatorioService.ObterRelatoriosAsync();
    return Ok(relatorios);
}
}