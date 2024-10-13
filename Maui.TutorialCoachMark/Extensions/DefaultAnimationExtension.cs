﻿using Maui.TutorialCoachMark.Models;
using BaseAnimation = Maui.TutorialCoachMark.Models.BaseAnimation;
using FadeAnimation = Maui.TutorialCoachMark.Models.FadeAnimation;

namespace Maui.TutorialCoachMark;

[ContentProperty(nameof(Source))]
public sealed class DefaultAnimationExtension : IMarkupExtension<BaseAnimation>
{
    public int Interval { get; set; } = 500;

    public double? Parameter
    {
        get; set;
    }

    public AnimationTypes Source
    {
        get; set;
    }

    public BaseAnimation? ProvideValue(IServiceProvider serviceProvider)
    {
        switch (Source)
        {
            case AnimationTypes.Beat:
                return new BeatAnimation(Interval, Parameter);
            case AnimationTypes.Fade:
                return new FadeAnimation(Interval, Parameter);
            case AnimationTypes.VerticalShake:
                return new VerticalShakeAnimation(Interval, Parameter);
            case AnimationTypes.HorizontalShake:
                return new HorizontalShakeAnimation(Interval, Parameter);
            case AnimationTypes.None:
            default:
                return null;
        }
    }

    object? IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) =>
        ProvideValue(serviceProvider);
}
