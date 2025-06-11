using Fluxor;
using Sakila.Contracts.Countries.Queries.Responses;

namespace Sakila.Web.Store.Countries;

public record CountryState(
    bool IsCreateDialogOpen,
    bool IsUpdateDialogOpen,
    bool IsDeleteDialogOpen,
    CountryGetByIdResponse SelectedCountry);

public class CountryFeature : Feature<CountryState>
{
    public override string GetName() => "Country";

    protected override CountryState GetInitialState() =>
        new(false, false, false, new());
}

public record ShowCreateDialogAction;
public record CloseCreateDialogAction;
public record ShowUpdateDialogAction(CountryGetByIdResponse Country);
public record CloseUpdateDialogAction;
public record ShowDeleteDialogAction(CountryGetByIdResponse Country);
public record CloseDeleteDialogAction;

public static class CountryReducers
{
    [ReducerMethod]
    public static CountryState ReduceShowCreateDialog(CountryState state, ShowCreateDialogAction action) =>
        state with { IsCreateDialogOpen = true };

    [ReducerMethod]
    public static CountryState ReduceCloseCreateDialog(CountryState state, CloseCreateDialogAction action) =>
        state with { IsCreateDialogOpen = false };

    [ReducerMethod]
    public static CountryState ReduceShowUpdateDialog(CountryState state, ShowUpdateDialogAction action) =>
        state with { IsUpdateDialogOpen = true, SelectedCountry = action.Country };

    [ReducerMethod]
    public static CountryState ReduceCloseUpdateDialog(CountryState state, CloseUpdateDialogAction action) =>
        state with { IsUpdateDialogOpen = false };

    [ReducerMethod]
    public static CountryState ReduceShowDeleteDialog(CountryState state, ShowDeleteDialogAction action) =>
        state with { IsDeleteDialogOpen = true, SelectedCountry = action.Country };

    [ReducerMethod]
    public static CountryState ReduceCloseDeleteDialog(CountryState state, CloseDeleteDialogAction action) =>
        state with { IsDeleteDialogOpen = false };
}
