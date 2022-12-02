// See https://aka.ms/new-console-template for more information
using AdventOfCode2022.Dec1;
using AdventOfCode2022.Dec2;

var path = "..\\..\\..\\Dec2\\input.txt";
Console.WriteLine($"Total score if everything goes like the strategy guide : {RockPaperScissors.TotalScoreFromTournament(path)}");

Console.WriteLine($"Total score if everything goes like the actual strategy guide : {RockPaperScissors.TotalScoreFromTournament(path, true)}");