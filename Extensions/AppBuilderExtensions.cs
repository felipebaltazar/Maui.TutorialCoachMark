using Mopups.Hosting;

namespace Maui.TutorialCoachMark;

public static class AppBuilderExtensions
{
    public static MauiAppBuilder UseTutorialCoachMark(this MauiAppBuilder builder)
    {
        builder.ConfigureMopups();
        return builder;
    }

    public static MauiAppBuilder UseTutorialCoachMark(this MauiAppBuilder builder, Action? backPressHandler)
    {
        builder.ConfigureMopups(backPressHandler);
        return builder;
    }
}
