using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            // Q1  
            string[] lines = File.ReadAllLines("../../files/Q1.txt");
            Console.WriteLine("Jour 1: " + getResponseQ1(lines));

            //Q2
            string lines2 = File.ReadAllText("../../files/Q2.txt");
            Console.WriteLine("Jour 2: " + getResponseQ2(lines2));
            Console.WriteLine("Jour 2 Partie 2: " + getReponse2Part2(lines2));

            //Q3
            string[] lines3 = File.ReadAllLines("../../files/Q3.txt");
            //Console.WriteLine("Jour 3: " + getResponseQ3(lines3));
            //Console.WriteLine("Jour 3 Partie 2:" + getResponseQ3Part2(lines3));

            //Q4
            Console.WriteLine("Jour 4: " + GetResponseQ4(165432, 707912));
            Console.WriteLine("Jour 4: " + GetResponseQ4Part2(165432, 707912));

            Console.ReadLine();
        }

        public static int getResponseQ1(string[] ligneFichier)
        {
            var sum = 0;

            foreach (string line in ligneFichier)
            {
                var chiffre = Convert.ToInt32(Convert.ToInt32(line) / 3);
                chiffre = chiffre -2;
                sum += chiffre;
                var temp = chiffre;
                while (temp > 0)
                {
                    temp = Convert.ToInt32(temp / 3);
                    temp -= 2;
                    if (temp > 0)
                    {
                        sum += temp;
                    }
                    
                }
            }

            return sum;
        }

        public static int getResponseQ2(string fichier)
        {
            var file = fichier.Split(',');

            int[] intArray = Array.ConvertAll(file, delegate (string s) { return int.Parse(s); });

            intArray[1] = 12;
            intArray[2] = 2;
            var key = 0;
            var next = 0;

            foreach (var number in intArray)
            {
                if (next == key)
                {
                    if (number == 1)
                    {
                        var index1Add = intArray[key + 1];
                        var index2Add = intArray[key + 2];
                        var index3Rem = intArray[key + 3];

                        intArray[index3Rem] = intArray[index1Add] + intArray[index2Add];
                        next = key + 4;
                    }
                    else if (number == 2)
                    {
                        var index1Add = intArray[key + 1];
                        var index2Add = intArray[key + 2];
                        var index3Rem = intArray[key + 3];

                        intArray[index3Rem] = intArray[index1Add] * intArray[index2Add];
                        next = key + 4;
                    }
                    else if (number == 99)
                    {
                        break;
                    }
                }
                key++;
            }

       
            return intArray[0];
        }

        public static int getReponse2Part2(string fichier)
        {
            var reponse = 0;
            foreach (var noun in Enumerable.Range(0, 100))
            {
                foreach (var verb in Enumerable.Range(0, 100))
                {
                    var intArray = Array.ConvertAll(fichier.Split(','), delegate (string s) { return int.Parse(s); });
                    intArray[1] = noun;
                    intArray[2] = verb;
                    int pos = 0;
                    while (intArray[pos] != 99)
                    {
                        int op = intArray[pos];
                        if (op == 1)
                        {
                            intArray[intArray[pos + 3]] = intArray[intArray[pos + 2]] + intArray[intArray[pos + 1]];
                        }
                        else if (op == 2)
                        {
                            intArray[intArray[pos + 3]] = intArray[intArray[pos + 2]] * intArray[intArray[pos + 1]];
                        }
                        pos += 4;
                    }

                    if (intArray[0] == 19690720)
                    {
                        reponse = (100 * noun + verb);
                    }
                }
            }
            return reponse;
        }



        public static List<int[]> lstCoordonner(string ligneFichier)
        {
            var lstInt = new List<int[]>();

            var tmp = ligneFichier.Split(',');
            var i = 0;
            var first = true;

            foreach (var coord in tmp)
            {
                for (var j = 0; j < Convert.ToInt32(coord.Substring(1)); j++)
                {
                    var x = 0;
                    var y = 0;

                    if (!first)
                    {
                        x = lstInt.ElementAt(lstInt.Count - 1)[0];
                        y = lstInt.ElementAt(lstInt.Count - 1)[1];
                    }

                    switch (coord.Substring(0, 1))
                    {
                        case "R":
                            x++;
                            break;
                        case "U":
                            y++;
                            break;
                        case "D":
                            y--;
                            break;
                        case "L":
                            x--;
                            break;
                    }

                    var tmpint = new int[2] { x, y };
                    lstInt.Add(tmpint);
                    first = false;
                }

                i++;
            }
            return lstInt;
        }

        public static int getResponseQ3(string[] fichier)
        {
            var lstCoord = new List<List<int[]>>();
            var lstIntersect = new List<int[]>();
            var distance = 0;

            foreach (string line in fichier)
            {
                lstCoord.Add(lstCoordonner(line));
            }


            for (var i = 0; i < lstCoord[0].Count; i++)
            {
                for (var j = 0; j < lstCoord[1].Count; j++)
                {
                    if (lstCoord[0][i][0] == lstCoord[1][j][0] && lstCoord[0][i][1] == lstCoord[1][j][1])
                    {
                        lstIntersect.Add(lstCoord[0][i]);
                    }
                }
            }

            for (var k = 0; k<lstIntersect.Count;k++)
            {
                if (Math.Abs(lstIntersect[k][0] + lstIntersect[k][1]) < distance || distance == 0)
                {
                    distance = Math.Abs(lstIntersect[k][0] + lstIntersect[k][1]);
                }
            }

            return distance;
        }

        public static int getResponseQ3Part2(string[] fichier)
        {
            var lstCoord = new List<List<int[]>>();
            var lstIntersect = new List<int[]>();
            var distance = 0;

            foreach (string line in fichier)
            {
                lstCoord.Add(lstCoordonnerDistance(line));
            }


            for (var i = 0; i < lstCoord[0].Count; i++)
            {
                for (var j = 0; j < lstCoord[1].Count; j++)
                {
                    if (lstCoord[0][i][0] == lstCoord[1][j][0] && lstCoord[0][i][1] == lstCoord[1][j][1])
                    {
                        var lstTmp = new int[4] { lstCoord[0][i][0], lstCoord[0][i][1], lstCoord[0][i][2] + lstCoord[1][j][2], lstCoord[0][i][3] + lstCoord[1][j][3] };
                        lstIntersect.Add(lstTmp);
                    }
                }
            }

            for (var k = 0; k < lstIntersect.Count; k++)
            {
                if (Math.Abs(lstIntersect[k][2] + lstIntersect[k][3]) < distance || distance == 0)
                {
                    distance = Math.Abs(lstIntersect[k][2] + lstIntersect[k][3]);
                }
            }

            return distance;
        }

        public static List<int[]> lstCoordonnerDistance(string ligneFichier)
        {
            var lstInt = new List<int[]>();

            var tmp = ligneFichier.Split(',');
            var i = 0;
            var first = true;
            var stepX = 0;
            var stepY = 0;

            foreach (var coord in tmp)
            {
                for (var j = 0; j < Convert.ToInt32(coord.Substring(1)); j++)
                {
                    var x = 0;
                    var y = 0;

                    if (!first)
                    {
                        x = lstInt.ElementAt(lstInt.Count - 1)[0];
                        y = lstInt.ElementAt(lstInt.Count - 1)[1];
                    }

                    switch (coord.Substring(0, 1))
                    {
                        case "R":
                            x++;
                            stepX++;
                            break;
                        case "U":
                            y++;
                            stepY++;
                            break;
                        case "D":
                            y--;
                            stepY++;
                            break;
                        case "L":
                            x--;
                            stepX++;
                            break;
                    }

                    var tmpint = new int[4] { x, y, stepX, stepY};
                    lstInt.Add(tmpint);
                    first = false;
                }

                i++;
            }
            return lstInt;
        }

        public static int GetResponseQ4(Int32 min, Int32 max)
        {
            var reponse = 0;

            for (var i = min; i <= max; i++)
            {
                var rule1 = false;
                var rule2 = true;
                for(var j = 0; j < i.ToString().Length - 1; j++)
                {
                    if (i.ToString()[j] == i.ToString()[j+1])
                    {
                        rule1 = true;
                    }

                    if (Convert.ToInt32(i.ToString()[j]) > Convert.ToInt32(i.ToString()[j + 1]))
                    {
                        rule2 = false;
                    }
                }

                if (rule1 && rule2)
                {
                    reponse++;
                }
            }

            return reponse;
        }

        public static int GetResponseQ4Part2(Int32 min, Int32 max)
        {
            var reponse = 0;
            for (var i = min; i <= max; i++)
            {
                var rule1 = false;
                var rule2 = true;
                var group = 0;

                for (var j = 0; j < i.ToString().Length - 1; j++)
                {
                    if (i.ToString()[j] == i.ToString()[j + 1])
                    {
                        group++;
                    } else
                    {
                        if (group == 1)
                        {
                            rule1 = true;
                        }
                        group = 0;
                    }

                    if (Convert.ToInt32(i.ToString()[j]) > Convert.ToInt32(i.ToString()[j + 1]))
                    {
                        rule2 = false;
                    }
                }

                if (group == 1) {
                    rule1 = true;
                }

                if (rule1 && rule2)
                {
                    reponse++;
                }
            }

            return reponse;
        }
    }
}
