using GastosResidenciais.Api.DTOs;

namespace GastosResidenciais.Api.Services;

public interface IPessoaService
{
    Task<PessoaResponseDto> CriarAsync(PessoaRequestDto pessoaRequest);
    Task RemoverAsync(Guid id);
    Task<List<PessoaResponseDto>> ListarTodasAsync();
}