using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Service.Data
{
    class MenegeDistance
    {
        public static int Distance(string plaA, string plaB)
        {
            string place1 = plaA;
            string place2 = plaB;
            if (place1 == place2) throw new Exception("the places is same");
            string AtoB = place1 + "&" + place2;
            string BtoA = place2 + "&" + place1;
            string Textresult = "0";
            int IntResult = 0;

            string projPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            string filePath = Path.Combine(projPath, "RealDistance.txt");

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }
            else if (File.Exists(filePath))
            {
                string lineToWrite = null;
                string[] lines = File.ReadAllLines(filePath);
                int corentLine = 0;
                while (corentLine < lines.Length)
                {
                    lineToWrite = lines[corentLine++];
                    if (lineToWrite == AtoB || lineToWrite == BtoA)
                    {
                        Textresult = lines[corentLine++];
                        if (!Int32.TryParse(Textresult, out IntResult)) throw new Exception("error in format distance:" + Textresult);
                        return IntResult;
                    }
                }
            }
            if (Textresult == "0") IntResult = HelpFuncCreateNewRoute(AtoB, plaA, plaB);
            return IntResult;
        }
        private static int HelpFuncCreateNewRoute(string citys, string A, string B)
        {
            int dist;
            try
            {
                dist = (int)UI.Service.Data.mapquest.DistanceFromServer(A, B);
            }catch { dist = 100; }
            string projPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            string filePath = Path.Combine(projPath, "RealDistance.txt");

            try
            {
                File.AppendAllText(filePath,
                   citys + Environment.NewLine);
                File.AppendAllText(filePath,
                          dist.ToString() + Environment.NewLine);
            }
            catch { throw new Exception("Erorr with write to file"); }
            return dist;
        }
    }
}
