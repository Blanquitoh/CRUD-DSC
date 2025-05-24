using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Languages.Responses;
using Sakila.Contracts.Services;

namespace Sakila.Web.Pages.Languages;

public partial class List
{
    [Inject] public ILanguageService LanguageService { get; set; } = null!;

    private LanguageGetAllResponse? _params;

    protected override async Task OnInitializedAsync()
    {
        _params = await LanguageService.GetAllAsync();
    }
}