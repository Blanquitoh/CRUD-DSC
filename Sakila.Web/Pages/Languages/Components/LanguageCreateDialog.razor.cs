using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Sakila.Contracts.Languages.Commands;
using Sakila.Web.Abstractions;
using Sakila.Web.Extensions;

namespace Sakila.Web.Pages.Languages.Components;

public partial class LanguageCreateDialog
{
    [Parameter] public bool Visible { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnSuccess { get; set; }
    [Inject] public ILanguageService LanguageService { get; set; } = null!;

    public IApiResponse<object>? ApiResponse { get; set; }
    private LanguageCreateRequest Language { get; set; } = new();
    private EditContext _editContext = null!;
    private ValidationMessageStore _messageStore = null!;

    protected override void OnInitialized()
    {
        _editContext = new EditContext(Language);
        _messageStore = new ValidationMessageStore(_editContext);
    }

    private async Task SubmitAsync()
    {
        ApiResponse = await LanguageService.CreateAsync(Language);

        if (_editContext.ApplyErrors(_messageStore, ApiResponse))
            await OnSuccess.InvokeAsync();
    }
}

