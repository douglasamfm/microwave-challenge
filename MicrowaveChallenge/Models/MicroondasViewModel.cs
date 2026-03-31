using MicrowaveChallenge.Domain.Entities;

namespace MicrowaveChallenge.Models;

public class MicroondasViewModel
{
    public int? Tempo { get; set; }
    public int? Potencia { get; set; }
    public string? NomeProgramaSelecionado { get; set; }

    public string Status { get; set; } = "Inativo";
    public string Progresso { get; set; } = string.Empty;
    public string TempoFormatado { get; set; } = "0:00";
    public string Mensagem { get; set; } = string.Empty;

    public List<ProgramaAquecimento> Programas { get; set; } = new();
}