using System;
using Avalonia.Labs.Controls;
using SkiaSharp;

namespace CircularProgressbar.DesktopApp.Models;

public class ProgressDrawer
{
    public ProgressDrawer(SKCanvasView canvas, Circle circle, Func<float> progress, float strokeWidth, SKColor progressColor, SKColor foregroundColor)
    {
        canvas.PaintSurface += (sender, args) =>
        {
            circle.CalculateCenter(args.Info);
            args.Surface.Canvas.Clear();
            DrawCircle(args.Surface.Canvas, circle, strokeWidth, progressColor);
            DrawArc(args.Surface.Canvas, circle, progress, strokeWidth, foregroundColor);
        };
    }

    #region Helpers

    private static void DrawCircle(SKCanvas canvas, Circle circle, float strokewidth, SKColor color)
    {
        canvas.DrawCircle(circle.Center, circle.Radius,
            new SKPaint
            {
                StrokeWidth = strokewidth,
                Color = color,
                IsStroke = true,
                IsAntialias = true
            });
    }

    private static void DrawArc(SKCanvas canvas, Circle circle, Func<float> progress, float strokewidth, SKColor color)
    {
        var angle = progress.Invoke() * 3.6f;
        canvas.DrawArc(circle.Rect, 270, angle, false, new SKPaint
        {
            StrokeWidth = strokewidth,
            Color = color,
            IsStroke = true,
            IsAntialias = true
        });
    }

    #endregion
}