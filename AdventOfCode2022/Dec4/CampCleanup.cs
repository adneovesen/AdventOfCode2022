using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Dec4
{
    public class CampCleanup
    {
        public static int RangeFullyContained(string path)
        {
            var lines = File.ReadAllLines(path);
            var score = 0;

            foreach(var line in lines)
            {
                var sections = line.Split(",");
                var firstSection = sections[0].Split("-").Select(int.Parse).ToArray();
                var secondSection = sections[1].Split("-").Select(int.Parse).ToArray();

                if (firstSection[0] == secondSection[0] || firstSection[1] == secondSection[1])
                {
                    score++;
                    continue;
                }
                if(firstSection[0] < secondSection[0])
                {
                    if(firstSection[1] > secondSection[1])
                    {
                        score++;
                        continue;
                    }
                }
                if (firstSection[0] > secondSection[0])
                {
                    if (firstSection[1] < secondSection[1])
                    {
                        score++;
                        continue;
                    }
                }
            }

            return score;
        }

        public static int RangeOverlap(string path)
        {
            var lines = File.ReadAllLines(path);
            var score = 0;

            foreach (var line in lines)
            {
                var sections = line.Split(",");

                var firstSection = sections[0].Split("-").Select(int.Parse).ToArray();
                var secondSection = sections[1].Split("-").Select(int.Parse).ToArray();

                var jankestBooleanValue = false;

                for(int i = firstSection[0]; i <= firstSection[1]; i++)
                {
                    for (int j = secondSection[0]; j <= secondSection[1]; j++)
                    {
                        if(i == j)
                        {
                            score++;
                            jankestBooleanValue = true;
                            break;
                        }
                    }
                    if (jankestBooleanValue) break;
                }
            }

            return score;
        }
    }
}
