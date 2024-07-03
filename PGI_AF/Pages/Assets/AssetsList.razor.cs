using BackEnd_PGI.Model;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using PGI_AF.Services;

namespace PGI_AF.Pages.Assets
{
    public partial class AssetsListComponent : ComponentBase
    {
        [Inject]
        private AssetsService? AssetsService { get; set; }
        [Inject]
        private MaquinasService? MaquinasService { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; } // Inyección de NavigationManager

        [Parameter]
        public int? CasoId { get; set; }

        public List<Asset>? assets = [];

        public Grid<Asset>? _assetsGrid;
        public List<Maquina>? maquinas { get; set; }

        protected async Task<GridDataProviderResult<Asset>> AssetDataProvider(
                                GridDataProviderRequest<Asset> request)
        {
            return await Task.FromResult(request.ApplyTo(assets!));
        }
        protected override async Task OnInitializedAsync()
        {
            if (CasoId.HasValue)
            {
                assets = await AssetsService?.GetAssetCasoAsync(CasoId.Value)!;
                await (_assetsGrid?.RefreshDataAsync() ?? Task.CompletedTask);
                maquinas = await MaquinasService?.GetMaquinasAsync()! ?? [];
                if (!assets.Any())
                {
                    NavigationManager?.NavigateTo($"/assets/create/{CasoId}");
                }
            }
        }

        public async Task DeleteAsset(int Id)
        {
            await AssetsService?.DeleteAssetAsync(Id)!;
            assets = await AssetsService.GetAssetCasoAsync(CasoId!.Value); 
            StateHasChanged(); 
            await _assetsGrid?.RefreshDataAsync()!;

        }

        public void EditAssets(int AssetId)
        {
            NavigationManager?.NavigateTo($"assets/edit/{AssetId}");
        }

        public void CreateNewAsset()
        {
            NavigationManager?.NavigateTo($"/assets/create/{CasoId}");
        }

    }

}
