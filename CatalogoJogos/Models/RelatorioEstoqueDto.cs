namespace CatalogoJogos.Models;

/// <summary>Linha do relatório de inventário agrupado por plataforma.</summary>
public record RelatorioEstoqueDto(
    string Plataforma,
    int QuantidadeTitulos,
    decimal ValorTotalInventario);
