using GastosResidenciais.Api.DTOs;
using GastosResidenciais.Api.Models;
using GastosResidenciais.Api.Repositories;

namespace GastosResidenciais.Api.Services;

/// <summary>Calcula receita, despesa e saldo por pessoa e o total geral.</summary>
public class RelatorioService : IRelatorioService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IPessoaRepository _pessoaRepository;
    public RelatorioService(ITransacaoRepository transacaoRepository, IPessoaRepository pessoaRepository)
    {
        _transacaoRepository = transacaoRepository;
        _pessoaRepository = pessoaRepository;

    }
    public async Task<RelatorioTotaisDto> ObterRelatoriosAsync()
    {
        var pessoas = await _pessoaRepository.ListarTodasAsync();

        var  todasTransacoes = await _transacaoRepository.ListarTodasAsync();
        
        // Agrupa transações por pessoa antes de cruzar, para não repetir o filtro em cada iteração.
        var transacoesAgrupadas = todasTransacoes.GroupBy(t => t.PessoaId);
        
        // Parte de "pessoas" (não de "transações agrupadas") para garantir que pessoas
        // sem nenhuma transação ainda apareçam no relatório, com totais zerados.
        var totaisPorPessoa = pessoas.Select(pessoa =>
        {
            var grupoDaPessoa = transacoesAgrupadas.FirstOrDefault(g => g.Key == pessoa.Id);
            var totalReceitas = grupoDaPessoa?.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor) ?? 0;
            var totalDespesas = grupoDaPessoa?.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor) ?? 0;
            return new TotalPorPessoaDto(pessoa.Id,pessoa.Nome,totalReceitas,totalDespesas, totalReceitas - totalDespesas);
        }).ToList();
        var totalGeralReceitas = totaisPorPessoa.Sum(p => p.TotalReceitas);
        var totalGeralDespesas = totaisPorPessoa.Sum(p => p.TotalDespesas);
        var saldo = (totalGeralReceitas - totalGeralDespesas);
        return new RelatorioTotaisDto(totaisPorPessoa,new TotalGeralDto(totalGeralReceitas,totalGeralDespesas,saldo));
    }
}
