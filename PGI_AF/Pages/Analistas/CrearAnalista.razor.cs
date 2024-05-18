using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Components;
using PGI_AF.Services;

namespace PGI_AF.Pages.Analistas
{
    public partial class CrearAnalistaComponent : ComponentBase
    {
        [Inject]
        public AnalistasService? AnalistasService { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        public Analista Analista { get; set; } = new Analista();

        protected async Task HandleValidSubmit()
        {           
            await AnalistasService!.CreateAnalistaAsync(Analista);
            NavigationManager?.NavigateTo("/analistas");
        }

        public void Cancel()
        {
            NavigationManager?.NavigateTo("/analistas");
        }
    }
}

