using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Components;
using PGI_AF.Services;

namespace PGI_AF.Pages.TipoAssets
{
    public partial class EditTipoAssetComponent : ComponentBase
    {
        [Inject]
        private TipoAssetsService TipoAssetsService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; } // Inyección de NavigationManager

        [Parameter]
        public int? TipoAssetId { get; set; }

        public TipoAsset TipoAsset { get; set; } = new TipoAsset();

        protected override async Task OnInitializedAsync()
        {
            if (TipoAssetId.HasValue)
            {
                TipoAsset = await TipoAssetsService.GetTipoAssetAsync(TipoAssetId.Value);
                if (TipoAsset == null)
                {
                    // Redireccionar si el tipoAsset no existe
                    NavigationManager.NavigateTo("/tipoAssets");
                }
            }
            else
            {
                // Inicializar un nuevo tipoAsset si no se proporciona un ID
                TipoAsset = new TipoAsset();
            }
        }

        public async Task HandleValidSubmit()
        {
            if (TipoAsset.ID == 0)
            {
                await TipoAssetsService.CreateTipoAssetAsync(TipoAsset);
            }
            else
            {
                await TipoAssetsService.UpdateTipoAssetAsync(TipoAsset.ID, TipoAsset);
            }

            // Redirigir después de guardar
            NavigationManager.NavigateTo("/tipoAssets");
        }
    }

}
