using GastosResidenciais.Api.Models;

namespace GastosResidenciais.Api.Repositories;

/// <summary>Contrato de acesso a dados de Pessoa (implementação isolada do EF Core).</summary>
public interface IPessoaRepository
{
    Task<Pessoa> CriarAsync(Pessoa pessoa);
    Task RemoverAsync(Guid id);
    Task<List<Pessoa>> ListarTodasAsync();
    Task<Pessoa?> BuscarPorIdAsync(Guid id);
}