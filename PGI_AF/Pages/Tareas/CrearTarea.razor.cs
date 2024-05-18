using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Components;
using PGI_AF.Services;

namespace PGI_AF.Pages.Tareas
{
    public partial class CrearTareaComponent : ComponentBase
    {
        [Inject]
        private TareasService TareasService { get; set; }

        [Inject]
        private CasosService CasosService { get; set; }

        [Inject]
        private AnalistasService AnalistasService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int? CasoId { get; set; }

        public Tarea Tarea { get; set; } = new Tarea();
        public List<Analista>? analistas { get; set; }

        public Caso? caso;

        protected async Task HandleValidSubmit()
        {
            Tarea.CasoID = CasoId.Value;
            if (Tarea.FechaInicio == default)
            {
                Tarea.FechaInicio = DateTime.Today; // Establece la fecha de inicio por defecto si no se proporciona                
            }            
            await TareasService.CreateTareaAsync(Tarea);

            NavigationManager.NavigateTo($"/tareas/{CasoId.Value}");
        }


        protected override async Task OnInitializedAsync()
        {
            if (CasoId.HasValue)
            {
                caso = await CasosService.GetCasoAsync(CasoId.Value);
                if (caso.Tareas == null)
                {
                    var item = new List<Tarea>();
                    caso.Tareas = item;
                }
            }
            analistas = await AnalistasService.GetAnalistaAsync() ?? new List<Analista>();
            if (analistas.Count > 0)
            {
                Tarea.AnalistaID = analistas[0].ID;
            }
            Tarea.FechaInicio = DateTime.Now;
            Tarea.FechaFin = DateTime.Now;
            Tarea.DeadLine = DateTime.Now;
        }



        public void Cancel()
        {
            NavigationManager.NavigateTo($"/tareas/{CasoId}");
        }
    }
}
