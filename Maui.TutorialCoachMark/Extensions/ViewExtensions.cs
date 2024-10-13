namespace Maui.TutorialCoachMark.Extensions;

public static class ViewExtensions
{
    internal static Page? GetPage(this IView view)
    {
        var parent = view.Parent;
        while (parent != null)
        {
            if (parent is Page page)
                return page;

            parent = parent.Parent;
        }

        return null;
    }

    internal static Point GetAbsolutePosition(this View originalView) =>
        GetCoordinates(originalView);

#if ANDROID

    private static Point GetCoordinates(View element)
    {
        var renderer = element.Handler;
        var nativeView = renderer?.PlatformView as Android.Views.View;
        var density = nativeView?.Context?.Resources?.DisplayMetrics?.Density ?? 0;

        if (nativeView is null)
            return Point.Zero;

        var location = new Android.Graphics.Rect();
        if (nativeView.GetGlobalVisibleRect(location))
        {
            return new Point(location.Left / density, location.Top / density);
        }

        return Point.Zero;
    }

#elif IOS || MACCATALYST

    private static Point GetCoordinates(View element)
    {
        var renderer = element.Handler;
        var nativeView = renderer?.PlatformView as UIKit.UIView;

        if (nativeView is null)
            return Point.Zero;

        var rect = nativeView.Superview.ConvertPointToView(nativeView.Frame.Location, null);
        return new Point((int)Math.Round(rect.X), (int)Math.Round(rect.Y));
    }

#else

    private static Point GetCoordinates(View element)
    {
        var renderer = element.Handler;
        var nativeView = renderer?.PlatformView;

        if (nativeView is null)
            return Point.Zero;

        //TODO(Windows): implement this
        //var element_Visual_Relative = nativeView.TransformToVisual(Window.Current.Content);
        //Point point = element_Visual_Relative.TransformPoint(new Point(0, 0));
        //return new System.Drawing.PointF((int)Math.Round(point.X), (int)Math.Round(point.Y));
    }

#endif
}
