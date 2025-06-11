using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Languages.Components;

public partial class ConfirmDelete
{
    [Parameter] public bool Visible { get; set; }
    [Parameter] public LanguageGetByIdResponse Language { get; set; } = new();
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnSuccess { get; set; }
    [Inject] public ILanguageService LanguageService { get; set; } = null!;

    public IApiResponse<object>? ApiResponse { get; set; }

    private async Task ConfirmAsync()
    {
        await LanguageService.DeleteAsync(Language.Id,
            async response =>
            {
                ApiResponse = response;
                await OnSuccess.InvokeAsync();
            });
    }
}