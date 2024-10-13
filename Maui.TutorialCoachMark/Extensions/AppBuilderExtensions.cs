using Mopups.Hosting;

namespace Maui.TutorialCoachMark;

public static class AppBuilderExtensions
{
    public static MauiAppBuilder UseTutorialCoachMark(this MauiAppBuilder builder)
    {
        builder.ConfigureMopups();
        builder.Services.AddSingleton<ITutorialService>(new TutorialService());

        return builder;
    }

    public static MauiAppBuilder UseTutorialCoachMark(this MauiAppBuilder builder, Action? backPressHandler)
    {
        builder.ConfigureMopups(backPressHandler);
        builder.Services.AddSingleton<ITutorialService>(new TutorialService());

        return builder;
    }
}
