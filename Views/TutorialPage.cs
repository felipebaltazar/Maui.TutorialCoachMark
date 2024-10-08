using Maui.TutorialCoachMark.Extensions;
using Microsoft.Maui.Controls.Shapes;
using Mopups.Pages;
using Path = Microsoft.Maui.Controls.Shapes.Path;

namespace Maui.TutorialCoachMark;

public class TutorialPage : PopupPage
{
    private Grid contentGrid;
    private Path? currentPath;

    public TutorialPage()
    {
        BackgroundColor = Colors.Transparent;
        CloseWhenBackgroundIsClicked = false;

        contentGrid = new Grid {
            BackgroundColor = Colors.Transparent,
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill
        };

        Content = contentGrid;
    }

    private void DrawCoachMarkForView(View view)
    {
        var point = view.GetAbsolutePosition();
        var path = new Path() {
            Fill = Brush.Black,
            Opacity = 0.8
        };

        var geometryCollection = new GeometryCollection()
        {
            new RoundRectangleGeometry(new CornerRadius(0), new Rect( point.X, point.Y, view.Width, view.Height)),
            new RectangleGeometry() { Rect = new Rect(0,0, this.Width, this.Height) },
        };

        var geometryGroup = new GeometryGroup() { Children = geometryCollection };
        path.Data = geometryGroup;

        contentGrid.Children.Insert(0, path);

        if (currentPath != null)
            contentGrid.Children.Remove(currentPath);

        currentPath = path;
    }
}