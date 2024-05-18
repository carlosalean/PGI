using BackEnd_PGI.Model;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using PGI_AF.Services;

namespace PGI_AF.Pages.Maquinas
{

    public partial class MaquinasListComponent : ComponentBase
    {
        [Inject]
        private MaquinasService MaquinasService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; } // Inyección de NavigationManager

        [Parameter]
        public int? CasoId { get; set; }


        public List<Maquina>? maquinas = [];

        public Grid<Maquina>? _maquinasGrid;

        protected async Task<GridDataProviderResult<Maquina>> MaquinaDataProvider(
                                GridDataProviderRequest<Maquina> request)
        {
            return await Task.FromResult(request.ApplyTo(maquinas));
        }
        protected override async Task OnInitializedAsync()
        {
            if (CasoId.HasValue)
            {
                maquinas = await MaquinasService.GetMaquinasCasoAsync(CasoId.Value);
                await (_maquinasGrid?.RefreshDataAsync() ?? Task.CompletedTask);
                if (!maquinas.Any())
                {
                    NavigationManager.NavigateTo($"/maquinas/create/{CasoId}");
                }
            }
        }

        public async Task DeleteMaquina(int maquinaId)
        {
            await MaquinasService.DeleteMaquinaAsync(maquinaId);
            maquinas = await MaquinasService.GetMaquinasAsync(); // Refresh list
            StateHasChanged(); // Re-render the component
            await (_maquinasGrid?.RefreshDataAsync() ?? Task.CompletedTask);
           
        }

        public void CreateNewMaquina()
        {
            NavigationManager.NavigateTo($"/maquinas/create/{CasoId}");
        }

        public void EditMaquina(int caseId)
        {
            NavigationManager.NavigateTo($"maquinas/edit/{caseId}");
        }

        public void CreateAsset(int? caseId)
        {
            NavigationManager.NavigateTo($"assets/{caseId}");
        }

    }
}
