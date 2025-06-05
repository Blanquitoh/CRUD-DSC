using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Countries.Responses;

namespace Sakila.Web.Pages.Countries.Components;

public partial class CountryFormDialog
{
    [Parameter] public bool Visible { get; set; }
    [Parameter] public CountryGetByIdResponse Country { get; set; } = new();
    [Parameter] public Dictionary<string, string[]> Errors { get; set; } = new();
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnSubmit { get; set; }
    private string Title => Country.Id != 0 ? "Edit" : "Add";
}