using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Web.Abstractions;
using Sakila.Web.Extensions;

namespace Sakila.Web.Pages.Countries.Components;

public partial class CountryUpdateDialog
{
    [Parameter] public bool Visible { get; set; }
    [Parameter] public CountryGetByIdResponse Country { get; set; } = new();
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnSuccess { get; set; }
    [Inject] public ICountryService CountryService { get; set; } = null!;

    public IApiResponse<object>? ApiResponse { get; set; }
    private CountryUpdateRequest Model { get; set; } = new();
    private EditContext _editContext = null!;
    private ValidationMessageStore _messageStore = null!;

    protected override void OnParametersSet()
    {
        Model = new CountryUpdateRequest { Id = Country.Id, Name = Country.Name };
        _editContext = new EditContext(Model);
        _messageStore = new ValidationMessageStore(_editContext);
    }

    private async Task SubmitAsync()
    {
        ApiResponse = await CountryService.UpdateAsync(Model);

        if (_editContext.ApplyErrors(_messageStore, ApiResponse))
            await OnSuccess.InvokeAsync();
    }
}

