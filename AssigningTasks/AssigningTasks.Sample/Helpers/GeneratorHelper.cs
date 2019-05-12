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
    
        public static IEnumerable<Data.Candidate> GetDewiJayaTeknikServiceArea()
        {
            _rnd = _rnd ?? new Random();

            var candidates = new List<Data.Candidate>
            {
                new Data.Candidate 
                {
                    Name = "Cigending, Ujung Berung",
                    Latitude = -6.910042,
                    Longitude = 107.697958,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Pasanggrahan, Ujung Berung",
                    Latitude = -6.910130,
                    Longitude = 107.714603,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Pasir Endah, Ujung Berung",
                    Latitude = -6.903078,
                    Longitude = 107.689689,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Pasirjati, Ujung Berung",
                    Latitude = -6.902496,
                    Longitude = 107.708974,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Pasirwangi, Ujung Berung",
                    Latitude = -6.900765,
                    Longitude = 107.705430,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Ujung Berung, Ujung Berung",
                    Latitude = -6.912177,
                    Longitude = 107.702669,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Babakan Ciamis, Sumur Bandung",
                    Latitude = -6.909770,
                    Longitude = 107.608430,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Braga, Sumur Bandung",
                    Latitude =  -6.919881,
                    Longitude =  107.606456,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Kebon Pisang, Sumur Bandung",
                    Latitude =  -6.918332,
                    Longitude =  107.617944,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Merdeka, Sumur Bandung",
                    Latitude =  -6.913713,
                    Longitude =  107.618639,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Geger Kalong, Sukasari",
                    Latitude =  -6.869881,
                    Longitude =  107.592063,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Isola, Sukasari",
                    Latitude =  -6.851284,
                    Longitude =  107.591463,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sarijadi, Sukasari",
                    Latitude =  -6.880372,
                    Longitude =  107.579319,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sukarasa, Sukasari",
                    Latitude =  -6.873500,
                    Longitude =  107.586193,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cipedes, Sukajadi",
                    Latitude =  -6.886655,
                    Longitude =  107.595731,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Pasteur, Sukajadi",
                    Latitude =  -6.889101,
                    Longitude =  107.598270,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sukabungah, Sukajadi",
                    Latitude =  -6.897830,
                    Longitude =  107.594676,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sukagalih, Sukajadi",
                    Latitude =  -6.885340,
                    Longitude =  107.587898,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sukawarna, Sukajadi",
                    Latitude =  -6.881981,
                    Longitude =  107.579437,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Ancol, Regol",
                    Latitude =  -6.942096,
                    Longitude =  107.616239,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Balong Gede, Regol",
                    Latitude =  -6.926876,
                    Longitude =  107.606917,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Ciateul, Regol",
                    Latitude =  -6.935396,
                    Longitude =  107.608419,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cigereleng, Regol",
                    Latitude =  -6.938676,
                    Longitude =  107.610410,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Ciseureuh, Regol",
                    Latitude =  -6.953305,
                    Longitude =  107.611596,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Pasirluyu, Regol",
                    Latitude =  -6.947869,
                    Longitude =  107.621301,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Pungkur, Regol",
                    Latitude =  -6.929538,
                    Longitude =  107.606785,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cipamokolan, Rancasari",
                    Latitude =  -6.944167,
                    Longitude =  107.676207,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Darwati, Rancasari",
                    Latitude =  -6.964824,
                    Longitude =  107.682739,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Manjahlega, Rancasari",
                    Latitude =  -6.942225,
                    Longitude =  107.667456,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Mekar Mulya, Rancasari",
                    Latitude =  -6.935293,
                    Longitude =  107.698507,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cipadung Kidul, Panyileukan",
                    Latitude =  -6.936456,
                    Longitude =  107.712206,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cipadung Kulon, Panyileukan",
                    Latitude =  -6.920473,
                    Longitude =  107.706239,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cipadung Wetan, Panyileukan",
                    Latitude =  -6.929430,
                    Longitude =  107.711710,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Mekarmulya, Panyileukan",
                    Latitude =  -6.933943,
                    Longitude =  107.702697,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Jatihandap, Mandalajati",
                    Latitude =  -6.902169,
                    Longitude =  107.660943,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Karang Pamulang, Mandalajati",
                    Latitude =  -6.902194,
                    Longitude =  107.668367,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Pasir Impun, Mandalajati",
                    Latitude =  -6.900951,
                    Longitude =  107.680469,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sindang Jaya, Mandalajati",
                    Latitude =  -6.904840,
                    Longitude =  107.682394,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Burangrang, Lengkong",
                    Latitude =  -6.931382,
                    Longitude =  107.616355,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cijagra, Lengkong",
                    Latitude =  -6.942544,
                    Longitude =  107.627311,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cikawao, Lengkong",
                    Latitude =  -6.926996,
                    Longitude =  107.613013,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Lingkar Selatan, Lengkong",
                    Latitude =  -6.926207,
                    Longitude =  107.627634,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Malabar, Lengkong",
                    Latitude =  -6.923240,
                    Longitude =  107.622375,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Paledang, Lengkong",
                    Latitude =  -6.926626,
                    Longitude =  107.616825,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Turangga, Lengkong",
                    Latitude =  -6.938200,
                    Longitude =  107.627220,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Babakan Sari, Kiaracondong",
                    Latitude =  -6.924051,
                    Longitude =  107.649108,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Babakan Surabaya, Kiaracondong",
                    Latitude =  -6.914189,
                    Longitude =  107.648933,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cicaheum, Kiaracondong",
                    Latitude =  -6.905621,
                    Longitude =  107.650311,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Kebon Kangkung, Kiaracondong",
                    Latitude =  -6.940376,
                    Longitude =  107.642500,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Kebun Jayanti, Kiaracondong",
                    Latitude =  -6.926634,
                    Longitude =  107.647446,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sukapura, Kiaracondong",
                    Latitude =  -6.930987,
                    Longitude =  107.653663,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cimenerang, Gedebage",
                    Latitude =  -6.944540,
                    Longitude =  107.705983,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cisaranten Kidul, Gedebage",
                    Latitude =  -6.949423,
                    Longitude =  107.687773,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Rancabalong, Gedebage",
                    Latitude =  -6.956322,
                    Longitude =  107.686709,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Rancanumpang, Gedebage",
                    Latitude =  -6.961214,
                    Longitude =  107.710260,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cipaganti, Coblong",
                    Latitude =  -6.891002,
                    Longitude =  107.604047,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Dago, Coblong",
                    Latitude =  -6.876155,
                    Longitude =  107.617975,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Lebak Gede, Coblong",
                    Latitude =  -6.890924,
                    Longitude =  107.616949,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Lebak Siliwangi, Coblong",
                    Latitude =  -6.894407,
                    Longitude =  107.611915,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sadang Serang, Coblong",
                    Latitude =  -6.895760,
                    Longitude =  107.625092,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sekeloa, Coblong, Cinambo",
                    Latitude =  -6.884752,
                    Longitude =  107.621159,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Babakan Penghulu, Cinambo",
                    Latitude =  -6.937993,
                    Longitude =  107.692352,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cisaranten Wetan, Cinambo",
                    Latitude =  -6.923160,
                    Longitude =  107.687716,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Pakemitan, Cinambo",
                    Latitude =  -6.920701,
                    Longitude =  107.697206,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sukamuly, Cinamboa",
                    Latitude =  -6.917968,
                    Longitude =  107.699753,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Ciumbuleuit, Cidadap",
                    Latitude =  -6.863245,
                    Longitude =  107.618827,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Hegarmanah, Cidadap",
                    Latitude =  -6.871567,
                    Longitude =  107.600611,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Ledeng, Cidadap, Cicendo",
                    Latitude =  -6.854624,
                    Longitude =  107.597386,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Arjuna, Cicendo",
                    Latitude =  -6.909106,
                    Longitude =  107.594858,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Husen Sastranegara, Cicendo",
                    Latitude =  -6.907964,
                    Longitude =  107.581873,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Pajajaran, Cicendo",
                    Latitude =  -6.897889,
                    Longitude =  107.587469,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Pamoyanan, Cicendo",
                    Latitude =  -6.900088,
                    Longitude =  107.594681,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Pasir Kaliki, Cicendo",
                    Latitude =  -6.906995,
                    Longitude =  107.599941,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sukaraja, Cicendo",
                    Latitude =  -6.893266,
                    Longitude =  107.569997,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cipadung, Cibiru",
                    Latitude =  -6.926333,
                    Longitude =  107.718050,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cisurupan, Cibiru",
                    Latitude =  -6.909986,
                    Longitude =  107.723214,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Palasari, Cibiru",
                    Latitude =  -6.917049,
                    Longitude =  107.719402,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Pasirbiru, Cibiru",
                    Latitude =  -6.930940,
                    Longitude =  107.721786,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cicadas, Cibeunying Kidul",
                    Latitude =  -6.904162,
                    Longitude =  107.636649,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cikutra, Cibeunying Kidul",
                    Latitude =  -6.905414,
                    Longitude =  107.643668,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Padasuka, Cibeunying Kidul",
                    Latitude =  -6.903687,
                    Longitude =  107.649505,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Pasirlayung, Cibeunying Kidul",
                    Latitude =  -6.895465,
                    Longitude =  107.655983,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sukamaju, Cibeunying Kidul",
                    Latitude =  -6.907776,
                    Longitude =  107.631880,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sukapada, Cibeunying Kidul",
                    Latitude =  -6.892714,
                    Longitude =  107.644758,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cigadung, Cibeunying Kidul",
                    Latitude =  -6.884800,
                    Longitude =  107.629480,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cihaur Geulis, Cibeunying Kidul",
                    Latitude =  -6.900877,
                    Longitude =  107.627311,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Neglasari, Cibeunying Kidul",
                    Latitude =  -6.893544,
                    Longitude =  107.639196,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sukaluyu, Cibeunying Kidul",
                    Latitude =  -6.894217,
                    Longitude =  107.632231,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cijaura, Buahbatu",
                    Latitude =  -6.958049,
                    Longitude =  107.652211,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Jatisari, Buahbatu",
                    Latitude =  -6.339957,
                    Longitude =  106.945024,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Margasari, Buahbatu",
                    Latitude =  -6.950069,
                    Longitude =  107.651934,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sekejati, Buahbatu",
                    Latitude =  -6.943848,
                    Longitude =  107.653902,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cibaduyut, Bojongloa Kidul",
                    Latitude =  6.953652,
                    Longitude =  107.592778,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cibaduyut Kidul, Bojongloa Kidul",
                    Latitude =  -6.960076,
                    Longitude =  107.589892,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cibaduyut Wetan, Bojongloa Kidul",
                    Latitude =  -6.959682,
                    Longitude =  107.600138,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Kebon Lega, Bojongloa Kidul",
                    Latitude =  -6.946063,
                    Longitude =  107.598552,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Mekarwangi, Bojongloa Kidul",
                    Latitude =  -6.954563,
                    Longitude =  107.605891,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Situsaeur, Bojongloa Kidul",
                    Latitude =  -6.942869,
                    Longitude =  107.596307,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Babakan Asih, Bojongloa Kaler",
                    Latitude =  -6.933180,
                    Longitude =  107.596047,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Babakan Tarogong, Bojongloa Kaler",
                    Latitude =  -6.929904,
                    Longitude =  107.590635,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Jamika, Bojongloa Kaler",
                    Latitude =  -6.922157,
                    Longitude =  107.586695,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Kopo, Bojongloa Kaler",
                    Latitude =  -6.941845,
                    Longitude =  107.590818,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Suka Asih, Bojongloa Kaler",
                    Latitude =  -6.932276,
                    Longitude =  107.590141,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Binong, Batununggal",
                    Latitude =  -6.939480,
                    Longitude =  107.642025,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cibangkong, Batununggal",
                    Latitude =  -6.923035,
                    Longitude =  107.633016,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Gumuruh, Batununggal",
                    Latitude =  -6.943820,
                    Longitude =  107.637235,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Kacapiring, Batununggal",
                    Latitude =  -6.914298,
                    Longitude =  107.634559,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Kebon Gedang, Batununggal",
                    Latitude =  -6.924710,
                    Longitude =  107.643140,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Kebonwaru, Batununggal",
                    Latitude =  -6.914215,
                    Longitude =  107.641342,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Maleer, Batununggal",
                    Latitude =  -6.929043,
                    Longitude =  107.638719,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Samoja, Batununggal",
                    Latitude =  -6.921484,
                    Longitude =  107.627610,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cihapit, Bandung Wetan",
                    Latitude =  -6.905826,
                    Longitude =  107.626632,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Citarum, Bandung Wetan",
                    Latitude =  -6.904051,
                    Longitude =  107.621097,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Tamansari, Bandung Wetan",
                    Latitude =  -6.899329,
                    Longitude =  107.608415,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Caringin, Bandung Kulon",
                    Latitude =  -6.929838,
                    Longitude =  107.572971,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cibuntu, Bandung Kulon",
                    Latitude =  -6.920946,
                    Longitude =  107.572336,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cigondewah Kaler, Bandung Kulon",
                    Latitude =  -6.935165,
                    Longitude =  107.564715,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cigondewah Kidul, Bandung Kulon",
                    Latitude =  -6.942508,
                    Longitude =  107.563660,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cigondewah Rahayu, Bandung Kulon",
                    Latitude =  -6.947160,
                    Longitude =  107.564605,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cijerah, Bandung Kulon",
                    Latitude =  -6.926409,
                    Longitude =  107.564533,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Gempolsari, Bandung Kulon",
                    Latitude =  -6.929059,
                    Longitude =  107.549745,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Warung Muncang, Bandung Kulon",
                    Latitude =  -6.923967,
                    Longitude =  107.576465,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Batununggal, Bandung Kidul",
                    Latitude =  -6.917712,
                    Longitude =  107.643165,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Kujangsari, Bandung Kidul",
                    Latitude =  -6.959592,
                    Longitude =  107.643952,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Mengger, Bandung Kidul",
                    Latitude =  -6.961782,
                    Longitude =  107.626033 ,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Wates, Bandung Kidul",
                    Latitude =  -6.959372,
                    Longitude =  107.613836,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Babakan, Babakan Ciparay",
                    Latitude =  -6.930383,
                    Longitude =  107.577132,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Babakan Ciparay, Babakan Ciparay",
                    Latitude =  -6.940788,
                    Longitude =  107.570619,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cirangrang, Babakan Ciparay",
                    Latitude =  -6.956185,
                    Longitude =  107.583620,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Margahayu Utara, Babakan Ciparay",
                    Latitude =  -6.949072,
                    Longitude =  107.576354,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Margasuka, Babakan Ciparay",
                    Latitude =  -6.950586,
                    Longitude =  107.571990,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sukahaji, Babakan Ciparay",
                    Latitude =  -6.927155,
                    Longitude =  107.583545,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cibadak, Astana Anyar",
                    Latitude =  -6.921048,
                    Longitude =  107.596113,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Karanganyar, Astana Anyar",
                    Latitude =  -6.923329,
                    Longitude =  107.600475,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Karasak, Astana Anyar",
                    Latitude =  -6.949888,
                    Longitude =  107.608361,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Nyengseret, Astana Anyar",
                    Latitude =  -6.930499,
                    Longitude =  107.601285,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Panjunan, Astana Anyar",
                    Latitude =  -6.930148,
                    Longitude =  107.597797,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Pelindung Hewan, Astana Anyar",
                    Latitude =  -6.940266,
                    Longitude =  107.603725,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cisaranten Endah, Arcamanik",
                    Latitude =  -6.931359,
                    Longitude =  107.673182,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cisaranten Kulon, Arcamanik",
                    Latitude =  -6.929037,
                    Longitude =  107.680551,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Cisarenten Bina Harapan, Arcamanik",
                    Latitude =  -6.911034,
                    Longitude =  107.683749,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sindang Jaya, Arcamanik",
                    Latitude =  -6.899834,
                    Longitude =  107.681733,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Sukamiskin, Arcamanik",
                    Latitude =  -6.917140,
                    Longitude =  107.674316,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Antapani Kidul, Antapani",
                    Latitude =  -6.922296,
                    Longitude =  107.659989,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Antapani Kulon, Antapani",
                    Latitude =  -6.910534,
                    Longitude =  107.657512,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Antapani Tengah, Antapani",
                    Latitude =  -6.916557,
                    Longitude =  107.661617,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Antapani Wetan, Antapani",
                    Latitude =  -6.907605,
                    Longitude =  107.662764,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Campaka",
                    Latitude =  -6.903414,
                    Longitude =  107.565736,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Ciroyom",
                    Latitude =  -6.915092,
                    Longitude =  107.588192,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Dungus Cariang",
                    Latitude =  -6.914470,
                    Longitude =  107.580742,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Garuda",
                    Latitude =  -6.911332,
                    Longitude =  107.577001,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Kebon Jeruk",
                    Latitude =  -6.917184,
                    Longitude =  107.596489,
                    Load = _rnd.Next(0, 10),
                },
                new Data.Candidate 
                {
                    Name = "Maleber",
                    Latitude =  -6.909374,
                    Longitude =  107.572800,
                    Load = _rnd.Next(0, 10),
                },
            };

            candidates.ForEach(c => c.TotalTravel = Helpers.GeneratorHelper.GenerateTotalTravel(c.Load));
            return candidates;
        }
    }
}
