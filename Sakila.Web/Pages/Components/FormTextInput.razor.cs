using Microsoft.AspNetCore.Components;

namespace Sakila.Web.Pages.Components;

public partial class FormTextInput
{
    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public string Field { get; set; } = string.Empty;
    [Parameter] public Dictionary<string, string[]> Errors { get; set; } = new();
    [Parameter] public string Value { get; set; } = string.Empty;
    [Parameter] public EventCallback<string> ValueChanged { get; set; }

    private IEnumerable<string> FieldErrors => Errors.TryGetValue(Field, out var messages)
        ? messages
        : Enumerable.Empty<string>();

    private bool HasErrors => FieldErrors.Any();

    private async Task OnInput(ChangeEventArgs e)
    {
        Value = e.Value?.ToString() ?? string.Empty;
        await ValueChanged.InvokeAsync(Value);
    }
}