using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveChallenge.Domain.Constants
{
    public static class MensagensDeErro
    {
        public const string TempoInvalido = "Tempo invalido. Informe um valor entre 1 e 120 segundos.";
        public const string PotenciaInvalida = "Potencia invalida. Informe um valor entre 1 e 10.";
        public const string AcrescimoNaoPermitidoProgramaPreDefinido = "Nao e permitido acrescentar tempo em programa pre-definido.";
        public const string CaractereInvalido = "O caractere de aquecimento nao pode ser '.'.";
        public const string CaractereDuplicado = "Caractere ja utilizado.";
        public const string ProgramaNaoEncontrado = "Programa nao encontrado.";
        public const string NomeProgramaObrigatorio = "Nome do programa e obrigatorio.";
        public const string AlimentoObrigatorio = "Alimento e obrigatorio.";
    }
}
