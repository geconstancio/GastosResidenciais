using GastosResidenciais.Api.DTOs;
using GastosResidenciais.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PessoasController : ControllerBase
{
    private readonly IPessoaService _pessoaService;
    public PessoasController (IPessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }

[HttpPost]
public async Task<ActionResult<PessoaResponseDto>> Criar ([FromBody] PessoaRequestDto pessoaRequest)
{
    var pessoaCriada = await _pessoaService.CriarAsync(pessoaRequest);
    return CreatedAtAction(nameof(ListarTodas), null, pessoaCriada);
}

[HttpGet]
public async Task<ActionResult<List<PessoaResponseDto>>> ListarTodas()
{
    var pessoas = await _pessoaService.ListarTodasAsync();
    return Ok(pessoas);
}

// Remove a pessoa; suas transações são apagadas em cascata (configurado no EF Core).
[HttpDelete("{id}")]
public async Task<IActionResult> Deletar (Guid id)
{
    await _pessoaService.RemoverAsync(id);
    return NoContent();
}
}