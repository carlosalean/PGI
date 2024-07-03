using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Components;
using PGI_AF.Services;

namespace PGI_AF.Pages.Casos
{
    public partial class EditCasoComponent : ComponentBase
    {
        [Inject]
        private CasosService? CasosService { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; } // Inyección de NavigationManager

        [Parameter]
        public int? CasoId { get; set; }

        public Caso Caso { get; set; } = new Caso();

        protected override async Task OnInitializedAsync()
        {
            if (CasoId.HasValue)
            {
                Caso = await CasosService?.GetCasoAsync(CasoId.Value)!;
                if (Caso == null)
                {
                    // Redireccionar si el caso no existe
                    NavigationManager?.NavigateTo("/casos");
                }
            }
            else
            {
                // Inicializar un nuevo caso si no se proporciona un ID
                Caso = new Caso();
            }
        }

        public async Task HandleValidSubmit()
        {
            if (Caso.ID == 0)
            {
                await CasosService?.CreateCasoAsync(Caso)!;
            }
            else
            {
                await CasosService?.UpdateCasoAsync(Caso.ID, Caso)!;
            }

            // Redirigir después de guardar
            NavigationManager?.NavigateTo("/casos");
        }
    }
}
