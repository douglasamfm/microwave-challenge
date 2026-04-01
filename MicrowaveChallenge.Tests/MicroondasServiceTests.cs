using MicrowaveChallenge.Application.Services;
using MicrowaveChallenge.Domain.Entities;
using MicrowaveChallenge.Domain.Enums;
using MicrowaveChallenge.Domain.Exceptions;
using Xunit;

namespace MicrowaveChallenge.Tests;

public class MicroondasServiceTests
{
    [Fact]
    public void Deve_Iniciar_Com_Potencia_Padrao_Quando_Nao_Informada()
    {
        var service = new MicroondasService();

        service.Iniciar(30, null);

        var estado = service.ObterEstado();

        Assert.Equal(10, estado.Potencia);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Quando_Tempo_For_Invalido()
    {
        var service = new MicroondasService();

        Assert.Throws<ExcecaoDeNegocio>(() => service.Iniciar(0, 5));
    }

    [Fact]
    public void Deve_Lancar_Excecao_Quando_Potencia_For_Invalida()
    {
        var service = new MicroondasService();

        Assert.Throws<ExcecaoDeNegocio>(() => service.Iniciar(10, 11));
    }

    [Fact]
    public void Deve_Iniciar_Inicio_Rapido_Corretamente()
    {
        var service = new MicroondasService();

        service.InicioRapido();
        var estado = service.ObterEstado();

        Assert.Equal(30, estado.TempoTotal);
        Assert.Equal(10, estado.Potencia);
    }

    [Fact]
    public void Nao_Deve_Permitir_Caractere_Repetido_Em_Programa_Customizado()
    {
        var service = new MicroondasService();

        var programa = new ProgramaAquecimento("Meu Programa", "Teste", 20, 5, '*', "Teste");

        Assert.Throws<ExcecaoDeNegocio>(() => service.AdicionarProgramaCustomizado(programa));
    }

    [Fact]
    public void Deve_Pausar_E_Retomar_Aquecimento()
    {
        var service = new MicroondasService();

        service.Iniciar(20, 5);
        service.PausarOuCancelar();
        service.Iniciar(20, 5);

        var estado = service.ObterEstado();

        Assert.Equal(StatusMicroondas.EmExecucao, estado.Status);
    }

    [Fact]
    public void Deve_Acrescentar_30_Segundos_Quando_Ja_Estiver_Em_Execucao()
    {
        var service = new MicroondasService();

        service.Iniciar(20, 5);
        service.Iniciar(20, 5);

        var estado = service.ObterEstado();

        Assert.Equal(50, estado.TempoRestante);
    }
}