using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Dec5
{
    public static class SupplyStacks
    {
        public static string RearrangementProcedure(string path, bool partTwo = false)
        {
            List<string> startingPoint = new List<string>();
            List<string> rearrengementProcedure = new List<string>();
            var procedureStarted = false;

            foreach(var line in File.ReadLines(path))
            {
                if (procedureStarted)
                {
                    rearrengementProcedure.Add(line);
                    continue;
                }
                if (string.IsNullOrWhiteSpace(line))
                {
                    procedureStarted = true;
                    continue;
                }
                startingPoint.Add(line);
            }

            var NumberOfStacks = (startingPoint[0].Length + 1) / 4; // its magic!
            string[] initialStacks = new string[NumberOfStacks];
            startingPoint.Reverse();
            foreach (var line in startingPoint)
            {
                var counter = 0;
                for(int i = 1; i < line.Length; i += 4)
                {
                    if(line[i] != ' ')
                    {
                        initialStacks[counter] += line[i];
                    } 
                    counter++;
                }
            }

            List<Stack<char>> stacks = CreateStacks(NumberOfStacks, initialStacks);

            foreach(var line in rearrengementProcedure)
            {
                var splitLine = line.Split(" ");
                var amount = int.Parse(splitLine[1]);
                var from = int.Parse(splitLine[3]) - 1;
                var to = int.Parse(splitLine[5]) - 1;

                if (partTwo)
                {
                    MoveCratesPartTwo(stacks, amount, from, to);
                } else
                {
                    MoveCreates(stacks, amount, from, to);
                }
                
            }

            var TopOfTheLine = "";
            foreach (var stack in stacks)
            {
                TopOfTheLine += stack.Pop();
            }

            return TopOfTheLine;
        }

        private static void MoveCratesPartTwo(List<Stack<char>> stacks, int amount, int from, int to)
        {
            var temp = "";
            for (var i = 0; i < amount; i++)
            {
                temp += stacks[from].Pop();
            }
            var newstring = temp.Reverse();
            foreach (var c in newstring)
            {
                stacks[to].Push(c);
            }
        }
        

        private static void MoveCreates(List<Stack<char>> stacks, int amount, int from, int to)
        {
            for(var i = 0; i < amount; i++)
            {
                var toBeMoved = stacks[from].Pop();
                stacks[to].Push(toBeMoved);
            }
        }

        private static List<Stack<char>> CreateStacks(int numberOfStacks, string[] initialStacks)
        {
            List<Stack<char>> stacks = new();
            for (int i = 0; i < numberOfStacks; i++)
            {
                stacks.Add(new Stack<char>());
                for(int j = 0; j < initialStacks[i].Length; j++)
                {
                    stacks[i].Push(initialStacks[i][j]);
                }
            }

            return stacks;
        }
    }
}
