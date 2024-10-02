using TestandoConhecimento.API.Entities;

namespace TestandoConhecimento.API.Percistence
{
    public class ConhecimentoDbContext
    {  
        public List<Conheciemento> Conheciementos { get; set; }

        public ConhecimentoDbContext()
        {
            Conheciementos = new List<Conheciemento>();
        }
    }
}
