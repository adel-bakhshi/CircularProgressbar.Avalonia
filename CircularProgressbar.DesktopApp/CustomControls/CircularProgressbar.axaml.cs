using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Labs.Controls;
using CircularProgressbar.DesktopApp.Models;
using SkiaSharp;

namespace CircularProgressbar.DesktopApp.CustomControls;

public class CircularProgressbar : TemplatedControl
{
    #region Private fields

    private SKCanvasView? _canvas;
    private ProgressDrawer? _progressDrawer;

    #endregion

    #region Properties

    public static readonly StyledProperty<double> ProgressSizeProperty = AvaloniaProperty
        .Register<CircularProgressbar, double>(nameof(ProgressSize), defaultValue: 100);

    public double ProgressSize
    {
        get => GetValue(ProgressSizeProperty);
        set => SetValue(ProgressSizeProperty, value);
    }

    public static readonly StyledProperty<double> ProgressThicknessProperty = AvaloniaProperty
        .Register<CircularProgressbar, double>(nameof(ProgressThickness), defaultValue: 10D);

    public double ProgressThickness
    {
        get => GetValue(ProgressThicknessProperty);
        set => SetValue(ProgressThicknessProperty, value);
    }

    public static readonly StyledProperty<double> ProgressProperty = AvaloniaProperty
        .Register<CircularProgressbar, double>(nameof(Progress));

    public double Progress
    {
        get => GetValue(ProgressProperty);
        set => SetValue(ProgressProperty, value);
    }

    #endregion

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _canvas = e.NameScope.Find<SKCanvasView>("CanvasControl");
        if (_canvas == null)
            throw new InvalidOperationException("CanvasControl not found");

        var radius = CalculateRadius();
        var circle = new Circle(radius, info => new SKPoint((float)info.Width / 2, (float)info.Height / 2));
        _progressDrawer = new ProgressDrawer(_canvas, circle, () => (float)Progress, (float)ProgressThickness, SKColors.DodgerBlue, SKColors.Crimson);
    }

    #region Helpers

    private float CalculateRadius()
    {
        return (float)(ProgressSize / 2 - ProgressThickness / 2);
    }

    #endregion
}