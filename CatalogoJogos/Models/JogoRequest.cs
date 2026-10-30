using System.ComponentModel.DataAnnotations;

namespace CatalogoJogos.Models;

/// <summary>Dados recebidos para cadastrar/atualizar um jogo (o Id é gerado pelo sistema).</summary>
public class JogoRequest
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "A plataforma é obrigatória.")]
    public string Plataforma { get; set; } = string.Empty;

    [Required(ErrorMessage = "O gênero é obrigatório.")]
    public string Genero { get; set; } = string.Empty;

    [Range(0, 1000000, ErrorMessage = "O preço deve estar entre 0 e 1.000.000.")]
    public decimal Preco { get; set; }

    [Range(1950, 2100, ErrorMessage = "Ano de lançamento inválido.")]
    public int AnoLancamento { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo.")]
    public int Estoque { get; set; }
}
