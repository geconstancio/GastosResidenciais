using GastosResidenciais.Api.Models;

namespace GastosResidenciais.Api.Repositories;

/// <summary>Contrato de acesso a dados de Transação.</summary>
public interface ITransacaoRepository
{
    Task<Transacao> CriarAsync(Transacao transacao);

    Task<List<Transacao>> ListarTodasAsync();
}