using CatalogoJogos.Models;

namespace CatalogoJogos.Repositories;

public interface IJogoRepository
{
    IEnumerable<Jogo> ObterTodos();
    Jogo? ObterPorId(int id);
    Jogo Adicionar(Jogo jogo);
    bool Atualizar(Jogo jogo);
    bool Remover(int id);
}
