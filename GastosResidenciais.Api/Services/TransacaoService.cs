using GastosResidenciais.Api.DTOs;
using GastosResidenciais.Api.Models;
using GastosResidenciais.Api.Repositories;

namespace GastosResidenciais.Api.Services;

/// <summary>Aplica as regras de negócio de Transação antes de persistir.</summary>
public class TransacaoService : ITransacaoService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IPessoaRepository _pessoaRepository;

    public TransacaoService(ITransacaoRepository transacaoRepository, IPessoaRepository pessoaRepository)
    {
        _transacaoRepository = transacaoRepository;
        _pessoaRepository = pessoaRepository;
    }

    public async Task<TransacaoResponseDto> CriarAsync(TransacaoRequestDto transacaoRequest)
    {
        var pessoa = await _pessoaRepository.BuscarPorIdAsync(transacaoRequest.PessoaId);

        // PessoaId informado precisa existir.
        if (pessoa == null)
        {
            throw new KeyNotFoundException("Essa pessoa não existe.");
        }

        // Menor de idade só pode cadastrar despesa.
        if (pessoa.EhMenorDeIdade && transacaoRequest.Tipo == TipoTransacao.Receita)
        {
            throw new InvalidOperationException ("Menores de idade só podem cadastrar despesas.");
        }
        
        var modeloTransacao = new Transacao
        {
            Descricao = transacaoRequest.Descricao,
            Valor = transacaoRequest.Valor,
            Tipo = transacaoRequest.Tipo,
            PessoaId = transacaoRequest.PessoaId
        };

        var transacaoCriada = await _transacaoRepository.CriarAsync(modeloTransacao);
        return new TransacaoResponseDto(transacaoCriada.Id,transacaoCriada.Descricao,transacaoCriada.Valor,transacaoCriada.Tipo,transacaoCriada.PessoaId);
    }
    public async Task<List<TransacaoResponseDto>> ListarTodasAsync()
    {
        var transacoes = await _transacaoRepository.ListarTodasAsync();

        return transacoes
            .Select(transacao => new TransacaoResponseDto(transacao.Id,transacao.Descricao,transacao.Valor,transacao.Tipo,transacao.PessoaId))
            .ToList();
            
    }
}