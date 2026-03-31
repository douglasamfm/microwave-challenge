using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using global::MicrowaveChallenge.Domain.Entities;
using MicrowaveChallenge.Domain.Entities;

namespace MicrowaveChallenge.Application.Interfaces
{


    public interface IMicroondasService
    {
        ProcessoAquecimento ObterEstado();

        void Iniciar(int? tempo, int? potencia);

        void InicioRapido();

        void IniciarPrograma(string nomePrograma);

        void PausarOuCancelar();

        void Processar();

        List<ProgramaAquecimento> ObterProgramas();

        void AdicionarProgramaCustomizado(ProgramaAquecimento programa);
    }
}
