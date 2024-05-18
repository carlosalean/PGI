using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Components;
using PGI_AF.Services;

namespace PGI_AF.Pages.Maquinas
{
    public partial class EditarMaquinaComponent : ComponentBase
    {
        [Inject]
        private MaquinasService MaquinasService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; } // Inyección de NavigationManager

        [Parameter]
        public int? MaquinaId { get; set; }

        public Maquina Maquina { get; set; } = new Maquina();


        protected override async Task OnInitializedAsync()
        {
            if (MaquinaId.HasValue)
            {
                Maquina = await MaquinasService.GetMaquinaAsync(MaquinaId.Value);
                if (Maquina == null)
                {
                    // Redireccionar si el caso no existe
                    NavigationManager.NavigateTo("/casos");
                }

            }
            else
            {
                // Inicializar un nuevo caso si no se proporciona un ID
                Maquina = new Maquina();
            }
        }

        public async Task HandleValidSubmit()
        {
            if (Maquina.ID == 0)
            {
                await MaquinasService.CreateMaquinaAsync(Maquina);
            }
            else
            {
                await MaquinasService.UpdateMaquinaAsync(Maquina.ID, Maquina);
            }

            // Redirigir después de guardar
            NavigationManager.NavigateTo($"/maquinas/{Maquina.CasoID}");
        }
    }

}
