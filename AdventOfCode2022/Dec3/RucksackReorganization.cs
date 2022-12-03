using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Dec3
{
    public class RucksackReorganization
    {
        public static int SumOfItemsInBothCompartments(string path)
        {
            var lines = File.ReadAllLines(path);
            var score = 0;
            foreach(var line in lines)
            {
                var firsthalf = line.Substring(0, line.Length / 2);
                var secondhalf = line.Substring(line.Length / 2, line.Length / 2);

                // not ideal or most effective way of finding the union
                var common = firsthalf.Intersect(secondhalf);

                score += FindScore(common);
            }

            return score;
        }

        public static int SumOfBadgePriorities(string path)
        {
            var lines = File.ReadAllLines(path);
            var score = 0;
            for(int i = 0; i < lines.Length ; i += 3)
            {
                // this is some jank ass shit, if it works give me a medal 
                var common = lines[i].Intersect(lines[i + 1].Intersect(lines[i + 2]));

                score += FindScore(common);
            }

            return score;
        }

        private static int FindScore(IEnumerable<char> common)
        {
            var apetor = (int)common.ElementAt(0);
            if(apetor > 95)
            {
                return apetor - 96;
            }
            return apetor - 38;
        }
    }
}
