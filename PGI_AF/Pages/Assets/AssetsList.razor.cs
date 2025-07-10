using System.Text.Json;
using BackEnd_PGI.Model;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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

        // Variables para el manejo del modal de importación
        public bool isImportModalVisible = false;
        public IBrowserFile? uploadedFile;

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

        // Métodos para mostrar y ocultar el modal
        public void ShowImportModal()
        {
            isImportModalVisible = true;
        }

        public void HideImportModal()
        {
            isImportModalVisible = false;
        }

        // Método para manejar la selección del archivo
        public async Task OnFileChange(InputFileChangeEventArgs e)
        {
            uploadedFile = e.File;
        }

        // Método para importar los assets desde el archivo JSON
        public async Task ImportAssets()
        {
            if (uploadedFile != null)
            {
                using var stream = uploadedFile.OpenReadStream();
                using var reader = new StreamReader(stream);
                var jsonContent = await reader.ReadToEndAsync();

                var importedAssets = JsonSerializer.Deserialize<List<Asset>>(jsonContent);

                if (importedAssets != null)
                {
                    foreach (var asset in importedAssets)
                    {
                        asset.CasoID = CasoId ?? 0; // Asigna el CasoID actual
                        await AssetsService?.CreateAssetAsync(asset)!;
                    }

                    // Refresca la lista después de la importación
                    assets = await AssetsService?.GetAssetCasoAsync(CasoId!.Value)!;
                    await (_assetsGrid?.RefreshDataAsync() ?? Task.CompletedTask);
                    StateHasChanged();
                }
            }

            HideImportModal(); // Cierra el modal después de la importación
        }
    }
}
