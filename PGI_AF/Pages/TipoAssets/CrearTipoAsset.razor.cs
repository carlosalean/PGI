using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Components;
using PGI_AF.Services;

namespace PGI_AF.Pages.TipoAssets
{
    public partial class CrearTipoAssetComponent : ComponentBase
    {
        [Inject]
        public TipoAssetsService TipoAssetsService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Value { get; set; }

        public TipoAsset TipoAsset { get; set; } = new TipoAsset();

        protected async Task HandleValidSubmit()
        {

            await TipoAssetsService.CreateTipoAssetAsync(TipoAsset);
            NavigationManager.NavigateTo("/tipoAssets");
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo("/tipoAssets");
        }
    }
}
