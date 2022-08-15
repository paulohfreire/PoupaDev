using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoupaDev.API.Entities;
using PoupaDev.API.Models;
using PoupaDev.API.Persistence;

namespace PoupaDev.API.Controllers
{
  [ApiController]
  [Route("api/objetivos-financeiros")]
  public class ObjetivosFinanceirosController : ControllerBase
  {
    private readonly PoupaDevDbContext _context;
    public ObjetivosFinanceirosController(PoupaDevDbContext context)
    {
      _context = context;
    }

    //GET api/objetivos-financeiros
    [HttpGet]
    public IActionResult GetAll()
    {
      var objetivos = _context.Objetivo;
      return Ok(objetivos);
    }

    //GET api/objetivos-financeiros/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
      var objetivo = _context
      .Objetivo
      .Include(o => o.Operacoes)
      .SingleOrDefault(o => o.Id == id);

      if (objetivo == null)
      {
        return NotFound();
      }
      return Ok();
    }

    //POST api/objetivos-financeiros
    [HttpPost]
    public IActionResult Post(ObjetivoFinanceiroModel model)
    {
      var objetivo = new ObjetivoFinanceiro(model.Titulo, model.Descricao, model.Valor);
      _context.Objetivo.Add(objetivo);
      _context.SaveChanges();

      var id = objetivo.Id;
      return CreatedAtAction("GetById", new { id = id }, model);
    }


    //POST api/objetivos-financeiros/{id}/Operacoes
    [HttpPost("{id}/operacoes")]
    public IActionResult Post(OperacaoInputModel model, Guid id)
    {
      var operacao = new Operacao(model.Tipo, model.Valor, model.IdObjetivo);
      var objetivo = _context
      .Objetivo
      .Include(o => o.Operacoes)
      .SingleOrDefault(o => o.Id == id);

      if (objetivo == null)
      {
        return NotFound();
      }

      objetivo.RealizarOperacao(operacao);

      _context.SaveChanges();

      return NoContent();
    }
  }

}