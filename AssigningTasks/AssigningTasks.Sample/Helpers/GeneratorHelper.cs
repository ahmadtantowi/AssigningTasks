using System;
using System.Text;

namespace AssigningTasks.Sample.Helpers
{
    public static class GeneratorHelper
    {
        private static Random _rnd;
        private static StringBuilder _stringBuilder;
        private static double _radiusInDegree, _u, _v, _w, _t, _x, _y, _newX;
        private static string[] _consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
        private static string[] _vowels = { "a", "e", "i", "o", "u", "ae", "y" };

        public static string GenerateName(int length)
        {
            _rnd = _rnd ?? new Random();
            _stringBuilder = _stringBuilder ?? new StringBuilder();
            _stringBuilder.Clear();

            _stringBuilder.Append(_consonants[_rnd.Next(_consonants.Length)].ToUpper());
            _stringBuilder.Append(_vowels[_rnd.Next(_vowels.Length)]);

            while (_stringBuilder.Length < length - 2)
            {
                _stringBuilder.Append(_consonants[_rnd.Next(_consonants.Length)]);
                _stringBuilder.Append(_vowels[_rnd.Next(_vowels.Length)]);
            }

            return _stringBuilder.ToString();
        }

        public static Location GenerateNearbyLocation(double latitudeY, double longitudeX, double radius)
        {
            _rnd = _rnd ?? new Random();
            
            // Convert radius from meters to degrees
            _radiusInDegree = radius / 111000f;

            _u = _rnd.NextDouble();
            _v = _rnd.NextDouble();
            _w = _radiusInDegree * Math.Sqrt(_u);
            _t = 2 * Math.PI * _v;
            _x = _w * Math.Cos(_t);
            _y = _w * Math.Sin(_t);

            // Adjust the x-coordinate for the shrinking of the east-west distances
            _newX = _x / Math.Cos(latitudeY);

            return new Location
            {
                Latitude = _y + latitudeY,
                Longitude = _newX + longitudeX
            };
        }

        public static int GenerateLoad(int minLoad, int maxLoad)
        {
            _rnd = _rnd ?? new Random();
            return _rnd.Next(minLoad, maxLoad);
        }

        public static int GenerateTotalTravel(int load)
        {
            _rnd = _rnd ?? new Random();
            return load == 0 ? 0 : _rnd.Next(load * 250, load * 500);
        }
    }
}
