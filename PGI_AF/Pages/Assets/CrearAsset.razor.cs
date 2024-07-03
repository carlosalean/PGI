using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PGI_AF.Services;

namespace PGI_AF.Pages.Assets
{
    public partial class CrearAssetComponent : ComponentBase
    {
        [Inject]
        private AssetsService? AssetsService { get; set; }

        [Inject]
        private CasosService? CasosService { get; set; }

        [Inject]
        private MaquinasService? MaquinasService { get; set; }

        [Inject]
        private TipoAssetsService? TipoAssetsService { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        [Parameter]
        public int? CasoId { get; set; }

        public Asset Asset { get; set; } = new Asset();
        public List<TipoAsset>? tipoAsets { get; set; }

        public Caso? caso;

        public List<Maquina>? maquinas { get; set; }
        protected async Task HandleValidSubmit()
        {
            Asset.CasoID = CasoId!.Value;
            await AssetsService?.CreateAssetAsync(Asset)!;
            NavigationManager?.NavigateTo($"/assets/{CasoId.Value}");
        }


        protected override async Task OnInitializedAsync()
        {
            if (CasoId.HasValue)
            {
                caso = await CasosService?.GetCasoAsync(CasoId.Value)!;
                if (caso.Assets == null)
                {
                    var item = new List<Asset>();
                    caso.Assets = item;
                }
            }
            tipoAsets = await TipoAssetsService?.GetTipoAssetAsync()! ?? [];
            if (tipoAsets.Count > 0)
            {
                Asset.TipoAssetID = tipoAsets[0].ID;
            }
            maquinas = await MaquinasService?.GetMaquinasAsync()! ?? [];
            if (maquinas.Count > 0)
            {
                Asset.MaquinaID = maquinas[0].ID;
            }

        }


        protected void HandleFileSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file != null)
            {
                Asset.Ubicacion = file.Name;
            }
        }

        public void Cancel()
        {
            NavigationManager?.NavigateTo($"/assets/{CasoId}");
        }
    }

}
