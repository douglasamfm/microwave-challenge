using MicrowaveChallenge.Application.Interfaces;
using MicrowaveChallenge.Domain.Entities;
using MicrowaveChallenge.Domain.Enums;
using MicrowaveChallenge.Domain.Exceptions;

namespace MicrowaveChallenge.Application.Services;

public class MicroondasService : IMicroondasService
{
    private readonly ProcessoAquecimento _processo = new();
    private readonly List<ProgramaAquecimento> _programas;

    public MicroondasService()
    {
        _programas = CriarProgramasPadrao();
        ValidarCaracteresUnicos();
    }

    public ProcessoAquecimento ObterEstado() => _processo;

    public void Iniciar(int? tempo, int? potencia)
    {
        if (_processo.Status == StatusMicroondas.EmExecucao)
        {
            _processo.AcrescentarTempo();
            return;
        }

        if (_processo.Status == StatusMicroondas.Pausado)
        {
            _processo.Retomar();
            return;
        }

        if (!tempo.HasValue && !potencia.HasValue)
        {
            _processo.InicioRapido();
            return;
        }

        if (!tempo.HasValue)
            throw new ExcecaoDeNegocio("Informe um tempo válido.");

        _processo.Iniciar(tempo.Value, potencia);
    }

    public void InicioRapido()
    {
        _processo.InicioRapido();
    }

    public void IniciarPrograma(string nomePrograma)
    {
        var programa = _programas.FirstOrDefault(p =>
            p.Nome.Equals(nomePrograma, StringComparison.OrdinalIgnoreCase));

        if (programa is null)
            throw new ExcecaoDeNegocio("Programa não encontrado.");

        _processo.IniciarProgramaPreDefinido(programa);
    }

    public void PausarOuCancelar()
    {
        _processo.PausarOuCancelar();
    }

    public void Processar()
    {
        _processo.ProcessarSegundo();
    }

    public List<ProgramaAquecimento> ObterProgramas()
    {
        return _programas;
    }

    public void AdicionarProgramaCustomizado(ProgramaAquecimento programa)
    {
        if (_programas.Any(p => p.CaractereAquecimento == programa.CaractereAquecimento))
            throw new ExcecaoDeNegocio("Caractere já utilizado.");

        _programas.Add(programa);
    }

    private List<ProgramaAquecimento> CriarProgramasPadrao()
    {
        return new List<ProgramaAquecimento>
        {
            new("Pipoca", "Pipoca de micro-ondas", 120, 7, '*',
                "Observar o intervalo entre estouros."),

            new("Leite", "Leite", 120, 5, '~',
                "Cuidado com líquidos quentes."),

            new("Carnes", "Carne em pedaços ou fatias", 120, 4, '#',
                "Interrompa na metade e vire o conteúdo."),

            new("Frango", "Frango", 120, 7, '@',
                "Interrompa na metade e vire o conteúdo."),

            new("Feijão", "Feijão congelado", 120, 9, '%',
                "Deixe o recipiente destampado.")
        };
    }

    private void ValidarCaracteresUnicos()
    {
        var repetidos = _programas
            .GroupBy(p => p.CaractereAquecimento)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (repetidos.Any())
            throw new Exception("Existem caracteres duplicados nos programas padrão.");
    }
}