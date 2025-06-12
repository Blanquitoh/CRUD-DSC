using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Sakila.Web.Components;

public partial class GeneralValidationSummary
{
    [CascadingParameter] private EditContext CurrentEditContext { get; set; } = null!;
    private IEnumerable<string> _messages = [];

    protected override void OnInitialized()
    {
        if (CurrentEditContext == null)
        {
            throw new InvalidOperationException("GeneralValidationSummary must be used inside an EditForm.");
        }

        CurrentEditContext.OnValidationStateChanged += OnValidationStateChanged;
        UpdateMessages();
    }

    private void OnValidationStateChanged(object? sender, ValidationStateChangedEventArgs e)
    {
        UpdateMessages();
        StateHasChanged();
    }

    private void UpdateMessages()
    {
        var identifier = new FieldIdentifier(CurrentEditContext.Model, string.Empty);
        _messages = CurrentEditContext.GetValidationMessages(identifier);
    }

    public void Dispose()
    {
        CurrentEditContext.OnValidationStateChanged -= OnValidationStateChanged;
    }
}