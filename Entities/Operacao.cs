using PoupaDev.API.Enums;

namespace PoupaDev.API.Entities
{
  public class Operacao
  {
    public Operacao(TipoOperacao tipo, decimal valor, Guid idObjetivo)
    {
      Tipo = tipo;
      Valor = valor;
      IdObjetivo = idObjetivo;
      DataOperacao = DateTime.Now;
    }

    public Guid Id { get; private set; }
    public TipoOperacao Tipo { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataOperacao { get; }
    public Guid IdObjetivo { get; set; }
  }
}