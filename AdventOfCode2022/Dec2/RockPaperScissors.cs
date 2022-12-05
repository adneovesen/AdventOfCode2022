using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Dec2
{
    public class RockPaperScissors
    {
        public static int TotalScoreFromTournament(string path, bool partTwo = false)
        {
            var totalScore = 0;

            foreach(var line in File.ReadLines(path))
            {
                var character = line.Split(' ');
                var firstInput = GetValue(character[0]);
                var secondInput = 0;
                if (partTwo)
                {
                    secondInput = GetValueBasedOnStrategy(firstInput, character[1]);
                } else
                {
                    secondInput = GetValue(character[1]);
                }
                totalScore += CalculatePoints(firstInput, secondInput);
            }

            return totalScore;
        }

        private static int GetValueBasedOnStrategy(int firstInput, string v)
        {
            var returnInput = 0;
            if(v == "X")
            {
                returnInput = firstInput - 1;
            }
            if (v == "Y")
            {
                returnInput = firstInput;
            }
            if (v == "Z")
            {
                returnInput = firstInput + 1;
            }

            if (returnInput == 0) returnInput = 3;
            if (returnInput == 4) returnInput = 1;

            return returnInput;
        }

        private static int GetValue(string v)
        {
            if(v == "A" || v == "X")
            {
                return 1;
            }
            if (v == "B" || v == "Y")
            {
                return 2;
            }
            return 3;
        }

        private static int CalculatePoints(int firstInput, int secondInput)
        {
            if(firstInput +1 == secondInput || secondInput + 2 == firstInput)
            {
                return 6 + secondInput;
            }
            if (firstInput == secondInput){
                return 3 + secondInput;
            }
            return 0 + secondInput;
            
        }
    }
}
