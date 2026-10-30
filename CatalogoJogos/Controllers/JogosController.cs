using CatalogoJogos.Models;
using CatalogoJogos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoJogos.Controllers;

[ApiController]
[Route("api/jogos")]
[Authorize]
public class JogosController : ControllerBase
{
    private readonly JogoService _service;

    public JogosController(JogoService service)
    {
        _service = service;
    }

    // POST /api/jogos -> 201 Created
    [HttpPost]
    public ActionResult<Jogo> Criar([FromBody] JogoRequest request)
    {
        var criado = _service.Cadastrar(request);
        return CreatedAtAction(nameof(BuscarPorId), new { id = criado.Id }, criado);
    }

    // GET /api/jogos -> 200 OK
    [HttpGet]
    public ActionResult<IEnumerable<Jogo>> Listar() => Ok(_service.Listar());

    // GET /api/jogos/busca?plataforma=&precoMaximo=
    [HttpGet("busca")]
    public ActionResult<IEnumerable<Jogo>> Buscar([FromQuery] string plataforma, [FromQuery] decimal precoMaximo)
    {
        if (string.IsNullOrWhiteSpace(plataforma))
            return BadRequest(new { mensagem = "Informe a plataforma." });

        return Ok(_service.BuscarPorPlataformaEPreco(plataforma, precoMaximo));
    }

    // GET /api/jogos/relatorio-estoque
    [HttpGet("relatorio-estoque")]
    public ActionResult<IEnumerable<RelatorioEstoqueDto>> RelatorioEstoque()
        => Ok(_service.GerarRelatorioEstoque());

    // GET /api/jogos/{id} -> 200 ou 404
    [HttpGet("{id:int}")]
    public ActionResult<Jogo> BuscarPorId(int id)
    {
        var jogo = _service.BuscarPorId(id);
        return jogo is null ? NotFound() : Ok(jogo);
    }

    // PUT /api/jogos/{id} -> 200 ou 404
    [HttpPut("{id:int}")]
    public ActionResult<Jogo> Atualizar(int id, [FromBody] JogoRequest request)
    {
        var atualizado = _service.Atualizar(id, request);
        return atualizado is null ? NotFound() : Ok(atualizado);
    }

    // DELETE /api/jogos/{id} -> 204 ou 404
    [HttpDelete("{id:int}")]
    public IActionResult Remover(int id)
        => _service.Remover(id) ? NoContent() : NotFound();
}
