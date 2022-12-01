// See https://aka.ms/new-console-template for more information
using AdventOfCode2022.Dec1;

var path = "..\\..\\..\\Dec1\\input1.txt";
Console.WriteLine($"Elf with the most calories has: {CalorieCounting.GetCaloriesFromElf(path)}");

Console.WriteLine($"The top three elves has: {CalorieCounting.GetCaloriesFromTopThreeElves(path)}");
