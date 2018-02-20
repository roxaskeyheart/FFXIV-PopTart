using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;


namespace FFXIV_PopTart
{
    class APIHelper
    {
        public static void GetAPIData()
        {
            var _ContentRoulette = GetCsvData(@"https://raw.githubusercontent.com/viion/ffxiv-datamining/master/csv/ContentRoulette.csv", @"ContentRoulette.csv");
            var _InstanceContent = GetCsvData(@"https://raw.githubusercontent.com/viion/ffxiv-datamining/master/csv/InstanceContent.csv", @"InstanceContent.csv");
            var _InstanceMap = GetCsvData(@"https://raw.githubusercontent.com/viion/ffxiv-datamining/master/csv/ContentFinderCondition.csv", @"InstanceMap.csv");

            GenerateInstanceTable(_InstanceContent);
            GenerateMapTable(_InstanceMap);
        }

        private static string GetCsvData(string url, string csvPath)
        {
            var client = new WebClient();
            var enviroment = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            var path = enviroment + @"/" + csvPath;

            client.DownloadFile(new Uri(url), path);

            if (File.Exists(path))
            {
                return path;
            }
            
            Console.WriteLine(@"An error occurred downloading the file " + csvPath + @" from URI: " + url);
            return string.Empty;
        }

        private static void GenerateInstanceTable(string file)
        {
            var delimiters = new[] { ',' };
            using (var reader = new StreamReader(file))
            {
                int lineptr = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    lineptr++;

                    if (lineptr < 5) continue;

                    if (line == null)
                    {
                        break;
                    }

                    var parts = line.Split(delimiters);

                    if (parts[0] == null && parts[0] == "") continue;
                    if (parts[4] == null && parts[4] == "") continue;

                    if (!int.TryParse(parts[0], out var id)) continue;
                    if (!Models.InstanceContentModel.ContainsKey(id))
                    {
                        Models.InstanceContentModel.Add(id, parts[4]?.Replace("\"", ""));
                        //Console.WriteLine(id + @"//" + parts[4]); s = s.Replace('"', '');
                    }
                }
            }
        }

        private static void GenerateMapTable(string file)
        {
            var delimiters = new[] { ',' };
            using (var reader = new StreamReader(file))
            {
                int lineptr = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    lineptr++;

                    if (lineptr < 4) continue;

                    if (line == null)
                    {
                        break;
                    }

                    var parts = line.Split(delimiters);

                    if (parts[0] == null && parts[0] == "") continue;
                    if (parts[3] == null && parts[3] == "") continue;

                    if (!int.TryParse(parts[0], out var id)) continue;
                    if (!int.TryParse(parts[3], out var map)) continue;
                    if (!Models.ContentMap.ContainsKey(id))
                    {
                        Models.ContentMap.Add(id, map);
                    }
                }
            }
        }

        public static string GetInstanceName(int id)
        {
            if (!Models.ContentMap.ContainsKey(id))
            {
                return @"invalid";
            }

            var instanceID = Models.ContentMap[id];

            if (!Models.InstanceContentModel.ContainsKey(instanceID))
            {
                return @"invalid";
            }

            return Models.InstanceContentModel[instanceID];
        }

    }
}
