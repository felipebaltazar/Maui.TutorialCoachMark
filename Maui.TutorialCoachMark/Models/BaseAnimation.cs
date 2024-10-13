namespace Maui.TutorialCoachMark.Models;

public abstract class BaseAnimation
{
    private CancellationToken _cancelToken;

    public uint Interval
    {
        get; set;
    }

    public double Parameter
    {
        get; set;
    }

    protected abstract Task<bool> Animate(BindableObject bindable);

    protected abstract Task StopAnimation(BindableObject bindable);

    public void Start(BindableObject bindable, CancellationToken cancelToken)
    {
        _cancelToken = cancelToken;
        Task.Run(async () => await Run(bindable));
    }

    public void Stop(BindableObject bindable)
    {
        Task.Run(async () => await StopAnimation(bindable));
    }

    private async Task<bool> Run(BindableObject bindable)
    {
        if (TutorialAnimation.GetCancelAnimation(bindable) || _cancelToken.IsCancellationRequested)
        {
            Stop(bindable);
            TutorialAnimation.SetAnimating(bindable, false);
            return false;
        }
        else
        {
            TutorialAnimation.SetAnimating(bindable, true);
            await Animate(bindable);
            return await Run(bindable);
        }
    }
}
