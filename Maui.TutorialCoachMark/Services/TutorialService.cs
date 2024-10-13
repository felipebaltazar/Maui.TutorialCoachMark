
using Mopups.Services;

namespace Maui.TutorialCoachMark;

internal class TutorialService : ITutorialService
{
    public static ITutorialService Instance
    {
        get; private set;
    }

    public TutorialService()
    {
        Instance = this;
    }

    public async Task ShowTutorialAsync(IList<View> coachMarkViews)
    {
        var instance = MopupService.Instance;
        var currentTutorialPage = instance.PopupStack.FirstOrDefault(p => p.GetType() == typeof(TutorialPage));

        if (currentTutorialPage is not null)
            await instance.RemovePageAsync(currentTutorialPage);

        var tutorialPage = new TutorialPage(coachMarkViews.OrderBy(Tutorial.GetTutorialOrder));
        await instance.PushAsync(tutorialPage);
    }
}
