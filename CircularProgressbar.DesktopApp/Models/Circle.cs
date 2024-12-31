using System;
using SkiaSharp;

namespace CircularProgressbar.DesktopApp.Models;

public class Circle
{
    #region Private fields

    private readonly Func<SKImageInfo, SKPoint> _centerFunc;

    #endregion

    #region Properties

    public SKPoint Center { get; private set; }
    public float Radius { get; }

    public SKRect Rect => new(Center.X - Radius, Center.Y - Radius, Center.X + Radius, Center.Y + Radius);

    #endregion

    public Circle(float radius, Func<SKImageInfo, SKPoint> centerFunc)
    {
        _centerFunc = centerFunc;
        Radius = radius;
    }

    public void CalculateCenter(SKImageInfo argsInfo)
    {
        Center = _centerFunc.Invoke(argsInfo);
    }
}