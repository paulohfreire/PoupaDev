using PoupaDev.API.Enums;
using PoupaDev.API.Exceptions;

namespace PoupaDev.API.Entities
{
  public class ObjetivoFinanceiro
  {
    public ObjetivoFinanceiro(string? titulo, string? descricao, decimal? valor)
    {
      Titulo = titulo;
      Descricao = descricao;
      Valor = valor;
      Data = DateTime.Now;

      Operacoes = new List<Operacao>();
    }
    public Guid Id { get; private set; }
    public string? Titulo { get; private set; }
    public string? Descricao { get; private set; }
    public decimal? Valor { get; private set; }
    public DateTime Data { get; private set; }
    public List<Operacao> Operacoes { get; private set; }

    public decimal Saldo => ObterSaldo();

    public void RealizarOperacao(Operacao operacao)
    {
      if (operacao.Tipo == TipoOperacao.Saque && Saldo < operacao.Valor)
        throw new SaldoInsuficienteException();
      Operacoes.Add(operacao);
    }
    public decimal ObterSaldo()
    {
      var totalDeposito = Operacoes.Where(o => o.Tipo == TipoOperacao.Deposito).Sum(o => o.Valor);
      var totalSaque = Operacoes.Where(o => o.Tipo == TipoOperacao.Saque).Sum(o => o.Valor);

      return totalDeposito - totalSaque;
    }

  }
}