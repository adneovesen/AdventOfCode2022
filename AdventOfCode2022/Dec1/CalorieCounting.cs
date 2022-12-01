using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Dec1
{
    public class CalorieCounting
    {
        public static int GetCaloriesFromElf(string path)
        {
            string[] lines = File.ReadAllLines(path);
            
            var calorie = 0;
            var calories = new List<int>();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    calories.Add(calorie);
                    calorie = 0;
                    continue;
                }
                calorie += int.Parse(line);
            }
            calories.Add(calorie);

            calories.Sort((a, b) => b.CompareTo(a));

            return calories[0];
        }

        public static int GetCaloriesFromTopThreeElves(string path)
        {
            string[] lines = File.ReadAllLines(path);
            
            var calorie = 0;
            var calories = new List<int>();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    calories.Add(calorie);
                    calorie = 0;
                    continue;
                }
                calorie += int.Parse(line);
            }
            calories.Add(calorie);

            calories.Sort((a, b) => b.CompareTo(a));
            
            return calories[0] + calories[1] + calories[2];
        }
    }
}
