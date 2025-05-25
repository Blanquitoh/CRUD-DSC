using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Languages.Responses;

namespace Sakila.Web.Pages.Languages.Components;

public partial class LanguageFormDialog
{
    [Parameter] public bool Visible { get; set; }
    [Parameter] public LanguageGetByIdResponse Language { get; set; } = new();
    [Parameter] public Dictionary<string, string[]> Errors { get; set; } = new();
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnSubmit { get; set; }
    private string Title => Language.Id != 0 ? "Edit" : "Add";
}