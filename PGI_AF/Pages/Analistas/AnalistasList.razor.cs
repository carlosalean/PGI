using BackEnd_PGI.Model;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using PGI_AF.Services;


namespace PGI_AF.Pages.Analistas
{
    public class AnalistasListComponent : ComponentBase
    {

        [Inject]
        private AnalistasService AnalistasService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; } // Inyección de NavigationManager

        public List<Analista>? analistas = [];

        public Grid<Analista>? _analistasGrid;

        protected async Task<GridDataProviderResult<Analista>> AnalistaDataProvider(
                                GridDataProviderRequest<Analista> request)
        {
            return await Task.FromResult(request.ApplyTo(analistas));
        }
        protected override async Task OnInitializedAsync()
        {
            analistas = await AnalistasService.GetAnalistaAsync();
            await (_analistasGrid?.RefreshDataAsync() ?? Task.CompletedTask);
            if (!analistas.Any())
            {
                NavigationManager.NavigateTo("/analistas/create");
            }

        }

        public async Task DeleteAnalista(int Id)
        {
            await AnalistasService.DeleteAnalistaAsync(Id);
            analistas = await AnalistasService.GetAnalistaAsync(); 
            StateHasChanged();
            await _analistasGrid?.RefreshDataAsync()!;
        }

        public void CreateNewAnalista()
        {
            NavigationManager.NavigateTo("/analistas/create");
        }

        public void EditAnalista(int Id)
        {
            NavigationManager.NavigateTo($"analistas/edit/{Id}");
        }

        public void ViewTask(int Id)
        {
            NavigationManager.NavigateTo($"tareas/{Id}");
        }       

    }
}
