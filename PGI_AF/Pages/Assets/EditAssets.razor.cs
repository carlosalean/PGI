using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PGI_AF.Services;

namespace PGI_AF.Pages.Assets
{
    public partial class EditAssetsComponent : ComponentBase
    {
        [Inject]
        private AssetsService? AssetsService { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        [Inject]
        private TipoAssetsService? TipoAssetsService { get; set; }
        [Inject]
        private MaquinasService? MaquinasService { get; set; }

        [Parameter]
        public int? AssetId { get; set; }

        public Asset Asset { get; set; } = new Asset();

        public List<TipoAsset>? tipoAsets { get; set; }

        public List<Maquina>? maquinas { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (AssetId.HasValue)
            {
                Asset = await AssetsService?.GetTareAsync(AssetId.Value)!;
                if (Asset == null)
                {
                    // Redireccionar si el caso no existe
                    NavigationManager?.NavigateTo("/casos");
                }

                tipoAsets = await TipoAssetsService?.GetTipoAssetAsync()! ?? [];
                maquinas = await MaquinasService?.GetMaquinasAsync()! ?? [];
            }
            else
            {
                // Inicializar un nuevo caso si no se proporciona un ID
                Asset = new Asset();
            }
        }

        public async Task HandleValidSubmit()
        {
            if (Asset.ID == 0)
            {
                await AssetsService?.CreateAssetAsync(Asset)!;
            }
            else
            {
                await AssetsService?.UpdateAssetAsync(Asset.ID, Asset)!;
            }

            // Redirigir después de guardar
            NavigationManager?.NavigateTo($"/assets/{Asset.CasoID}");
        }

        protected void HandleFileSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file != null)
            {
                Asset.Ubicacion = file.Name;
            }
        }
    }
}
