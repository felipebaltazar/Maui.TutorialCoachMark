namespace Maui.TutorialCoachMark.Extensions;

public static class ViewExtensions
{
    internal static Point GetAbsolutePosition(this View originalView)
    {
        double x = 0;
        double y = 0;
        View? view = originalView;

        while (view != null)
        {
            x += view.X;
            y += view.Y;

            if (view.Parent != null && view.Parent is View p)
            {
                view = p;
            }
            else
            {
                var currentPage = Shell.Current.CurrentPage;
                if (Shell.GetTitleView(currentPage) != null)
                {
                    y += Shell.GetTitleView(currentPage).Height;
                }

                view = null;
            }
        }

        return new Point(x, y);
    }
}
