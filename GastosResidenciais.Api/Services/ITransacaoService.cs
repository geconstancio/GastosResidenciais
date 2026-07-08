using GastosResidenciais.Api.DTOs;

namespace GastosResidenciais.Api.Services;

public interface ITransacaoService
{
    Task<TransacaoResponseDto> CriarAsync(TransacaoRequestDto transacaoRequest);
    Task<List<TransacaoResponseDto>> ListarTodasAsync();
}