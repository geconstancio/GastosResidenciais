using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace GastosResidenciais.Api.Middlewares;

/// <summary>
/// Converte exceções em respostas HTTP corretas, evitando 500
/// cru com stack trace exposta para o cliente da API.
/// </summary>
public class TratamentoDeExcecoesMiddleware
{
    private readonly RequestDelegate _next;
    public TratamentoDeExcecoesMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException ex)
        {
            // Ex: PessoaId informado não existe.
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            // Ex: menor de idade tentando cadastrar receita.
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(ex.Message);
        }
    }
}