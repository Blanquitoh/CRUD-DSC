using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Web.Abstractions;
using Sakila.Web.Extensions;

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
    private EditContext _editContext = null!;
    private ValidationMessageStore _messageStore = null!;

    protected override void OnParametersSet()
    {
        Model = new LanguageUpdateRequest { Id = Language.Id, Name = Language.Name };
        _editContext = new EditContext(Model);
        _messageStore = new ValidationMessageStore(_editContext);
    }

    private async Task SubmitAsync()
    {
        ApiResponse = await LanguageService.UpdateAsync(Model);

        if (_editContext.ApplyErrors(_messageStore, ApiResponse))
            await OnSuccess.InvokeAsync();
    }
}

