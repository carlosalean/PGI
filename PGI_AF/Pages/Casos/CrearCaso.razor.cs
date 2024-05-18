using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Components;
using PGI_AF.Services;

namespace PGI_AF.Pages.Casos
{
    public partial class CrearCasoComponent : ComponentBase
    {
        [Inject]
        public CasosService CasosService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Caso Caso { get; set; } = new Caso();

        protected async Task HandleValidSubmit()
        {
            if (Caso.FechaInicio == default)
            {
                Caso.FechaInicio = DateTime.Today; // Establece la fecha de inicio por defecto si no se proporciona
            }

            await CasosService.CreateCasoAsync(Caso);
            NavigationManager.NavigateTo("/casos");
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo("/casos");
        }
    }
}