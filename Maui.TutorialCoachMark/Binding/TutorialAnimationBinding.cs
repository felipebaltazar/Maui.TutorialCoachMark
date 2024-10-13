
namespace Maui.TutorialCoachMark;

public static class TutorialAnimation
{
    public static BindableProperty CancelTutorialAnimation =
        BindableProperty.CreateAttached("CancelTutorialAnimation", typeof(bool), typeof(TutorialAnimation), false);

    public static bool GetCancelAnimation(BindableObject bindable) =>
        (bool)bindable.GetValue(CancelTutorialAnimation);

    public static void SetCancelAnimation(BindableObject bindable, bool value) =>
        bindable.SetValue(CancelTutorialAnimation, value);


    public static BindableProperty AnimatingProperty =
        BindableProperty.CreateAttached("Animating", typeof(bool), typeof(TutorialAnimation), false);

    public static void SetAnimating(BindableObject bindable, bool value) =>
        bindable.SetValue(AnimatingProperty, value);

    public static bool GetAnimating(BindableObject bindable) => 
        (bool)bindable.GetValue(AnimatingProperty);
}
