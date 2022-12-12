using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2022.De11
{
    public class MonkeyInTheMiddlePart2
    {
        public static long MonkeyBussiness(string path)
        {
            List<Monkey2> monkeys;
            long modulus;
            CreateMonkeysAndLCM(path, out monkeys, out modulus);

            for (int i = 0; i < 10000; i++)
            {
                MonkeyThrows2(monkeys, modulus);
            }

            var sortedMonkey = monkeys.OrderByDescending(x => x.NumberOfTouches).ToList();

            return sortedMonkey[0].NumberOfTouches * sortedMonkey[1].NumberOfTouches;
        }

        private static void CreateMonkeysAndLCM(string path, out List<Monkey2> monkeys, out long modulus)
        {
            monkeys = new List<Monkey2>();
            var stringMonkeys = File.ReadAllText(path).Split("\r\n\r\n");
            List<long> modulus1 = new();
            foreach (var stringmonkey in stringMonkeys)
            {
                var monkeyLines = stringmonkey.Split("\n");
                var monkey = new Monkey2();

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
                                monkey.Items.Add((ulong)int.Parse(number));
                            }
                            break;
                        case 2:
                            var parsed = int.TryParse(line.Split(" ")[5], out var operationNumber);

                            if (line.Split(" ")[4] == "*")
                            {
                                monkey.Multiply = true;
                            }

                            if (parsed)
                            {
                                monkey.OperationNumber = (ulong)operationNumber;
                                break;
                            }
                            monkey.OperationNumberIsNotNumber = true;
                            break;
                        case 3:
                            var divisor = int.Parse(line.Split(" ")[3]);
                            monkey.Divisor = (ulong)divisor;
                            modulus1.Add(divisor);
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

            modulus = LCM(modulus1.ToArray());
        }

        private static void MonkeyThrows2(List<Monkey2> monkeys, long modulus)
        {
            for (var i = 0; i < monkeys.Count; i++)
            {
                monkeys[i].Operations();
                foreach (var item in monkeys[i].Items)
                {
                    var (target, newItem) = monkeys[i].Test(item, modulus);
                    monkeys[target].Items.Add(newItem);
                }
                monkeys[i].Items = new List<ulong>();
            }
        }

        public static long LCM(long[] ofNumbers) // Find LCM using GCD (source: https://iq.opengenus.org/lcm-of-array-of-numbers/ )
        {
            long ans = ofNumbers[0];
            for (int i = 1; i < ofNumbers.Length; i++)
            {
                ans = ofNumbers[i] * ans / GCD(ofNumbers[i], ans);
            }
            return ans;
        }

        private static long GCD(long a, long b) { return b == 0 ? a : GCD(b, a % b); }
    }

    public class Monkey2
    {
        public Monkey2()
        {
            Items = new List<ulong>();
        }
        public bool Multiply { get; set; } = false;
        
        public List<ulong> Items { get; set; }
        public ulong Divisor { get; set; }
        public int TrueTarget { get; set; }
        public int FalseTarget { get; set; }
        public ulong OperationNumber { get; set; }
        public bool OperationNumberIsNotNumber { get; set; } = false;
        public long NumberOfTouches { get; set; }

        public void Operations()
        {
            List<ulong> itemsAfterOperations = new();
            foreach (var item in Items)
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

        public (int, ulong) Test(ulong number, long modulus)
        {
            var worryLevel = number % (ulong)modulus;
            var modulo = worryLevel % Divisor;

            if(worryLevel < 0)
            {
            }

            if (modulo is 0)
            {
                return (TrueTarget, worryLevel);
            }
            return (FalseTarget, worryLevel);
        }
    }
}
