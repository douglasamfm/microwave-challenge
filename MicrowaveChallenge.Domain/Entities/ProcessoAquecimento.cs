using MicrowaveChallenge.Domain.Enums;
using MicrowaveChallenge.Domain.Exceptions;

namespace MicrowaveChallenge.Domain.Entities;

public class ProcessoAquecimento
{
    public int TempoTotal { get; private set; }
    public int TempoRestante { get; private set; }
    public int Potencia { get; private set; }
    public char CaractereAquecimento { get; private set; } = '.';
    public string Progresso { get; private set; } = string.Empty;
    public StatusMicroondas Status { get; private set; } = StatusMicroondas.Inativo;
    public bool EhProgramaPreDefinido { get; private set; }

    public bool EstaEmExecucao => Status == StatusMicroondas.EmExecucao;
    public bool EstaPausado => Status == StatusMicroondas.Pausado;
    public bool EstaFinalizado => Status == StatusMicroondas.Finalizado;
    public bool EstaInativo => Status == StatusMicroondas.Inativo;
    public bool PodeAcrescentarTempo => EstaEmExecucao && !EhProgramaPreDefinido;

    public void Iniciar(int tempoEmSegundos, int? potencia)
    {
        if (tempoEmSegundos < 1 || tempoEmSegundos > 120)
            throw new ExcecaoDeNegocio("Tempo inválido. Informe um valor entre 1 e 120 segundos.");

        int potenciaFinal = potencia ?? 10;

        if (potenciaFinal < 1 || potenciaFinal > 10)
            throw new ExcecaoDeNegocio("Potência inválida. Informe um valor entre 1 e 10.");

        TempoTotal = tempoEmSegundos;
        TempoRestante = tempoEmSegundos;
        Potencia = potenciaFinal;
        CaractereAquecimento = '.';
        Progresso = string.Empty;
        Status = StatusMicroondas.EmExecucao;
        EhProgramaPreDefinido = false;
    }

    public void IniciarProgramaPreDefinido(ProgramaAquecimento programa)
    {
        TempoTotal = programa.TempoEmSegundos;
        TempoRestante = programa.TempoEmSegundos;
        Potencia = programa.Potencia;
        CaractereAquecimento = programa.CaractereAquecimento;
        Progresso = string.Empty;
        Status = StatusMicroondas.EmExecucao;
        EhProgramaPreDefinido = true;
    }

    public void InicioRapido()
    {
        Iniciar(30, 10);
    }

    public void AcrescentarTempo()
    {
        if (EhProgramaPreDefinido)
            throw new ExcecaoDeNegocio("Não é permitido acrescentar tempo em programa pré-definido.");

        if (Status != StatusMicroondas.EmExecucao)
            return;

        TempoRestante += 30;
        TempoTotal += 30;

        if (TempoRestante > 120)
            TempoRestante = 120;

        if (TempoTotal > 120)
            TempoTotal = 120;
    }

    public void PausarOuCancelar()
    {
        if (Status == StatusMicroondas.EmExecucao)
        {
            Status = StatusMicroondas.Pausado;
            return;
        }

        if (Status == StatusMicroondas.Pausado)
        {
            Cancelar();
            return;
        }

        Limpar();
    }

    public void Retomar()
    {
        if (Status == StatusMicroondas.Pausado)
            Status = StatusMicroondas.EmExecucao;
    }

    public void ProcessarSegundo()
    {
        if (Status != StatusMicroondas.EmExecucao)
            return;

        if (TempoRestante <= 0)
            return;

        Progresso += new string(CaractereAquecimento, Potencia) + " ";
        TempoRestante--;

        if (TempoRestante == 0)
        {
            Status = StatusMicroondas.Finalizado;
            Progresso += " Aquecimento concluído";
        }
    }

    public void Cancelar()
    {
        Limpar();
        Status = StatusMicroondas.Cancelado;
    }

    public void Limpar()
    {
        TempoTotal = 0;
        TempoRestante = 0;
        Potencia = 0;
        Progresso = string.Empty;
        Status = StatusMicroondas.Inativo;
        EhProgramaPreDefinido = false;
        CaractereAquecimento = '.';
    }

    public string ObterTempoFormatado()
    {
        int minutos = TempoRestante / 60;
        int segundos = TempoRestante % 60;
        return $"{minutos}:{segundos:D2}";
    }
}