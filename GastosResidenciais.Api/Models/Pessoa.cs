namespace GastosResidenciais.Api.Models;

/// <summary>Pessoa cadastrada no sistema, dona de zero ou mais transações.</summary>
public class Pessoa
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    
    /// <summary>Usado para restringir menores de idade a cadastrar apenas despesas.</summary>
    public bool EhMenorDeIdade => Idade < 18;
}