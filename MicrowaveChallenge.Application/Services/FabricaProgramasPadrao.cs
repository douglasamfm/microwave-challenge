using MicrowaveChallenge.Domain.Entities;

namespace MicrowaveChallenge.Application.Services;

public static class FabricaProgramasPadrao
{
    public static List<ProgramaAquecimento> Criar()
    {
        return new List<ProgramaAquecimento>
        {
            new("Pipoca", "Pipoca de micro-ondas", 120, 7, '*',
                "Observar o barulho dos estouros do milho. Caso haja intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento."),

            new("Leite", "Leite", 120, 5, '~',
                "Cuidado com aquecimento de liquidos, o choque termico aliado ao movimento do recipiente pode causar fervura imediata e queimaduras."),

            new("Carnes de boi", "Carne em pedaco ou fatias", 120, 4, '#',
                "Interrompa o processo na metade e vire o conteudo para aquecimento e descongelamento uniforme."),

            new("Frango", "Frango (qualquer corte)", 120, 7, '@',
                "Interrompa o processo na metade e vire o conteudo para aquecimento e descongelamento uniforme."),

            new("Feijao", "Feijao congelado", 120, 9, '%',
                "Deixe o recipiente destampado. Em recipientes plasticos, cuidado ao retirar, pois podem perder resistencia em altas temperaturas.")
        };
    }
}