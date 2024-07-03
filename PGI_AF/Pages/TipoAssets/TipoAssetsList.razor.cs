using BackEnd_PGI.Model;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using PGI_AF.Services;

namespace PGI_AF.Pages.TipoAssets
{
    public partial class TipoAssetsListComponent : ComponentBase
    {
        [Inject]
        private TipoAssetsService? TipoAssetsService { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; } // Inyección de NavigationManager

        public List<TipoAsset>? tipoAsset = [];

        public Grid<TipoAsset>? _tipoAssetGrid;

        protected async Task<GridDataProviderResult<TipoAsset>> TipoAssetDataProvider(
                                GridDataProviderRequest<TipoAsset> request)
        {
            return await Task.FromResult(request.ApplyTo(tipoAsset!));
        }
        protected override async Task OnInitializedAsync()
        {
            tipoAsset = await TipoAssetsService?.GetTipoAssetAsync()!;
            await (_tipoAssetGrid?.RefreshDataAsync() ?? Task.CompletedTask);
            if (!tipoAsset.Any())
            {
                NavigationManager?.NavigateTo("/tipoAssets/create");
            }
        }

        public async Task DeleteTipoAsset(int Id)
        {
            await TipoAssetsService?.DeleteTipoAssetAsync(Id)!;
            tipoAsset = await TipoAssetsService.GetTipoAssetAsync(); // Refresh list
            StateHasChanged(); // Re-render the component
            await (_tipoAssetGrid?.RefreshDataAsync() ?? Task.CompletedTask);
           
        }

        public void CreateNewTipoAsset()
        {
            NavigationManager?.NavigateTo("/tipoAssets/create");
        }

        public void EditTipoAsset(int Id)
        {
            NavigationManager?.NavigateTo($"tipoAssets/edit/{Id}");
        }



    }
}
