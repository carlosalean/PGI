using BackEnd_PGI.Model;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using PGI_AF.Services;
using PGI_AF.Components;

namespace PGI_AF.Pages.Analisis
{
    public class AnalisisListComponent : ComponentBase
    {
        [Inject]
        private MaquinasService MaquinasService { get; set; }

        [Inject]
        private TipoIOCsService TipoIOCsService { get; set; }

        [Inject]
        private CasosService CasosService { get; set; }

        [Inject]
        private AssetsService AssetsService { get; set; }

        [Inject]
        private TipoAssetsService TipoAssetsService { get; set; }

        protected TipoIOCTabs tipoIOCTabs;
        protected List<TipoIOC> tipoIOCs;

        protected List<TreeNode> treeNodes;

        public List<Caso>? casos = [];
        public List<Maquina>? maquinas = [];

        protected IEnumerable<NavItem>? navItems;
        protected Sidebar sidebar = default!;
        protected bool applyPurpleStyle = true;
        private void ToggleSidebarStyles() => applyPurpleStyle = true;
        private Caso _caso;

        protected Caso? SelectedCaso
        {
            get => _caso;
            set { _caso = value; LoadMaquinasCaso(_caso!); }
        }
        protected override async Task OnInitializedAsync()
        {
            tipoIOCs = await TipoIOCsService.GetTipoIOCAsync();
            var maquinas = await MaquinasService.GetMaquinasWithAssetsAsync();
            treeNodes = maquinas.Select(m => new TreeNode
            {
                ID  = m.ID,
                Text = m.Nombre,
                Tipo = "Maquina",
                Icon = IconName.Fullscreen,
                Children = m.Assets.Select(a => new TreeNode
                {
                    ID = a.ID,
                    Text = a.Nombre,
                    Tipo = "Asset",
                    Icon = (IconName)Enum.Parse(typeof(IconName), a.TipoAsset.Icono),
                }).ToList()
            }).ToList();

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
            casos = await CasosService.GetCasosAsync() ?? new List<Caso>();
            if (casos.Count > 0)
            {
                Caso caso = casos[0];
                navItems = await LoadMaquinasCaso(caso);
            }
            return navItems;
        }

        private async Task<List<NavItem>> LoadMaquinasCaso(Caso caso)
        {
            maquinas = await MaquinasService.GetMaquinasCasoAsync(caso.ID);
            List<NavItem> navItems = new List<NavItem>();
            Int32 id = 1;
            foreach (var maquina in maquinas)
            {
                NavItem item = AddMenuItem(id.ToString(), IconName.WindowFullscreen, maquina.Nombre, "");
                navItems.Add(item);
                var assets = await AssetsService.GetAssetMaquinaAsync(maquina.ID);
                var paternId = id.ToString();
                if (assets.Count == 0) id++;
                foreach (var asset in assets)
                {
                    TipoAsset tipoAsset = await TipoAssetsService.GetTipoAssetAsync(asset.ID);
                    var IconAsset = (IconName)Enum.Parse(typeof(IconName), tipoAsset.Icono!);
                    NavItem item2 = AddMenuItem(id.ToString(), IconAsset, asset.Nombre, paternId);
                    navItems.Add(item2);
                    id++;
                }
            }
            return navItems;
        }

        private NavItem AddMenuItem(string id, IconName iconName, string text, string parentId)
        {
            NavItem menuItem;
            if (!string.IsNullOrEmpty(parentId))
            {
                menuItem = new NavItem { Id = id, IconName = iconName, Text = text, ParentId = parentId, Href = "#maliciousfile" };
            }
            else
            {
                menuItem = new NavItem { Id = id, IconName = iconName, Text = text };
            }
            return menuItem;
        }

        protected async Task HandleNodeClick(TreeNode node)
        {
            if (node != null && node.Tipo != "Asset") {
                Asset asset = await AssetsService.GetAssetsWithIOCAsync(node.ID);

                foreach (BackEnd_PGI.Model.IOC child in asset.IOCs)
                {
                    if (child.TipoIOC != null)
                    {
                        tipoIOCTabs.UpdateTabContent(child.TipoIocId, child.Valor);
                    }
                }
            }
        }

    }
}
