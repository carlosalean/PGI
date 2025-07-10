using System.Text.Json;
using System.Text.Json.Serialization;
using BackEnd_PGI.Model;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PGI_AF.Services;

namespace PGI_AF.Pages.TiposIOC
{
    public partial class TiposIOCListComponent : ComponentBase
    {
        [Inject]
        private TipoIOCsService? TiposIOCService { get; set; }

        public List<TipoIOC>? tiposIOC = [];
        public Grid<TipoIOC>? _tipoIOCGrid;
        public IBrowserFile? uploadedFile;

        // Variables para el manejo del modal de importación
        public bool isImportModalVisible = false;

        protected async Task<GridDataProviderResult<TipoIOC>> TipoIOCDataProvider(
                                GridDataProviderRequest<TipoIOC> request)
        {
            return await Task.FromResult(request.ApplyTo(tiposIOC!));
        }

        protected override async Task OnInitializedAsync()
        {
            tiposIOC = await TiposIOCService?.GetTipoIOCAsync()!;
            await (_tipoIOCGrid?.RefreshDataAsync() ?? Task.CompletedTask);
        }

        // Método para eliminar un tipo de IOC
        public async Task DeleteTipoIOC(int Id)
        {
            await TiposIOCService?.DeleteTipoIOCAsync(Id)!;
            tiposIOC = await TiposIOCService?.GetTipoIOCAsync()!; // Refresca la lista después de la eliminación
            StateHasChanged();
            await (_tipoIOCGrid?.RefreshDataAsync() ?? Task.CompletedTask);
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

        // Método para importar los tipos de IOC desde el archivo JSON

        public async Task ImportTiposIOC()
        {
            if (uploadedFile != null)
            {
                using var stream = uploadedFile.OpenReadStream();
                using var reader = new StreamReader(stream);
                var jsonContent = await reader.ReadToEndAsync();

                var options = new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() }
                };

                var importedTiposIOC = JsonSerializer.Deserialize<List<TipoIOC>>(jsonContent, options);

                if (importedTiposIOC != null)
                {
                    foreach (var tipoIOC in importedTiposIOC)
                    {
                        await TiposIOCService?.CreateTipoIOCAsync(tipoIOC)!;
                    }

                    tiposIOC = await TiposIOCService?.GetTipoIOCAsync()!; // Refresca la lista después de la importación
                    await (_tipoIOCGrid?.RefreshDataAsync() ?? Task.CompletedTask);
                    StateHasChanged();
                }
            }

            HideImportModal(); // Cierra el modal después de la importación
        }

    }
}

