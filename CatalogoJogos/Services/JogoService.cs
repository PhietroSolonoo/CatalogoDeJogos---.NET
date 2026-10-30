using CatalogoJogos.Models;
using CatalogoJogos.Repositories;

namespace CatalogoJogos.Services;

public class JogoService
{
    private readonly IJogoRepository _repository;

    public JogoService(IJogoRepository repository)
    {
        _repository = repository;
    }

    public Jogo Cadastrar(JogoRequest request)
    {
        var jogo = MapearParaJogo(request);
        return _repository.Adicionar(jogo);
    }

    public IEnumerable<Jogo> Listar() => _repository.ObterTodos();

    public Jogo? BuscarPorId(int id) => _repository.ObterPorId(id);

    public Jogo? Atualizar(int id, JogoRequest request)
    {
        var jogo = MapearParaJogo(request);
        jogo.Id = id;
        return _repository.Atualizar(jogo) ? _repository.ObterPorId(id) : null;
    }

    public bool Remover(int id) => _repository.Remover(id);

    // ---------- Desafio LINQ ----------

    /// <summary>Jogos da plataforma informada com preço menor ou igual ao máximo.</summary>
    public IEnumerable<Jogo> BuscarPorPlataformaEPreco(string plataforma, decimal precoMaximo)
    {
        return _repository.ObterTodos()
            .Where(j => j.Plataforma.Equals(plataforma, StringComparison.OrdinalIgnoreCase)
                        && j.Preco <= precoMaximo)
            .OrderBy(j => j.Preco)
            .ToList();
    }

    /// <summary>Relatório de inventário agrupado por plataforma.</summary>
    public IEnumerable<RelatorioEstoqueDto> GerarRelatorioEstoque()
    {
        return _repository.ObterTodos()
            .GroupBy(j => j.Plataforma)
            .Select(g => new RelatorioEstoqueDto(
                Plataforma: g.Key,
                QuantidadeTitulos: g.Count(),
                ValorTotalInventario: g.Sum(j => j.Preco * j.Estoque)))
            .OrderBy(r => r.Plataforma)
            .ToList();
    }

    private static Jogo MapearParaJogo(JogoRequest request) => new()
    {
        Titulo = request.Titulo,
        Plataforma = request.Plataforma,
        Genero = request.Genero,
        Preco = request.Preco,
        AnoLancamento = request.AnoLancamento,
        Estoque = request.Estoque
    };
}
