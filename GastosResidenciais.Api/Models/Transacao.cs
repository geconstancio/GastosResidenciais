namespace GastosResidenciais.Api.Models;

/// <summary>Transação financeira (receita ou despesa) vinculada a uma Pessoa.</summary>
public class Transacao
{
     public Guid Id { get; set; } = Guid.NewGuid();
    public string Descricao { get; set; } = string.Empty;

    /// <summary>decimal evita erro de arredondamento em valores monetários.</summary>
    public decimal Valor { get; set; } 
    
    public TipoTransacao Tipo { get; set; }
    public Guid PessoaId { get; set; }
    public Pessoa? Pessoa { get; set; }
}