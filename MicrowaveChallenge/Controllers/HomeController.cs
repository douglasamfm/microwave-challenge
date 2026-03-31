using Microsoft.AspNetCore.Mvc;
using MicrowaveChallenge.Application.Interfaces;
using MicrowaveChallenge.Models;

namespace MicrowaveChallenge.Controllers;

public class HomeController : Controller
{
    private readonly IMicroondasService _microondasService;

    public HomeController(IMicroondasService microondasService)
    {
        _microondasService = microondasService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var estado = _microondasService.ObterEstado();

        var viewModel = new MicroondasViewModel
        {
            Status = estado.Status.ToString(),
            Progresso = estado.Progresso,
            TempoFormatado = estado.ObterTempoFormatado(),
            Programas = _microondasService.ObterProgramas()
        };

        if (TempData["Mensagem"] != null)
            viewModel.Mensagem = TempData["Mensagem"]!.ToString()!;

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Iniciar(MicroondasViewModel model)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(model.NomeProgramaSelecionado))
                _microondasService.IniciarPrograma(model.NomeProgramaSelecionado);
            else
                _microondasService.Iniciar(model.Tempo, model.Potencia);

            for (int i = 0; i < 5; i++)
                _microondasService.Processar();
        }
        catch (Exception ex)
        {
            TempData["Mensagem"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult InicioRapido()
    {
        try
        {
            _microondasService.InicioRapido();

            for (int i = 0; i < 5; i++)
                _microondasService.Processar();
        }
        catch (Exception ex)
        {
            TempData["Mensagem"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult PausarOuCancelar()
    {
        try
        {
            _microondasService.PausarOuCancelar();
        }
        catch (Exception ex)
        {
            TempData["Mensagem"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Processar()
    {
        try
        {
            _microondasService.Processar();
        }
        catch (Exception ex)
        {
            TempData["Mensagem"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}