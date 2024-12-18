﻿using System.Drawing;
using TagsCloudVisualization.ConsoleCommands;

namespace TagsCloudVisualization.Distributors;

public class SpiralDistribution : ICloudDistribution
{
    private const double AngleStep = 0.02;
    private const double RadiusStep = 0.01;
    private double angle;
    private double radius;
    private readonly Point cloudCenter;

    public SpiralDistribution(Options options)
    {
        cloudCenter = new Point(options.ImageWidth / 2, options.ImageHeight / 2);
    }
    
    public Point GetNextPoint()
    {
        var nextPoint = ConvertPolarToCartesian();
        angle += AngleStep;
        radius += RadiusStep;
        return nextPoint;
    }
    
    public Point GetCenter() => cloudCenter;

    private Point ConvertPolarToCartesian()
    {
        var cartesian = new Point((int)(radius * Math.Cos(angle)), (int)(radius * Math.Sin(angle)));
        cartesian.Offset(cloudCenter);
        return cartesian;
    }
}