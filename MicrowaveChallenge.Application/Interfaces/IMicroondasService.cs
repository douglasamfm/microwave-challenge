using MicrowaveChallenge.Domain.Entities;

namespace MicrowaveChallenge.Application.Interfaces;

public interface IMicroondasService
{
    ProcessoAquecimento ObterEstado();

    void Iniciar(int? tempo, int? potencia);

    void InicioRapido();

    void IniciarPrograma(string nomePrograma);

    void PausarOuCancelar();

    void Processar();

    IReadOnlyCollection<ProgramaAquecimento> ObterProgramas();

    void AdicionarProgramaCustomizado(ProgramaAquecimento programa);
}