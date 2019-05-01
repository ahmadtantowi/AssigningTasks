using AssigningTasks.Sample.Business;
using AssigningTasks.Sample.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public static async Task<List<Data.Candidate>> GetCandidatesFromMinimarket(IHereMaps hereMaps)
        {
            _rnd = _rnd ?? new Random();
            List<Data.Candidate> candidates = new List<Data.Candidate>();

            var s = await hereMaps.DiscoverSearch("indomaret", -6.8986037, 107.6225108);
            s.results.items.ForEach(c => candidates.Add(c.ToCandidate()));
            s = await hereMaps.DiscoverSearch("alfamart", -6.8986037, 107.6225108);
            s.results.items.ForEach(c => candidates.Add(c.ToCandidate()));
            s = await hereMaps.DiscoverSearch("yomart", -6.8986037, 107.6225108);
            s.results.items.ForEach(c => candidates.Add(c.ToCandidate()));

            return candidates;
        }

        public static IEnumerable<Data.Candidate> GetCandidatesFromEga()
        {
            _rnd = _rnd ?? new Random();

            return new List<Data.Candidate>
            {
                new Data.Candidate //1
                {
                    Name = "Toko Pak Murtadoni",
                    Latitude = -6.86403947,
                    Longitude = 107.5753171,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //2
                {
                    Name = "Galon Mang Asoy",
                    Latitude = -6.900641198,
                    Longitude = 107.5909812,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //3
                {
                    Name = "Toko Pa Yayat Baru",
                    Latitude = -6.940847118,
                    Longitude = 107.5796351,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //4
                {
                    Name = "Raja Photocopy",
                    Latitude = -6.862496352,
                    Longitude = 107.5867151,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //5
                {
                    Name = "Rai Raka",
                    Latitude = -6.86985248,
                    Longitude = 107.5898845,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //6
                {
                    Name = "Gas dan Galon",
                    Latitude = -6.8977157,
                    Longitude = 107.6091094,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //7
                {
                    Name = "Toko Ibu Herni",
                    Latitude = -6.896358098,
                    Longitude = 107.6083481,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //8
                {
                    Name = "Toko StarQua",
                    Latitude = -6.894078733,
                    Longitude = 107.608031,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //9
                {
                    Name = "Toko Dewa",
                    Latitude = -6.88680419,
                    Longitude = 107.6164565,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //10
                {
                    Name = "Depot Isi Ulang",
                    Latitude = -6.885480755,
                    Longitude = 107.6225146,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //11
                {
                    Name = "Toko Konsumen",
                    Latitude = -6.86927942,
                    Longitude = 107.5880881,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //13
                {
                    Name = "Toko Drell",
                    Latitude = -6.90192339,
                    Longitude = 107.5953813,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //17
                {
                    Name = "Toko Olim",
                    Latitude = -6.90301174,
                    Longitude = 107.5959634,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //18
                {
                    Name = "Toko Aas Elektro",
                    Latitude = -6.888488118,
                    Longitude = 107.5910412,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //19
                {
                    Name = "Toko Wendi",
                    Latitude = -6.886436722,
                    Longitude = 107.5910217,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //20
                {
                    Name = "PD. Mitra",
                    Latitude = -6.890999509,
                    Longitude = 107.5910016,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //21
                {
                    Name = "Warung Aneka",
                    Latitude = -6.899506852,
                    Longitude = 107.58968,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //22
                {
                    Name = "Depo Gunawan",
                    Latitude = -6.891502784,
                    Longitude = 107.5909698,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //23
                {
                    Name = "Depo Air Minum",
                    Latitude = -6.891923511,
                    Longitude = 107.5908531,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //24
                {
                    Name = "Toko 113",
                    Latitude = -6.86285286,
                    Longitude = 107.5877853,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //25
                {
                    Name = "Warung Pak Asep",
                    Latitude = -6.88825504,
                    Longitude = 107.5971711,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //31
                {
                    Name = "Toko Mandiri",
                    Latitude = -6.944671193,
                    Longitude = 107.6669792,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //36
                {
                    Name = "Toko Samsudin",
                    Latitude = -6.55266,
                    Longitude = 107.35209,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //37
                {
                    Name = "Toko Wawan",
                    Latitude = -6.55255,
                    Longitude = 107.35207,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //38
                {
                    Name = "Ibu Iha",
                    Latitude = -6.55234,
                    Longitude = 107.35214,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //39
                {
                    Name = "Toko Ibu Hj Yayah",
                    Latitude = -6.55224,
                    Longitude = 107.35261,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //40
                {
                    Name = "Waserba Teh Yati",
                    Latitude = -6.55307,
                    Longitude = 107.35309,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //41
                {
                    Name = "Warung Teh Nova",
                    Latitude = -6.55281,
                    Longitude = 107.35309,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //42
                {
                    Name = "Warung Tante",
                    Latitude = -6.55256,
                    Longitude = 107.35342,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //43
                {
                    Name = "Warung Teh Ela",
                    Latitude = -6.55232,
                    Longitude = 107.3536,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //44
                {
                    Name = "Warung Bpk Ona",
                    Latitude = -6.55213,
                    Longitude = 107.35403,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //45
                {
                    Name = "Warung Ibu Lilis",
                    Latitude = -6.55227,
                    Longitude = 107.35454,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //46
                {
                    Name = "Warung Ibu Yuyun",
                    Latitude = -6.55303,
                    Longitude = 107.35473,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //47
                {
                    Name = "Warung Sinaga",
                    Latitude = -6.55257,
                    Longitude = 107.35407,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //48
                {
                    Name = "Warung Ibu Oca",
                    Latitude = -6.55303,
                    Longitude = 107.35368,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //49
                {
                    Name = "Warung Ibu Eni",
                    Latitude = -6.53363,
                    Longitude = 107.37041,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //50
                {
                    Name = "Warung Agus",
                    Latitude = -6.53353,
                    Longitude = 107.37059,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //51
                {
                    Name = "Depot Alwa",
                    Latitude = -6.53351,
                    Longitude = 107.3704,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //52
                {
                    Name = "Toko Rizki",
                    Latitude = -6.53307,
                    Longitude = 107.37001,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //53
                {
                    Name = "Warung Adit3",
                    Latitude = -6.53211,
                    Longitude = 107.37142,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //54
                {
                    Name = "Toko Ibu Engkas",
                    Latitude = -6.5324,
                    Longitude = 107.37128,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //55
                {
                    Name = "Warung Cece",
                    Latitude = -6.53276,
                    Longitude = 107.3715,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //56
                {
                    Name = "Toko AM",
                    Latitude = -6.51826,
                    Longitude = 107.37578,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //57
                {
                    Name = "Toko Tini",
                    Latitude = -6.51876,
                    Longitude = 107.37431,
                    Load = _rnd.Next(0, 10),
                },

                new Data.Candidate //58
                {
                    Name = "Warung Ibu Yati",
                    Latitude = -6.51804,
                    Longitude = 107.37399,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //59
                {
                    Name = "Toko Cece",
                    Latitude = -6.52359,
                    Longitude = 107.37035,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //60
                {
                    Name = "Toko Slamet",
                    Latitude = -6.52439,
                    Longitude = 107.37135,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //61
                {
                    Name = "Toko Habhab",
                    Latitude = -6.53319,
                    Longitude = 107.37158,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //62
                {
                    Name = "Toko Agam",
                    Latitude = -6.5264,
                    Longitude = 107.37055,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //63
                {
                    Name = "Toko Hikmat",
                    Latitude = -6.52714,
                    Longitude = 107.37007,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //64
                {
                    Name = "Toko 96",
                    Latitude = -6.52362,
                    Longitude = 107.36468,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //65
                {
                    Name = "Toko Herman",
                    Latitude = -6.938494,
                    Longitude = 107.750407,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //66
                {
                    Name = "Warung Pak Aih",
                    Latitude = -6.938777,
                    Longitude = 107.753543,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //67
                {
                    Name = "Toko Wawan",
                    Latitude = -6.939699,
                    Longitude = 107.74665,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //68
                {
                    Name = "Toko Adang",
                    Latitude = -6.939056,
                    Longitude = 107.7502,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //69
                {
                    Name = "Toko Supriatin",
                    Latitude = -6.937074,
                    Longitude = 107.750218,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //70
                {
                    Name = "Warung Fikri",
                    Latitude = -6.940095,
                    Longitude = 107.750323,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //71
                {
                    Name = "Toko Nabila",
                    Latitude = -6.936706,
                    Longitude = 107.75,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //72
                {
                    Name = "Warung mimin",
                    Latitude = -6.936511,
                    Longitude = 107.75037,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //73
                {
                    Name = "Toko faza",
                    Latitude = -6.934921,
                    Longitude = 107.750598,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //74
                {
                    Name = "Toko lima",
                    Latitude = -6.934641,
                    Longitude = 107.750411,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //75
                {
                    Name = "Toko agunsa",
                    Latitude = -6.931363,
                    Longitude = 107.750932,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //76
                {
                    Name = "Depot al-amien",
                    Latitude = -6.931267,
                    Longitude = 107.750951,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //77
                {
                    Name = "Warung ibu rifan",
                    Latitude = -6.930057,
                    Longitude = 107.751028,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //78
                {
                    Name = "Jus ibu risin",
                    Latitude = -6.929886,
                    Longitude = 107.751061,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //79
                {
                    Name = "Toko diva",
                    Latitude = -6.928682,
                    Longitude = 107.750888,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //80
                {
                    Name = "Toko yuyun",
                    Latitude = -6.927952,
                    Longitude = 107.750899,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //87
                {
                    Name = "Toko gunawan",
                    Latitude = -6.896862,
                    Longitude = 107.608998,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //88
                {
                    Name = "Toko ivan",
                    Latitude = -6.896767,
                    Longitude = 107.608752,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //89
                {
                    Name = "Toko kalom",
                    Latitude = -6.896603,
                    Longitude = 107.608628,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //90
                {
                    Name = "toko ria",
                    Latitude = -6.896977,
                    Longitude = 107.60643,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //91
                {
                    Name = "Toko ivan",
                    Latitude = -6.89748,
                    Longitude = 107.606598,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //92
                {
                    Name = "Toko bejo",
                    Latitude = -6.879348,
                    Longitude = 107.624236,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //93
                {
                    Name = "Toko jajang",
                    Latitude = -6.888411,
                    Longitude = 107.626661,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //94
                {
                    Name = "Toko kawali",
                    Latitude = -6.888454,
                    Longitude = 107.627124,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //95
                {
                    Name = "Toko erdi tarigan",
                    Latitude = -6.889123,
                    Longitude = 107.628749,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //96
                {
                    Name = "Toko cinday",
                    Latitude = -6.889242,
                    Longitude = 107.629222,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //97
                {
                    Name = "Toko muamalah",
                    Latitude = -6.878706,
                    Longitude = 107.638467,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //98
                {
                    Name = "Tasya aqua",
                    Latitude = -6.878665,
                    Longitude = 107.638353,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //99
                {
                    Name = "Toko serayu",
                    Latitude = -6.898699,
                    Longitude = 107.623686,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //100
                {
                    Name = "Andi barokah",
                    Latitude = -6.898356,
                    Longitude = 107623848,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //101
                {
                    Name = "Toko ujang",
                    Latitude = -6.869986,
                    Longitude = 107.584819,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //102
                {
                    Name = "Toko barokah",
                    Latitude = -6.872408,
                    Longitude = 107.584876,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //103
                {
                    Name = "Warung nyai",
                    Latitude = -6.873661,
                    Longitude = 107.584477,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //104
                {
                    Name = "Toko asep",
                    Latitude = -6.889541,
                    Longitude = 107.630822,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //105
                {
                    Name = "Toko Fajar",
                    Latitude = -6.889523,
                    Longitude = 107.630966,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //106
                {
                    Name = "Toko Rama",
                    Latitude = -6.889712,
                    Longitude = 107.631739,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //107
                {
                    Name = "Toko Yuki",
                    Latitude = -6.88972,
                    Longitude = 107.632148,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //108
                {
                    Name = "Toko M Dadan",
                    Latitude = -6.890337,
                    Longitude = 107.632403,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //109
                {
                    Name = "Toko Barokah",
                    Latitude = -6.890079,
                    Longitude = 107.632344,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //110
                {
                    Name = "Warung Bu Iis",
                    Latitude = -6.890193,
                    Longitude = 107.632293,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //111
                {
                    Name = "Toko Lingga",
                    Latitude = -6.891687,
                    Longitude = 107.633288,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //112
                {
                    Name = "Toko Fikri",
                    Latitude = -6.890888,
                    Longitude = 107.633427,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //113
                {
                    Name = "Toko Siska",
                    Latitude = -6.890728,
                    Longitude = 107.633385,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //114
                {
                    Name = "Toko Yeyeh",
                    Latitude = -6.896334,
                    Longitude = 107.633404,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //115
                {
                    Name = "Warung Siska",
                    Latitude = -6.890277,
                    Longitude = 107.633316,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //116
                {
                    Name = "Yoga Qua",
                    Latitude = -6.889682,
                    Longitude = 107.63361,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //117
                {
                    Name = "Warung Hafidz",
                    Latitude = -6.889615,
                    Longitude = 107.63367,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //118
                {
                    Name = "Toko Arham",
                    Latitude = -6.889167,
                    Longitude = 107.633858,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate //119
                {
                    Name = "Toko Fida",
                    Latitude = -6.88875,
                    Longitude = 107.633176,
                    Load = _rnd.Next(0, 10),
                },
            };
        }
    }
}
