using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Languages.Responses;

namespace Sakila.Web.Pages.Languages.Components;

public partial class ConfirmDelete
{
    [Parameter] public bool Visible { get; set; }
    [Parameter] public LanguageGetByIdResponse Language { get; set; } = new();
    [Parameter] public Dictionary<string, string[]> Errors { get; set; } = new();
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnConfirm { get; set; }
}