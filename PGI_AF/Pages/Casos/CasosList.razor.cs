using BackEnd_PGI.Model;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using PGI_AF.Services;

namespace PGI_AF.Pages.Casos
{
    public partial class CasosListComponent : ComponentBase
    {
        [Inject]
        private CasosService CasosService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; } // Inyección de NavigationManager

        public List<Caso>? casos = [];

        public Grid<Caso>? _casosGrid;

        protected async Task<GridDataProviderResult<Caso>> CasoDataProvider(
                                GridDataProviderRequest<Caso> request)
        {
            return await Task.FromResult(request.ApplyTo(casos));
        }
        protected override async Task OnInitializedAsync()
        {
            casos = await CasosService.GetCasosAsync();
            await (_casosGrid?.RefreshDataAsync() ?? Task.CompletedTask);
            if (!casos.Any())
            {
                NavigationManager.NavigateTo("/casos/create");
            }
        }

        public async Task DeleteCaso(int casoId)
        {
            await CasosService.DeleteCasoAsync(casoId);
            casos = await CasosService.GetCasosAsync(); // Refresh list
            StateHasChanged(); // Re-render the component
            await (_casosGrid?.RefreshDataAsync() ?? Task.CompletedTask);            
        }

        public void CreateNewCase()
        {
            NavigationManager.NavigateTo("/casos/create");
        }

        public void EditCase(int caseId)
        {
            NavigationManager.NavigateTo($"casos/edit/{caseId}");
        }

        public void CreateTask(int caseId)
        {
            NavigationManager.NavigateTo($"tareas/{caseId}");
        }
        
        public void CreateAsset(int caseId)
        {
            NavigationManager.NavigateTo($"assets/{caseId}");
        }

        public void CreateMaquina(int caseId)
        {
            NavigationManager.NavigateTo($"maquinas/{caseId}");
        }
        

    }
}