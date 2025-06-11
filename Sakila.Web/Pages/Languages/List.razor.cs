using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Web.Abstractions;
using Sakila.Web.Pages.Languages.Components;

namespace Sakila.Web.Pages.Languages
{
    partial class List
    {
        private IApiResponse<LanguageGetAllResponse>? _getAllResponse;

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

        private async Task ShowCreateDialog()
        {
            var dialog = await DialogService.ShowAsync<LanguageCreateDialog>("Add Language");
            var result = (await dialog.Result)!;
            if (!result.Canceled)
            {
                await RefreshLanguages();
            }
        }

        private async Task ShowUpdateDialog(LanguageGetByIdResponse language)
        {
            var parameters = new DialogParameters { [nameof(LanguageUpdateDialog.Language)] = language };
            var dialog = await DialogService.ShowAsync<LanguageUpdateDialog>("Edit Language", parameters);
            var result = (await dialog.Result)!;
            if (!result.Canceled)
            {
                await RefreshLanguages();
            }
        }

        private async Task ShowDeleteDialog(LanguageGetByIdResponse language)
        {
            var parameters = new DialogParameters { [nameof(ConfirmDelete.Language)] = language };
            var dialog = await DialogService.ShowAsync<ConfirmDelete>("Delete Language", parameters);
            var result = (await dialog.Result)!;
            if (!result.Canceled)
            {
                await RefreshLanguages();
            }
        }
    }
}