using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Languages.Commands;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Languages.Components;

public partial class LanguageCreateDialog
{
    [Parameter] public bool Visible { get; set; }
    [Parameter] public LanguageCreateRequest Language { get; set; } = new();
    [Parameter] public IApiResponse<object>? ApiResponse { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnSubmit { get; set; }
}

