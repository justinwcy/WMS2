using MudBlazor;

namespace WebUI.Themes;

public static class Theme
{
    public static Variant DefaultInputVariant()
    {
        return Variant.Text;
    }

    public static DialogOptions DefaultCreateDialogOptions()
    {
        return new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true };
    }

    public static DialogOptions DefaultViewDialogOptions()
    {
        return new DialogOptions() { MaxWidth = MaxWidth.ExtraLarge, FullWidth = true };
    }

    public static MudTheme ApplicationTheme()
    {
        var theme = new MudTheme
        {
            PaletteLight = new PaletteLight
            {
                Primary = "#5052ba", 
                Secondary = "#7D7D7D", 
                Success = "#0CAD39", 
                Info = "#4099f3", 
                Warning = "#f0c42b", 
                Error = "rgba(244,67,54,1)",
                ErrorContrastText = "#ffffff",
                ErrorDarken = "rgb(242,28,13)",
                ErrorLighten = "rgb(246,96,85)",
                Tertiary = "#20c997",
                Black = "#111", 
                White = "#ffffff", 
                AppbarBackground = "rgba(245, 245, 245, 0.8)",
                AppbarText = "#424242",
                Background = "#f5f5f5", 
                Surface = "#ffffff", 
                DrawerBackground = "#ffffff",
                TextPrimary = "#2E2E2E", 
                TextSecondary = "#6c757d", 
                SecondaryContrastText = "#F5F5F5", 
                TextDisabled = "#B0B0B0", 
                ActionDefault = "#80838b", 
                ActionDisabled = "rgba(128, 131, 139, 0.3)",
                ActionDisabledBackground = "rgba(128, 131, 139, 0.12)",
                Divider = "#e2e5e8", 
                DividerLight = "rgba(128, 131, 139, 0.15)",
                TableLines = "#eff0f2", 
                LinesDefault = "#e2e5e8", 
                LinesInputs = "#e2e5e8",
            },
            PaletteDark = new PaletteDark
            {
                Primary = "#5052ba", 
                Secondary = "#A5A5A5", 
                Success = "#0CAD39", 
                Info = "#4099f3", 
                Warning = "#f0c42b",
                Error = "#e02d48",
                ErrorContrastText = "#ffffff",
                ErrorDarken = "#e02d48",
                ErrorLighten = "#ff3333",
                Tertiary = "#20c997",
                Black = "#000000", 
                White = "#ffffff", 
                Background = "#202124", 
                Surface = "#303134",
                AppbarBackground = "rgba(32, 33, 36, 0.8)",
                AppbarText = "rgba(255, 255, 255, 0.7)",
                DrawerBackground = "#303134",
                TextPrimary = "#DADADA", 
                TextSecondary = "#A8A8A8",
                TextDisabled = "rgba(255, 255, 255, 0.3)",
                SecondaryContrastText = "#D5D5D5",
                ActionDefault = "#e8eaed", 
                ActionDisabled = "rgba(255, 255, 255, 0.26)",
                ActionDisabledBackground = "rgba(255, 255, 255, 0.12)",
                Divider = "#3F4452", 
                DividerLight = "rgba(255, 255, 255, 0.06)",
                TableLines = "rgba(63, 68, 82, 0.6)",
                LinesDefault = "#3F4452", 
                LinesInputs = "rgba(255, 255, 255, 0.3)",
            },
            LayoutProperties = new LayoutProperties
            {
                AppbarHeight = "80px",
                DefaultBorderRadius = "6px"
            }
        };
        return theme;
    }
}