using Maui.TutorialCoachMark.Extensions;
using Mopups.Pages;
using Mopups.Services;

namespace Maui.TutorialCoachMark;

public class TutorialPage : PopupPage
{
    private CancellationTokenSource cancelationTokenSource = new();
    private readonly IEnumerable<View> _views;
    private Grid? contentGrid;
    private int currentIndex;

    public TutorialPage(IEnumerable<View> coachMarkViews)
    {
        Animation = new Mopups.Animations.FadeAnimation();
        CloseWhenBackgroundIsClicked = false;
        BackgroundColor = Colors.Transparent;

        _views = coachMarkViews;
        Content = BuildGridContainer();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var firstView = _views.FirstOrDefault();
        if (firstView is View currentView)
            DrawCoachMarkForView(currentView);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        cancelationTokenSource?.Cancel();
        cancelationTokenSource?.Dispose();
    }

    private void DrawCoachMarkForView(View view)
    {
        if (contentGrid is null)
            return;

        if (contentGrid.Children.Count > 0)
        {
            ResetView();
        }

        var width = view.Width;
        var height = view.Height;

        var point = view.GetAbsolutePosition();
        point = new Point(point.X, point.Y - view.Height / 2);

        var elementAnimation = Tutorial.GetViewAnimation(view);
        var coachmarkViewAnimation = Tutorial.GetCoachMarkAnimation(view);
        var coachmarkView = (View)Tutorial.GetCoachMarkView(view);

        if (coachmarkView.HorizontalOptions == LayoutOptions.Start ||
            coachmarkView.HorizontalOptions == LayoutOptions.Center ||
            coachmarkView.HorizontalOptions == LayoutOptions.End)
            coachmarkView.MaximumWidthRequest = width;

        coachmarkView.Margin = new Thickness(point.X, point.Y + view.Height + 10, 0, 0);

        var viewContainer = new Frame() {
            BackgroundColor = Colors.White,
            CornerRadius = 10,
            WidthRequest = width + 8,
            HeightRequest = height + 8,
            Padding = 4,
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Start,
            BorderColor = Colors.White,
            Margin = new Thickness(point.X - 4, point.Y - 4, 0, 0),
            Content = view,
        };

        contentGrid.Children.Insert(0, viewContainer);
        contentGrid.Children.Insert(0, coachmarkView);

        coachmarkViewAnimation?.Start(coachmarkView, cancelationTokenSource.Token);
        elementAnimation.Start(viewContainer, cancelationTokenSource.Token);
    }

    private void ResetView()
    {
        cancelationTokenSource?.Cancel();
        cancelationTokenSource?.Dispose();
        cancelationTokenSource = new();

        contentGrid.Children.Clear();
    }

    private Grid BuildGridContainer()
    {
        contentGrid = new Grid {
            BackgroundColor = Colors.Black.WithAlpha(.8f),
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill
        };

        contentGrid.GestureRecognizers.Add(new TapGestureRecognizer {
            Command = new Command(async () => {
                currentIndex++;
                var view = _views.ElementAtOrDefault(currentIndex);
                if (view is View currentView)
                    DrawCoachMarkForView(currentView);
                else
                    await MopupService.Instance.PopAsync();
            })
        });

        return contentGrid;
    }
}