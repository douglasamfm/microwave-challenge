namespace MicrowaveChallenge.Models
{
    public class CadastroProgramaViewModel
    {
        public string Nome { get; set; } = string.Empty;
        public string Alimento { get; set; } = string.Empty;
        public int TempoEmSegundos { get; set; }
        public int Potencia { get; set; }
        public char CaractereAquecimento { get; set; }
        public string? Instrucoes { get; set; }
    }
}
