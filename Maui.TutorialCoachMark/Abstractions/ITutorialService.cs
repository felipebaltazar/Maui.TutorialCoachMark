
namespace Maui.TutorialCoachMark;

public interface ITutorialService
{
    Task ShowTutorialAsync(IList<View> coachMarkViews);
}
