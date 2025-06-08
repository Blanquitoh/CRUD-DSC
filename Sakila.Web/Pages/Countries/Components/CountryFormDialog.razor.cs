using Microsoft.AspNetCore.Components;
using Sakila.Web.Abstractions;
using Sakila.Contracts.Countries.Queries.Responses;

namespace Sakila.Web.Pages.Countries.Components;

public partial class CountryFormDialog
{
    [Parameter] public bool Visible { get; set; }
    [Parameter] public CountryGetByIdResponse Country { get; set; } = new();
    [Parameter] public IApiResponse<object>? ApiResponse { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnSubmit { get; set; }
    private string Title => Country.Id != 0 ? "Edit" : "Add";
}