using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Components;
using PGI_AF.Services;

namespace PGI_AF.Pages.Analistas
{
    public partial class EditarAnalistaComponent : ComponentBase
    {
        [Inject]
        private AnalistasService AnalistasService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; } // Inyección de NavigationManager

        [Parameter]
        public int? AnalistaId { get; set; }

        public Analista Analista { get; set; } = new Analista();

        protected override async Task OnInitializedAsync()
        {
            if (AnalistaId.HasValue)
            {
                Analista = await AnalistasService.GetAnalistaAsync(AnalistaId.Value);
                if (Analista == null)
                {
                    // Redireccionar si el caso no existe
                    NavigationManager.NavigateTo("/analistas");
                }
            }
            else
            {
                // Inicializar un nuevo caso si no se proporciona un ID
                Analista = new Analista();
            }
        }

        public async Task HandleValidSubmit()
        {
            if (Analista.ID == 0)
            {
                await AnalistasService.CreateAnalistaAsync(Analista);
            }
            else
            {
                await AnalistasService.UpdateAnalistaAsync(Analista.ID, Analista);
            }

            // Redirigir después de guardar
            NavigationManager.NavigateTo("/analistas");
        }
    }
}
