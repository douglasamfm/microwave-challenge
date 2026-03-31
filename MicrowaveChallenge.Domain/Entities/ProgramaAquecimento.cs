using MicrowaveChallenge.Domain.Exceptions;

namespace MicrowaveChallenge.Domain.Entities;

public class ProgramaAquecimento
{
    public string Nome { get; private set; }
    public string Alimento { get; private set; }
    public int TempoEmSegundos { get; private set; }
    public int Potencia { get; private set; }
    public char CaractereAquecimento { get; private set; }
    public string Instrucoes { get; private set; }

    public ProgramaAquecimento(
        string nome,
        string alimento,
        int tempoEmSegundos,
        int potencia,
        char caractereAquecimento,
        string instrucoes)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ExcecaoDeNegocio("Nome do programa é obrigatório.");

        if (string.IsNullOrWhiteSpace(alimento))
            throw new ExcecaoDeNegocio("Alimento é obrigatório.");

        if (tempoEmSegundos < 1)
            throw new ExcecaoDeNegocio("Tempo deve ser maior que zero.");

        if (potencia < 1 || potencia > 10)
            throw new ExcecaoDeNegocio("Potência deve estar entre 1 e 10.");

        if (caractereAquecimento == '.')
            throw new ExcecaoDeNegocio("O caractere de aquecimento não pode ser '.'.");

        Nome = nome;
        Alimento = alimento;
        TempoEmSegundos = tempoEmSegundos > 120 ? 120 : tempoEmSegundos;
        Potencia = potencia;
        CaractereAquecimento = caractereAquecimento;
        Instrucoes = instrucoes ?? string.Empty;
    }
}