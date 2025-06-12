using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sakila.Web.Abstractions;
using Sakila.Web.Components.BaseCrud;

namespace Sakila.Web.Pages.BaseCrud;

public class BaseCrudPageBase<TService, TCreate, TUpdate, TGetAll, TGetById, TItem, TCreateDialog, TUpdateDialog, TDeleteDialog> : ComponentBase
    where TService : ICrudService<TCreate, TUpdate, TGetAll, TGetById>
    where TCreateDialog : IComponent
    where TUpdateDialog : IComponent
    where TDeleteDialog : IComponent
{
    protected ISakilaApiResponse<TGetAll>? GetAllResponse;

    [Inject] protected TService Service { get; set; } = default!;
    [Inject] protected IDialogService DialogService { get; set; } = null!;

    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public string AddButtonText { get; set; } = string.Empty;
    [Parameter] public RenderFragment? HeaderContent { get; set; }
    [Parameter] public RenderFragment<TItem>? RowTemplate { get; set; }
    [Parameter] public Func<TGetAll, IEnumerable<TItem>>? ItemsSelector { get; set; }
    [Parameter] public string CreateDialogTitle { get; set; } = string.Empty;
    [Parameter] public string UpdateDialogTitle { get; set; } = string.Empty;
    [Parameter] public string DeleteDialogTitle { get; set; } = string.Empty;
    [Parameter] public Func<TItem, DialogParameters>? UpdateDialogParameters { get; set; }
    [Parameter] public Func<TItem, DialogParameters>? DeleteDialogParameters { get; set; }

    protected IEnumerable<TItem>? Items => GetAllResponse?.Data == null ? null : ItemsSelector?.Invoke(GetAllResponse.Data);

    protected override async Task OnInitializedAsync()
    {
        await RefreshAsync();
    }

    protected async Task RefreshAsync()
    {
        await Service.GetAllAsync(r => Task.FromResult(GetAllResponse = r));
    }

    protected Task ShowCreateDialog()
    {
        return DialogService.ShowDialogAsync<TCreateDialog>(CreateDialogTitle, onSuccess: RefreshAsync);
    }

    protected Task ShowUpdateDialog(TItem item)
    {
        var parameters = UpdateDialogParameters?.Invoke(item);
        return DialogService.ShowDialogAsync<TUpdateDialog>(UpdateDialogTitle, parameters, RefreshAsync);
    }

    protected Task ShowDeleteDialog(TItem item)
    {
        var parameters = DeleteDialogParameters?.Invoke(item);
        return DialogService.ShowDialogAsync<TDeleteDialog>(DeleteDialogTitle, parameters, RefreshAsync);
    }
}
