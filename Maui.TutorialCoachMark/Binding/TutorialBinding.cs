using Maui.TutorialCoachMark.Models;

namespace Maui.TutorialCoachMark;

public static class Tutorial
{
    private static Dictionary<Page, List<View>> _tutorialMap =
        [];

    #region CoachMarkView

    public static BindableProperty CoachMarkViewProperty =
        BindableProperty.CreateAttached(
            "CoachMarkView",
            typeof(IView),
            typeof(IView),
            null);

    public static IView GetCoachMarkView(BindableObject view) =>
        (IView)view.GetValue(CoachMarkViewProperty);

    public static void SetCoachMarkView(BindableObject view, IView value) =>
        view.SetValue(CoachMarkViewProperty, value);

    #endregion

    #region CoachMarkViewAnimation

    public static BindableProperty CoachMarkAnimationProperty =
        BindableProperty.CreateAttached(
            "CoachMarkAnimation",
            typeof(BaseAnimation),
            typeof(IView),
            null);

    public static void SetCoachMarkAnimation(BindableObject view, BaseAnimation value) =>
        view.SetValue(CoachMarkAnimationProperty, value);

    public static BaseAnimation? GetCoachMarkAnimation(View view)
    {
        if (view.GetValue(CoachMarkAnimationProperty) is BaseAnimation animation)
            return animation;

        return null;
    }

    #endregion

    #region

    public static BindableProperty ViewAnimationProperty =
        BindableProperty.CreateAttached(
            "ViewAnimation",
            typeof(BaseAnimation),
            typeof(IView),
            null);

    public static void SetViewAnimation(BindableObject view, BaseAnimation value) =>
        view.SetValue(ViewAnimationProperty, value);

    public static BaseAnimation GetViewAnimation(View view)
    {
        if (view.GetValue(ViewAnimationProperty) is BaseAnimation animation)
            return animation;

        return new BeatAnimation(500, null);
    }


    #endregion

    #region EnableTutorial

    public static BindableProperty EnableTutorialProperty =
        BindableProperty.CreateAttached(
            "EnableTutorial",
            typeof(bool),
            typeof(Page),
            false,
            propertyChanged: OnEnableTutorialChanged);


    public static bool GetEnableTutorial(BindableObject view) =>
        (bool)view.GetValue(EnableTutorialProperty);

    public static void SetEnableTutorial(BindableObject view, bool value) =>
        view.SetValue(EnableTutorialProperty, value);

    private static void OnEnableTutorialChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not Page page)
            return;

        if (newValue is bool enabled && enabled)
            page.Appearing += OnPageAppearing;
        else
            page.Appearing -= OnPageAppearing;
    }

    #endregion

    #region TutorialOrder

    public static BindableProperty TutorialOrderProperty =
        BindableProperty.CreateAttached(
            "TutorialOrder",
            typeof(int),
            typeof(IView),
            int.MaxValue,
            propertyChanged: OnTutorialOrderChanged);


    public static int GetTutorialOrder(BindableObject view) =>
        (int)view.GetValue(TutorialOrderProperty);

    public static void SetTutorialOrder(BindableObject view, int value) =>
        view.SetValue(TutorialOrderProperty, value);

    private static void OnTutorialOrderChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not View coachMarkView)
            return;

        var page = GetTutorialParent(coachMarkView);
        if (page is null)
            return;

        if (!_tutorialMap.ContainsKey(page))
            _tutorialMap.Add(page, []);

        _tutorialMap[page].Add(coachMarkView);
    }

    #endregion

    #region TutorialParent

    public static BindableProperty TutorialParentProperty =
        BindableProperty.CreateAttached(
            "TutorialParent",
            typeof(Page),
            typeof(View),
            null,
            propertyChanged: OnTutorialOrderChanged);


    public static Page GetTutorialParent(BindableObject view) =>
        (Page)view.GetValue(TutorialParentProperty);

    public static void SetTutorialParent(BindableObject view, Page value) =>
        view.SetValue(TutorialParentProperty, value);

    #endregion

    internal static List<View> GetCoachMarksForPage(Page page) =>
        _tutorialMap.ContainsKey(page) ? _tutorialMap[page] : new List<View>(0);

    private static async void OnPageAppearing(object? sender, EventArgs e)
    {
        if (sender is not Page page)
            return;

        if (!_tutorialMap.ContainsKey(page))
            return;

        var coachMarkViews = _tutorialMap[page];

        await TutorialService.Instance.ShowTutorialAsync(coachMarkViews);
        page.Appearing -= OnPageAppearing;
    }
}
