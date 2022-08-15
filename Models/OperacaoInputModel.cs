using PoupaDev.API.Enums;

namespace PoupaDev.API.Models
{
  public class OperacaoInputModel
  {
    public TipoOperacao Tipo { get; set; }
    public decimal Valor { get; set; }
    public Guid IdObjetivo { get; set; }
  }
}
