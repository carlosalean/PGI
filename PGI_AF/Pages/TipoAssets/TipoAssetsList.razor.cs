using BackEnd_PGI.Model;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PGI_AF.Services;
using System.Text.Json;

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
        public bool isImportModalVisible = false;
        public IBrowserFile? uploadedFile;

        protected async Task<GridDataProviderResult<TipoAsset>> TipoAssetDataProvider(
                                GridDataProviderRequest<TipoAsset> request)
        {
            return await Task.FromResult(request.ApplyTo(tipoAsset!));
        }
        protected override async Task OnInitializedAsync()
        {
            tipoAsset = await TipoAssetsService?.GetTipoAssetAsync()!;
            await (_tipoAssetGrid?.RefreshDataAsync() ?? Task.CompletedTask);
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
        public async Task OnFileChange(InputFileChangeEventArgs e)
        {
            uploadedFile = e.File;
        }

        public async Task ImportAssets()
        {
            if (uploadedFile != null)
            {
                using var stream = uploadedFile.OpenReadStream();
                using var reader = new StreamReader(stream);
                var jsonContent = await reader.ReadToEndAsync();

                var assets = JsonSerializer.Deserialize<List<TipoAsset>>(jsonContent);

                if (assets != null)
                {
                    foreach (var asset in assets)
                    {
                        await TipoAssetsService?.CreateTipoAssetAsync(asset)!;
                    }

                    tipoAsset = await TipoAssetsService?.GetTipoAssetAsync()!; // Refresh list after import
                    await (_tipoAssetGrid?.RefreshDataAsync() ?? Task.CompletedTask);
                    StateHasChanged();
                }
            }

            HideImportModal(); // Close the modal after import
        }

        public void ShowImportModal()
        {
            isImportModalVisible = true;
        }

        public void HideImportModal()
        {
            isImportModalVisible = false;
        }


    }
}
