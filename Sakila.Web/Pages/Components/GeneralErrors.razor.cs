using Microsoft.AspNetCore.Components;

namespace Sakila.Web.Pages.Components;

public partial class GeneralErrors
{
    [Parameter] public IEnumerable<string>? Messages { get; set; }
}
