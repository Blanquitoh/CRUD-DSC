using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Languages.Commands;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Languages.Components;

public partial class LanguageCreateDialog
{
    [Parameter] public bool Visible { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnSuccess { get; set; }
    [Inject] public ILanguageService LanguageService { get; set; } = null!;

    public IApiResponse<object>? ApiResponse { get; set; }
    private LanguageCreateRequest Language { get; } = new();

    protected override void OnInitialized()
    {
        LanguageService.Initialize(Language);
    }

    private async Task SubmitAsync()
    {
        await LanguageService.CreateAsync(Language,
            async response =>
            {
                ApiResponse = response;
                await OnSuccess.InvokeAsync();
            });
    }
}