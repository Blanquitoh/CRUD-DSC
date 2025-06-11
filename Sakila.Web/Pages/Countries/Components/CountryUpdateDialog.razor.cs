using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Countries.Commands;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Countries.Components;

public partial class CountryUpdateDialog
{
    [Parameter] public bool Visible { get; set; }
    [Parameter] public CountryUpdateRequest Country { get; set; } = new();
    [Parameter] public IApiResponse<object>? ApiResponse { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnSubmit { get; set; }
}

