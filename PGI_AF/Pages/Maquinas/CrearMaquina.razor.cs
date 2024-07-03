using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Components;
using PGI_AF.Services;

namespace PGI_AF.Pages.Maquinas
{
    public partial class CrearMaquinaComponent : ComponentBase
    {
        [Inject]
        private MaquinasService? MaquinasService { get; set; }

        [Inject]
        private CasosService? CasosService { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        [Parameter]
        public int? CasoId { get; set; }

        public Maquina Maquina { get; set; } = new Maquina();

        public Caso? caso;

        protected async Task HandleValidSubmit()
        {
            Maquina.CasoID = CasoId!.Value;
            await MaquinasService?.CreateMaquinaAsync(Maquina)!;

            NavigationManager?.NavigateTo($"/maquinas/{CasoId.Value}");
        }


        protected override async Task OnInitializedAsync()
        {
            if (CasoId.HasValue)
            {
                caso = await CasosService?.GetCasoAsync(CasoId.Value)!;
                if (caso.Maquinas == null)
                {
                    var item = new List<Maquina>();
                    caso.Maquinas = item;
                }
            }
        }



        public void Cancel()
        {
            NavigationManager?.NavigateTo($"/maquinas/{CasoId}");
        }
    }

}
