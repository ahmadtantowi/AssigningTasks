using System;
namespace AssigningTasks
{
    public static class Extensions
    {
        public static double DegreeToRadian(this double degree)
        {
            return degree * Math.PI / 180;
        }

        public static double RadianToDegree(this double radian)
        {
            return radian / Math.PI * 180;
        }
    }
}
