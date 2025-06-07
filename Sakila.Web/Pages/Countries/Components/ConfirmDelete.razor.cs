using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Countries.Queries.Responses;

namespace Sakila.Web.Pages.Countries.Components;

public partial class ConfirmDelete
{
    [Parameter] public bool Visible { get; set; }
    [Parameter] public CountryGetByIdResponse Country { get; set; } = new();
    [Parameter] public Dictionary<string, string[]> Errors { get; set; } = new();
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnConfirm { get; set; }
}
