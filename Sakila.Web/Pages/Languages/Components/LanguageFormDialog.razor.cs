using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Common;
using Sakila.Contracts.Languages.Queries.Responses;

namespace Sakila.Web.Pages.Languages.Components;

public partial class LanguageFormDialog
{
    [Parameter] public bool Visible { get; set; }
    [Parameter] public LanguageGetByIdResponse Language { get; set; } = new();
    [Parameter] public IApiResponse<object>? ApiResponse { get; set; }
    // WE MUST HANDLE SERVER ERRORS
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnSubmit { get; set; }
    private string Title => Language.Id != 0 ? "Edit" : "Add";
}
