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
        _programas = FabricaProgramasPadrao.Criar();
        ValidarCaracteresUnicos();
    }

    public ProcessoAquecimento ObterEstado()
    {
        return _processo;
    }

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
            throw new ExcecaoDeNegocio("Informe um tempo valido.");

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
            throw new ExcecaoDeNegocio("Programa nao encontrado.");

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

    public IReadOnlyCollection<ProgramaAquecimento> ObterProgramas()
    {
        return _programas.AsReadOnly();
    }

    public void AdicionarProgramaCustomizado(ProgramaAquecimento programa)
    {
        if (_programas.Any(p => p.CaractereAquecimento == programa.CaractereAquecimento))
            throw new ExcecaoDeNegocio("Caractere ja utilizado.");

        _programas.Add(programa);
    }

    private void ValidarCaracteresUnicos()
    {
        var existemCaracteresDuplicados = _programas
            .GroupBy(p => p.CaractereAquecimento)
            .Any(g => g.Count() > 1);

        if (existemCaracteresDuplicados)
            throw new InvalidOperationException("Existem caracteres duplicados nos programas padrao.");
    }
}