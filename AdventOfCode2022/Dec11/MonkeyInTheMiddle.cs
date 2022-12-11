using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022.De11
{
    public class MonkeyInTheMiddle
    {
        public static int MonkeyBussiness(string path)
        {
            var monkeys = new List<Monkey>();
            var stringMonkeys = File.ReadAllText(path).Split("\r\n\r\n");
            foreach (var stringmonkey in stringMonkeys)
            {
                var monkeyLines = stringmonkey.Split("\n");
                var monkey = new Monkey();

                for (var i = 1; i < monkeyLines.Length; i++)
                {
                    var line = monkeyLines[i].Split("\r")[0].Trim();
                    switch (i)
                    {
                        case 1:
                            var matches = Regex.Matches(line, @"\d+");
                            var numbers = matches.Cast<Match>().Select(match => match.Value).ToArray();
                            foreach (var number in numbers)
                            {
                                monkey.Items.Add(int.Parse(number));
                            }
                            break;
                        case 2:
                            var parsed = int.TryParse(line.Split(" ")[5], out var operationNumber);

                            if(line.Split(" ")[4] == "*")
                            {
                                monkey.Multiply = true;
                            }

                            if (parsed)
                            {
                                monkey.OperationNumber = operationNumber;
                                break;
                            }
                            monkey.OperationNumberIsNotNumber = true;
                            break;
                        case 3:
                            var divisor = int.Parse(line.Split(" ")[3]);
                            monkey.Divisor = divisor;
                            break;
                        case 4:
                            var trueMonkey = int.Parse(line.Split(" ")[5]);
                            monkey.TrueTarget = trueMonkey;
                            break;
                        case 5:
                            var falaseMonkey = int.Parse(line.Split(" ")[5]);
                            monkey.FalseTarget = falaseMonkey;
                            break;
                    }
                }

                monkeys.Add(monkey);
            }

            for(int i = 0; i < 20; i++)
            {
                MonkeyThrows(monkeys);
            }

            var sortedMonkey = monkeys.OrderByDescending(x => x.NumberOfTouches).ToList();

            return sortedMonkey[0].NumberOfTouches * sortedMonkey[1].NumberOfTouches;
        }

        private static void MonkeyThrows(List<Monkey> monkeys)
        {
            for (var i = 0; i < monkeys.Count; i++)
            {
                monkeys[i].Operations();
                foreach (var item in monkeys[i].Items)
                {
                    var (target, newItem) = monkeys[i].Test(item);
                    monkeys[target].Items.Add(newItem);
                }
                monkeys[i].Items = new List<int>();
            }
        }
    }

    public class Monkey
    {
        public Monkey()
        {
            Items = new List<int>();
        }
        public bool Multiply { get; set; } = false;

        public List<int> Items { get; set; }
        public int Divisor { get; set; }
        public int TrueTarget { get; set; }
        public int FalseTarget { get; set; }
        public int OperationNumber { get; set; }
        public bool OperationNumberIsNotNumber { get; set; } = false;
        public int NumberOfTouches { get; set; }

        public void Operations()
        {
            List<int> itemsAfterOperations = new();
            foreach(var item in Items)
            {
                if (OperationNumberIsNotNumber)
                {
                    OperationNumber = item;
                }

                if (Multiply)
                {
                    itemsAfterOperations.Add(item * OperationNumber);
                    continue;
                } 

                itemsAfterOperations.Add(item + OperationNumber);
            }
            Items = itemsAfterOperations;
            NumberOfTouches += itemsAfterOperations.Count;
        }

        public (int,int) Test(int number)
        {
            var worryLevel = number / 3;
            var modulo = worryLevel % Divisor;

            if(modulo is 0)
            {
                return (TrueTarget, worryLevel);
            }
            return (FalseTarget, worryLevel);
        }
    }
}
