using CatalogoJogos.Models;

namespace CatalogoJogos.Repositories;

/// <summary>
/// Repositório em memória. Registrado como Singleton para que a lista
/// seja mantida durante toda a vida da aplicação.
/// </summary>
public class JogoRepository : IJogoRepository
{
    private readonly List<Jogo> _jogos = new();
    private readonly object _lock = new();
    private int _proximoId = 1;

    public JogoRepository()
    {
        Seed(); // Para iniciar com a lista vazia, basta apagar/comentar esta linha.
    }

    public IEnumerable<Jogo> ObterTodos()
    {
        lock (_lock) return _jogos.ToList();
    }

    public Jogo? ObterPorId(int id)
    {
        lock (_lock) return _jogos.FirstOrDefault(j => j.Id == id);
    }

    public Jogo Adicionar(Jogo jogo)
    {
        lock (_lock)
        {
            jogo.Id = _proximoId++;
            _jogos.Add(jogo);
            return jogo;
        }
    }

    public bool Atualizar(Jogo jogo)
    {
        lock (_lock)
        {
            var existente = _jogos.FirstOrDefault(j => j.Id == jogo.Id);
            if (existente is null) return false;

            existente.Titulo = jogo.Titulo;
            existente.Plataforma = jogo.Plataforma;
            existente.Genero = jogo.Genero;
            existente.Preco = jogo.Preco;
            existente.AnoLancamento = jogo.AnoLancamento;
            existente.Estoque = jogo.Estoque;
            return true;
        }
    }

    public bool Remover(int id)
    {
        lock (_lock)
        {
            var existente = _jogos.FirstOrDefault(j => j.Id == id);
            if (existente is null) return false;
            return _jogos.Remove(existente);
        }
    }

    // ---------- Dados de exemplo ----------
    private void Seed()
    {
        Adicionar(new Jogo { Titulo = "God of War Ragnarök", Plataforma = "PlayStation 5", Genero = "Ação", Preco = 299.90m, AnoLancamento = 2022, Estoque = 15 });
        Adicionar(new Jogo { Titulo = "Gran Turismo 7", Plataforma = "PlayStation 5", Genero = "Corrida", Preco = 249.90m, AnoLancamento = 2022, Estoque = 10 });
        Adicionar(new Jogo { Titulo = "The Legend of Zelda: Tears of the Kingdom", Plataforma = "Nintendo Switch", Genero = "RPG", Preco = 349.90m, AnoLancamento = 2023, Estoque = 20 });
        Adicionar(new Jogo { Titulo = "Mario Kart 8 Deluxe", Plataforma = "Nintendo Switch", Genero = "Corrida", Preco = 279.90m, AnoLancamento = 2017, Estoque = 25 });
        Adicionar(new Jogo { Titulo = "Forza Horizon 5", Plataforma = "Xbox Series X", Genero = "Corrida", Preco = 199.90m, AnoLancamento = 2021, Estoque = 12 });
        Adicionar(new Jogo { Titulo = "Starfield", Plataforma = "Xbox Series X", Genero = "RPG", Preco = 299.90m, AnoLancamento = 2023, Estoque = 8 });
    }
}
