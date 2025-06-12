using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Web.Abstractions;
using Sakila.Web.Pages.Languages.Components;
using Sakila.Web.Extensions;

namespace Sakila.Web.Pages.Languages;

partial class List
{
    private ISakilaApiResponse<LanguageGetAllResponse>? _getAllResponse;

    [Inject] public ILanguageService LanguageService { get; set; } = null!;
    [Inject] public IDialogService DialogService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await RefreshLanguages();
    }

    private async Task RefreshLanguages()
    {
        await LanguageService.GetAllAsync(r => Task.FromResult(_getAllResponse = r));
    }

    private Task ShowCreateDialog()
    {
        return DialogService.ShowDialogAsync<LanguageCreateDialog>(
            "Add Language",
            onSuccess: RefreshLanguages);
    }

    private Task ShowUpdateDialog(LanguageGetByIdResponse language)
    {
        var parameters = new DialogParameters { [nameof(LanguageUpdateDialog.Language)] = language };
        return DialogService.ShowDialogAsync<LanguageUpdateDialog>(
            "Edit Language",
            parameters,
            RefreshLanguages);
    }

    private Task ShowDeleteDialog(LanguageGetByIdResponse language)
    {
        var parameters = new DialogParameters { [nameof(ConfirmDelete.Language)] = language };
        return DialogService.ShowDialogAsync<ConfirmDelete>(
            "Delete Language",
            parameters,
            RefreshLanguages);
    }
}