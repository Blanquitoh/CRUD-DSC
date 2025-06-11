using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Languages.Commands;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Languages.Components;

public partial class LanguageUpdateDialog
{
    [Parameter] public bool Visible { get; set; }
    [Parameter] public LanguageGetByIdResponse Language { get; set; } = new();
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnSuccess { get; set; }
    [Inject] public ILanguageService LanguageService { get; set; } = null!;

    public IApiResponse<object>? ApiResponse { get; set; }
    private LanguageUpdateRequest Model { get; set; } = new();

    protected override void OnParametersSet()
    {
        Model = new LanguageUpdateRequest { Id = Language.Id, Name = Language.Name };
    }

    private async Task SubmitAsync()
    {
        ApiResponse = await LanguageService.UpdateAsync(Model);
        if (ApiResponse.IsSuccess)
        {
            await OnSuccess.InvokeAsync();
        }
    }
}

