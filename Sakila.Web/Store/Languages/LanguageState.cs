using Fluxor;
using Sakila.Contracts.Languages.Queries.Responses;

namespace Sakila.Web.Store.Languages;

public record LanguageState(
    bool IsCreateDialogOpen,
    bool IsUpdateDialogOpen,
    bool IsDeleteDialogOpen,
    LanguageGetByIdResponse SelectedLanguage);

public class LanguageFeature : Feature<LanguageState>
{
    public override string GetName()
    {
        return "Language";
    }

    protected override LanguageState GetInitialState()
    {
        return new LanguageState(false, false, false, new LanguageGetByIdResponse());
    }
}

public record ShowCreateDialogAction;

public record CloseCreateDialogAction;

public record ShowUpdateDialogAction(LanguageGetByIdResponse Language);

public record CloseUpdateDialogAction;

public record ShowDeleteDialogAction(LanguageGetByIdResponse Language);

public record CloseDeleteDialogAction;

public static class LanguageReducers
{
    [ReducerMethod]
    public static LanguageState ReduceShowCreateDialog(LanguageState state, ShowCreateDialogAction action)
    {
        return state with { IsCreateDialogOpen = true };
    }

    [ReducerMethod]
    public static LanguageState ReduceCloseCreateDialog(LanguageState state, CloseCreateDialogAction action)
    {
        return state with { IsCreateDialogOpen = false };
    }

    [ReducerMethod]
    public static LanguageState ReduceShowUpdateDialog(LanguageState state, ShowUpdateDialogAction action)
    {
        return state with { IsUpdateDialogOpen = true, SelectedLanguage = action.Language };
    }

    [ReducerMethod]
    public static LanguageState ReduceCloseUpdateDialog(LanguageState state, CloseUpdateDialogAction action)
    {
        return state with { IsUpdateDialogOpen = false };
    }

    [ReducerMethod]
    public static LanguageState ReduceShowDeleteDialog(LanguageState state, ShowDeleteDialogAction action)
    {
        return state with { IsDeleteDialogOpen = true, SelectedLanguage = action.Language };
    }

    [ReducerMethod]
    public static LanguageState ReduceCloseDeleteDialog(LanguageState state, CloseDeleteDialogAction action)
    {
        return state with { IsDeleteDialogOpen = false };
    }
}