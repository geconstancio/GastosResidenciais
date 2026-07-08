using GastosResidenciais.Api.DTOs;
using GastosResidenciais.Api.Models;
using GastosResidenciais.Api.Repositories;
using Microsoft.VisualBasic;
using System.Linq;

namespace GastosResidenciais.Api.Services;

/// <summary>Criação, remoção e listagem de Pessoas, convertendo DTO para Entidade.</summary>
public class PessoaService : IPessoaService
{
    private readonly IPessoaRepository _pessoaRepository;
    public PessoaService(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }
    public async Task<PessoaResponseDto> CriarAsync(PessoaRequestDto pessoaRequest)
    {
        var modeloPessoa = new Pessoa
        {
            Nome = pessoaRequest.Nome,
            Idade = pessoaRequest.Idade
        };

        var pessoaCriada = await _pessoaRepository.CriarAsync(modeloPessoa);  
        
        return new PessoaResponseDto(pessoaCriada.Id, pessoaCriada.Nome, pessoaCriada.Idade);
    }

    public async Task<List<PessoaResponseDto>> ListarTodasAsync()
    {
        var pessoas = await _pessoaRepository.ListarTodasAsync();
        
        return pessoas
            .Select(pessoa => new PessoaResponseDto(pessoa.Id,pessoa.Nome,pessoa.Idade))
            .ToList();
    }

    // Existência da pessoa e cascade delete
    // já são tratados no Repository/EF Core.
    public async Task RemoverAsync(Guid id)
    {
       await _pessoaRepository.RemoverAsync(id); 
    }
}