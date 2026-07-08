using GastosResidenciais.Api.Data;
using GastosResidenciais.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Api.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly AppDbContext _context;
    public PessoaRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Pessoa> CriarAsync(Pessoa pessoa)
    {
        await _context.Pessoas.AddAsync(pessoa);
        await _context.SaveChangesAsync();

        return pessoa;
    }
    public async Task<List<Pessoa>> ListarTodasAsync()
    {
        return await _context.Pessoas.ToListAsync();
    }
    public async Task<Pessoa?> BuscarPorIdAsync(Guid id)
    {
        return await _context.Pessoas.FindAsync(id);
    }
    public async Task RemoverAsync(Guid id)
    {
        var removerPessoa = await _context.Pessoas.FindAsync(id);

        if (removerPessoa == null)
        {
            throw new KeyNotFoundException("Essa pessoa não existe.");
        } 
        
        // Cascade delete (configurado no OnModelCreating) apaga as transações da pessoa.
        _context.Pessoas.Remove(removerPessoa);
        await _context.SaveChangesAsync();
        
    }
}