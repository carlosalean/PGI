using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Components;
using PGI_AF.Services;

namespace PGI_AF.Pages.Tareas
{
    public partial class EditTareaComponent : ComponentBase
    {
        [Inject]
        private TareasService TareasService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; } // Inyección de NavigationManager
        
        [Inject]
        private AnalistasService AnalistasService { get; set; }

        [Parameter]
        public int? TareaId { get; set; }

        public Tarea Tarea { get; set; } = new Tarea();

        public List<Analista>? analistas { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (TareaId.HasValue)
            {
                Tarea = await TareasService.GetTareAsync(TareaId.Value);
                if (Tarea == null)
                {
                    // Redireccionar si el caso no existe
                    NavigationManager.NavigateTo("/casos");
                }

                analistas = await AnalistasService.GetAnalistaAsync() ?? new List<Analista>();
            }
            else
            {
                // Inicializar un nuevo caso si no se proporciona un ID
                Tarea = new Tarea();
            }
        }

        public async Task HandleValidSubmit()
        {
            if (Tarea.ID == 0)
            {
                await TareasService.CreateTareaAsync(Tarea);
            }
            else
            {
                await TareasService.UpdateTareaAsync(Tarea.ID, Tarea);
            }

            // Redirigir después de guardar
            NavigationManager.NavigateTo($"/tareas/{Tarea.CasoID}");
        }
    }
}
