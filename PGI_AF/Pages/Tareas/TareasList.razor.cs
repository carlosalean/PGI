using BackEnd_PGI.Model;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using PGI_AF.Services;

namespace PGI_AF.Pages.Tareas
{
    public partial class TareasListComponent : ComponentBase
    {
        [Inject]
        private TareasService TareasService { get; set; }

        [Inject]
        private AnalistasService AnalistasService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; } // Inyección de NavigationManager

        [Parameter]
        public int? CasoId { get; set; }

        public List<Tarea>? tareas = [];
       
        public Grid<Tarea>? _tareasGrid;
        public List<Analista>? analistas { get; set; }

        protected async Task<GridDataProviderResult<Tarea>> TareaDataProvider(
                                GridDataProviderRequest<Tarea> request)
        {
            return await Task.FromResult(request.ApplyTo(tareas));
        }
        protected override async Task OnInitializedAsync()
        {
            if (CasoId.HasValue)
            {
                tareas = await TareasService.GetTareaCasoAsync(CasoId.Value);
                await (_tareasGrid?.RefreshDataAsync() ?? Task.CompletedTask);
                analistas = await AnalistasService.GetAnalistaAsync() ?? new List<Analista>();
                if (!tareas.Any())
                {
                    NavigationManager.NavigateTo($"/tarea/create/{CasoId}");
                }
            }

        }

        public async Task DeleteTarea(int Id)
        {
            await TareasService.DeleteTareaAsync(Id);
            tareas = await TareasService.GetTareaCasoAsync(CasoId.Value); // Refresh list
            StateHasChanged(); // Re-render the component
            await _tareasGrid.RefreshDataAsync();

        }
        
        public void EditTarea(int TareaId)
        {
            NavigationManager.NavigateTo($"tareas/edit/{TareaId}");
        }
        public void CreateNewTask()
        {
            NavigationManager.NavigateTo($"/tarea/create/{CasoId}");
        }

    }
}
