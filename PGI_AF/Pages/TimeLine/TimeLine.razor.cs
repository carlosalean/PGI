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

        public LineChart lineChart = default!;
        public LineChartOptions lineChartOptions = default!;
        public ChartData chartData = default!;

        public int datasetsCount;
        public int labelsCount;

        public Random random = new();

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

        protected override void OnInitialized()
        {
            chartData = new ChartData { Labels = GetDefaultDataLabels(6), Datasets = GetDefaultDataSets(3) };
            lineChartOptions = new()
            {
                IndexAxis = "x",
                Interaction = new Interaction { Mode = InteractionMode.Index, Intersect = false },
                Responsive = true,
            };
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await lineChart.InitializeAsync(chartData, lineChartOptions);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        protected async Task RandomizeAsync()
        {
            if (chartData is null || chartData.Datasets is null || !chartData.Datasets.Any()) return;

            var newDatasets = new List<IChartDataset>();

            foreach (var dataset in chartData.Datasets)
            {
                if (dataset is LineChartDataset lineChartDataset
                    && lineChartDataset is not null
                    && lineChartDataset.Data is not null)
                {
                    var count = lineChartDataset.Data.Count;

                    var newData = new List<double?>();
                    for (var i = 0; i < count; i++)
                    {
                        newData.Add(random.Next(200));
                    }

                    lineChartDataset.Data = newData;
                    newDatasets.Add(lineChartDataset);
                }
            }

            chartData.Datasets = newDatasets;
            var option = new ChartOptions();
            await lineChart.UpdateAsync(chartData, option);
        }

        protected async Task AddDatasetAsync()
        {
            if (chartData is null || chartData.Datasets is null) return;

            var chartDataset = GetRandomLineChartDataset();
            chartData = await lineChart.AddDatasetAsync(chartData, chartDataset, lineChartOptions);
        }

        protected async Task AddDataAsync()
        {
            if (chartData is null || chartData.Datasets is null)
                return;

            var data = new List<IChartDatasetData>();
            foreach (var dataset in chartData.Datasets)
            {
                if (dataset is LineChartDataset lineChartDataset)
                    data.Add(new LineChartDatasetData(lineChartDataset.Label, random.Next(200)));
            }

            chartData = await lineChart.AddDataAsync(chartData, GetNextDataLabel(), data);
        }

        protected async Task ShowHorizontalLineChartAsync()
        {
            lineChartOptions.IndexAxis = "y";
            await lineChart.UpdateAsync(chartData, lineChartOptions);
        }

        protected async Task ShowVerticalLineChartAsync()
        {
            lineChartOptions.IndexAxis = "x";
            await lineChart.UpdateAsync(chartData, lineChartOptions);
        }

        #region Data Preparation

        protected List<IChartDataset> GetDefaultDataSets(int numberOfDatasets)
        {
            var datasets = new List<IChartDataset>();

            for (var index = 0; index < numberOfDatasets; index++)
            {
                datasets.Add(GetRandomLineChartDataset());
            }

            return datasets;
        }

        protected LineChartDataset GetRandomLineChartDataset()
        {
            var c = ColorUtility.CategoricalTwelveColors[datasetsCount].ToColor();

            datasetsCount += 1;

            return new LineChartDataset
            {
                Label = $"Máquina {datasetsCount}",
                Data = GetRandomData(),
                BackgroundColor = c.ToRgbaString(),
                BorderColor = c.ToRgbString(),
                PointRadius = new List<double> { 5 },
                PointHoverRadius = new List<double> { 8 },
            };
        }

        protected List<double?> GetRandomData()
        {
            var data = new List<double?>();
            for (var index = 0; index < labelsCount; index++)
            {
                data.Add(random.Next(200));
            }

            return data;
        }

        protected List<string> GetDefaultDataLabels(int numberOfLabels)
        {
            var labels = new List<string>();
            for (var index = 0; index < numberOfLabels; index++)
            {
                labels.Add(GetNextDataLabel());
            }

            return labels;
        }

        protected string GetNextDataLabel()
        {
            labelsCount += 1;
            return $"Dia {labelsCount}";
        }

        #endregion Data Preparation

    }

}
