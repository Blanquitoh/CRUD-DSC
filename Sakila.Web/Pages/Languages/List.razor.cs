using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Responses;
using Sakila.Contracts.Services;
using Sakila.Web.Common;

namespace Sakila.Web.Pages.Languages;

public partial class List
{
    private bool _isDeleteDialogOpen;
    private bool _isDialogOpen;
    private LanguageGetAllResponse? _params;
    private LanguageGetByIdResponse _selectedLanguage = new();
    [Inject] public ILanguageService LanguageService { get; set; } = null!;
    private Dictionary<string, string[]> Errors { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        _params = await LanguageService.GetAllAsync();
    }

    private void ShowDialog(LanguageGetByIdResponse? language = null)
    {
        if (language is null)
            _selectedLanguage = new LanguageGetByIdResponse();
        else
            _selectedLanguage = new LanguageGetByIdResponse
            {
                Id = language.Id,
                Name = language.Name
            };

        Errors = new Dictionary<string, string[]>();
        _isDialogOpen = true;
    }

    private void CloseDialog()
    {
        _isDialogOpen = false;
        _selectedLanguage = new LanguageGetByIdResponse();
        Errors = new Dictionary<string, string[]>();
    }

    private async Task SubmitLanguage()
    {
        try
        {
            if (_selectedLanguage.Id == 0)
            {
                var request = new LanguageCreateRequest { Name = _selectedLanguage.Name };
                await LanguageService.CreateAsync(request);
            }
            else
            {
                var request = new LanguageUpdateRequest { Id = _selectedLanguage.Id, Name = _selectedLanguage.Name };
                await LanguageService.UpdateAsync(request);
            }

            await RefreshLanguages();
            CloseDialog();
        }
        catch (ApiValidationException exception)
        {
            Errors = exception.Errors;
        }
    }

    private async Task RefreshLanguages()
    {
        _params = await LanguageService.GetAllAsync();
    }

    private void ShowDeleteDialog(LanguageGetByIdResponse language)
    {
        _selectedLanguage = language;
        Errors = new Dictionary<string, string[]>();
        _isDeleteDialogOpen = true;
    }

    private void CloseDeleteDialog()
    {
        _isDeleteDialogOpen = false;
        Errors = new Dictionary<string, string[]>();
    }

    private async Task ConfirmDelete()
    {
        try
        {
            await LanguageService.DeleteAsync(_selectedLanguage.Id);
            await RefreshLanguages();
            CloseDeleteDialog();
        }
        catch (ApiValidationException exception)
        {
            Errors = exception.Errors;
        }
    }
}