namespace ReceitasWebApi.Domain.Entities
{
    public class Receita : Entity
    {
        public string Titulo { get; set; }
        public string ImagemUrl { get; set; }
        public string Ingredientes { get; set; }
        public string Descricao { get; set; }
        public string MetodoDePreparo { get; set; }

        public Receita(string titulo)
        {
            this.Titulo = titulo;

        }

        public Receita ()
        {

        }

    
    }
}