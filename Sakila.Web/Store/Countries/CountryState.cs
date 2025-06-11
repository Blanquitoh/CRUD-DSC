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
    public override string GetName()
    {
        return "Country";
    }

    protected override CountryState GetInitialState()
    {
        return new CountryState(false, false, false, new CountryGetByIdResponse());
    }
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
    public static CountryState ReduceShowCreateDialog(CountryState state, ShowCreateDialogAction action)
    {
        return state with { IsCreateDialogOpen = true };
    }

    [ReducerMethod]
    public static CountryState ReduceCloseCreateDialog(CountryState state, CloseCreateDialogAction action)
    {
        return state with { IsCreateDialogOpen = false };
    }

    [ReducerMethod]
    public static CountryState ReduceShowUpdateDialog(CountryState state, ShowUpdateDialogAction action)
    {
        return state with { IsUpdateDialogOpen = true, SelectedCountry = action.Country };
    }

    [ReducerMethod]
    public static CountryState ReduceCloseUpdateDialog(CountryState state, CloseUpdateDialogAction action)
    {
        return state with { IsUpdateDialogOpen = false };
    }

    [ReducerMethod]
    public static CountryState ReduceShowDeleteDialog(CountryState state, ShowDeleteDialogAction action)
    {
        return state with { IsDeleteDialogOpen = true, SelectedCountry = action.Country };
    }

    [ReducerMethod]
    public static CountryState ReduceCloseDeleteDialog(CountryState state, CloseDeleteDialogAction action)
    {
        return state with { IsDeleteDialogOpen = false };
    }
}