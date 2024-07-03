using BackEnd_PGI.Model;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using PGI_AF.Components;
using PGI_AF.Services;

namespace PGI_AF.Pages.TimeLine
{
    public class TimeLineComponent : ComponentBase
    {
        [Inject]
        private MaquinasService? MaquinasService { get; set; }

        [Inject]
        private TipoIOCsService? TipoIOCsService { get; set; }

        [Inject]
        private CasosService? CasosService { get; set; }

        [Inject]
        private AssetsService? AssetsService { get; set; }


        protected TipoIOCTabs? tipoIOCTabs;
        protected List<TipoIOC>? tipoIOCs;

        protected List<TreeNode>? treeNodes;

        public List<Caso>? casos = [];
        public List<Maquina>? maquinas = [];

        protected IEnumerable<NavItem>? navItems;
        protected Sidebar sidebar = default!;
        protected bool applyPurpleStyle = true;
        private void ToggleSidebarStyles() => applyPurpleStyle = true;
        private Caso? _caso;

        protected Caso? SelectedCaso
        {
            get => _caso;
            set
            {
                if (_caso != value)
                {
                    _caso = value;
                    if (_caso != null)
                    {
                        //LoadMaquinasCaso(_caso);
                    }
                }
            }
        }

        private int selectedCasoId;
        protected int SelectedCasoId
        {
            get => selectedCasoId;
            set
            {
                selectedCasoId = value;
                SelectedCaso = casos?.FirstOrDefault(c => c.ID == value);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            casos = await CasosService?.GetCasosAsync()! ?? new List<Caso>();

            if (casos.Count > 0)
            {
                SelectedCaso = casos[0];
                SelectedCasoId = casos[0].ID;
            }
            await LoadMaquinasCaso(SelectedCaso!);

            tipoIOCs = await TipoIOCsService?.GetTipoIOCAsync()!;


            if (casos.Count > 0)
                _caso = casos[0];
        }

        private ICollection<BackEnd_PGI.Model.IOC> List<T>()
        {
            throw new NotImplementedException();
        }

        protected async Task<SidebarDataProviderResult> SidebarDataProvider(SidebarDataProviderRequest request)
        {
            if (navItems is null)
                navItems = await GetNavItems();
            ToggleSidebarStyles();
            return await Task.FromResult(request.ApplyTo(navItems));
        }

        private async Task<IEnumerable<NavItem>> GetNavItems()
        {
            List<NavItem> navItems = new List<NavItem>();
            casos = await CasosService?.GetCasosAsync()! ?? new List<Caso>();
            if (casos.Count > 0)
            {
                Caso caso = casos[0];
                await LoadMaquinasCaso(caso);
            }
            return navItems;
        }

        private async Task LoadMaquinasCaso(Caso caso)
        {
            var maquinas = await MaquinasService?.GetMaquinasWithAssetsAsync(caso.ID)!;
            treeNodes = maquinas.Select(m => new TreeNode
            {
                ID = m.ID,
                Text = m.Nombre,
                Tipo = "Maquina",
                Icon = IconName.Fullscreen,
                Children = m.Assets?.Select(a => new TreeNode
                {
                    ID = a.ID,
                    Text = a.Nombre,
                    Tipo = "Asset",
                    Icon = (IconName)Enum.Parse(typeof(IconName), a.TipoAsset?.Icono!),
                }).ToList()
            }).ToList();
        }

        protected async Task HandleNodeClick(TreeNode node)
        {
            if (node != null && node.Tipo != "Asset")
            {
                Asset asset = await AssetsService?.GetAssetsWithIOCAsync(node.ID)!;

                foreach (BackEnd_PGI.Model.IOC child in asset?.IOCs!)
                {
                    if (child.TipoIOC != null)
                    {
                        tipoIOCTabs?.UpdateTabContent(child.TipoIocId, child?.Valor!);
                    }
                }
            }
        }

    }

}
