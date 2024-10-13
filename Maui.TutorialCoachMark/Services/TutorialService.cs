
using Mopups.Services;

namespace Maui.TutorialCoachMark;

internal class TutorialService : ITutorialService
{
    public static TutorialService Instance
    {
        get; private set;
    }

    public TutorialService()
    {
        Instance = this;
    }

    public Task ShowTutorialAsync(Page parent)
    {
        var coachMarkViews = Tutorial.GetCoachMarksForPage(parent);
        return ShowTutorialAsync(coachMarkViews);
    }

    internal async Task ShowTutorialAsync(IList<View> coachMarkViews)
    {
        var instance = MopupService.Instance;
        var currentTutorialPage = instance.PopupStack.FirstOrDefault(p => p.GetType() == typeof(TutorialPage));

        if (currentTutorialPage is not null)
            await instance.RemovePageAsync(currentTutorialPage);

        var tutorialPage = new TutorialPage(coachMarkViews.OrderBy(Tutorial.GetTutorialOrder));
        await instance.PushAsync(tutorialPage);
    }
}
