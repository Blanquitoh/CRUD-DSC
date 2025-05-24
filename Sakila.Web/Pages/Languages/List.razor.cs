using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Responses;
using Sakila.Contracts.Services;

namespace Sakila.Web.Pages.Languages;

public partial class List
{
    [Inject] public ILanguageService LanguageService { get; set; } = null!;

    private LanguageGetAllResponse? _params;

    private bool _isDialogOpen;
    private bool _isDeleteDialogOpen;
    private LanguageGetByIdResponse _selectedLanguage = new();
    private string _actionName = "Add";

    protected override async Task OnInitializedAsync()
    {
        _params = await LanguageService.GetAllAsync();
    }

    private void ShowDialog(LanguageGetByIdResponse? language = null)
    {
        if (language == null)
        {
            _selectedLanguage = new LanguageGetByIdResponse();
            _actionName = "Add";
        }
        else
        {
            _selectedLanguage = language;
            _actionName = "Edit";
        }

        _isDialogOpen = true;
    }

    private void CloseDialog()
    {
        _isDialogOpen = false;
        _selectedLanguage = null;
    }

    private async Task SubmitLanguage()
    {
        if (!string.IsNullOrWhiteSpace(_selectedLanguage.Name))
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

            _params = await LanguageService.GetAllAsync();
        }

        _isDialogOpen = false;
    }

    private void ShowDeleteDialog(LanguageGetByIdResponse language)
    {
        _selectedLanguage = language;
        _isDeleteDialogOpen = true;
    }

    private void CloseDeleteDialog()
    {
        _isDeleteDialogOpen = false;
    }

    private async Task ConfirmDelete()
    {
        await LanguageService.DeleteAsync(_selectedLanguage.Id);
        _params = await LanguageService.GetAllAsync();
        _isDeleteDialogOpen = false;
    }
}