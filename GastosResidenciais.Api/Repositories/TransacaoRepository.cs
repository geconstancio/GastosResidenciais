using Microsoft.EntityFrameworkCore;
using GastosResidenciais.Api.Data;
using GastosResidenciais.Api.Models;

namespace GastosResidenciais.Api.Repositories;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly AppDbContext _context;
    public TransacaoRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Transacao> CriarAsync(Transacao transacao)
    {
        await _context.Transacoes.AddAsync(transacao);
        await _context.SaveChangesAsync();

        return transacao;
    }
    public async Task<List<Transacao>> ListarTodasAsync()
    {
        return await _context.Transacoes.ToListAsync();
    }
}