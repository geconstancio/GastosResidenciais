using GastosResidenciais.Api.DTOs;
using GastosResidenciais.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Api.Controllers;

// Criação e listagem de transação.
[ApiController]
[Route("api/[controller]")]

public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _transacaoService;
    public TransacoesController (ITransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

    [HttpPost]
public async Task<ActionResult<TransacaoResponseDto>> Criar ([FromBody] TransacaoRequestDto transacaoRequest)
{
    var transacaoCriada = await _transacaoService.CriarAsync(transacaoRequest);
    return CreatedAtAction(nameof(ListarTodas), null, transacaoCriada);
}

[HttpGet]
public async Task<ActionResult<List<TransacaoResponseDto>>> ListarTodas()
{
    var transacoes = await _transacaoService.ListarTodasAsync();
    return Ok(transacoes);
}
}