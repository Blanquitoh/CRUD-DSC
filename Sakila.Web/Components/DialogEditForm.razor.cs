using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Sakila.Web.Components;

public partial class DialogEditForm
{
    [Parameter] public EditContext EditContext { get; set; } = null!;
    [Parameter] public EventCallback OnValidSubmit { get; set; }
    [Parameter] public RenderFragment? DialogContent { get; set; }
    [Parameter] public RenderFragment? DialogActions { get; set; }
}