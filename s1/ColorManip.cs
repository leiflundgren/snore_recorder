using System;
using System.Drawing;

namespace s1
{
    public class ColorManip
    {
        public static Color Lighten(Color inColor, double inAmount)
        {
            return Color.FromArgb(
              inColor.A,
              Math.Min(255, (int)(inColor.R + 255 * inAmount)),
              Math.Min(255, (int)(inColor.G + 255 * inAmount)),
              Math.Min(255, (int)(inColor.B + 255 * inAmount)));
        }

        public static Color Darken(Color inColor, double inAmount)
        {
            return Color.FromArgb(
              inColor.A,
              Math.Max(0, (int)(inColor.R - 255 * inAmount)),
              Math.Max(0, (int)(inColor.G - 255 * inAmount)),
              Math.Max(0, (int)(inColor.B - 255 * inAmount)));
        }

        public static Color Alpha(Color inColor, double alpha)
        {
            return Color.FromArgb(
              (int)(alpha * 255.0),
              inColor.R,
              inColor.G,
              inColor.B);
        }
    }
}
