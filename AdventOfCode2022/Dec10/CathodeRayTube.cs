using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2022.Dec10
{
    public class CathodeRayTube
    {
        public static int SignalStrength(string path)
        {
            var signalX = 1;
            var cycle = 0;
            var cycleCheck = 20;
            var score = 0;
            foreach(var line in File.ReadLines(path))
            {
                var split = line.Split(" ");

                if (split[0] == "addx")
                {
                    var x = int.Parse(split[1]);
                    AddCycles(ref signalX, ref cycle, ref cycleCheck, ref score, x, 2);
                    continue;
                }
                AddCycles(ref signalX, ref cycle, ref cycleCheck, ref score);
                if (cycle >= 220) return score;
            }

            return score;
        }

        public static void DrawScreen(string path)
        {
            var signalX = 1;
            var cycle = 0;
            var cycleCheck = 39;
            char[,] screen = new char[6,40];
            var screenline = 0;
            foreach (var line in File.ReadLines(path))
            {
                var split = line.Split(" ");
                
                if (split[0] == "addx")
                {
                    var x = int.Parse(split[1]);
                    Draw(ref signalX, ref cycle, ref cycleCheck, ref screen, ref screenline, x, 2);
                    continue;
                }
                Draw(ref signalX, ref cycle, ref cycleCheck, ref screen, ref screenline);

                if(screenline == 6)
                {
                    for(int i = 0; i<6; i++)
                    {
                        for(int j = 0; j < 39; j++)
                        {
                            Console.Write(screen[i,j]);
                            if(j == 38)
                            {
                                Console.WriteLine();
                            }
                        }
                    }
                }
            }
        }
        
        private static void Draw(ref int signalX, ref int cycle, ref int cycleCheck, ref char[,] screen, ref int screenline, int x = 0, int cycles = 1)
        {
            for (int i = 0; i < cycles; i++)
            {
                if (cycle + 1 <= signalX + 2 && cycle + 1 >= signalX)
                {
                    screen[screenline, cycle] = '#';
                } else
                {
                    screen[screenline, cycle] = '.';
                }
                //Console.WriteLine(screen[screenline, cycle]);
                cycle++;
                
                if(cycle == 40)
                {
                    cycle = 0;
                    screenline++;
                }
                if (i == 1)
                {
                    signalX += x;
                }
            }
        }

        private static void AddCycles(ref int signalX, ref int cycle, ref int cycleCheck, ref int score,  int x = 0, int cycles = 1)
        {
            for(int i = 0; i < cycles; i++)
            {
                cycle++;
                if(cycle == cycleCheck)
                {
                    score += (cycleCheck * signalX);
                    cycleCheck += 40;
                }
                if (i == 1)
                {
                    signalX += x;
                }
            }
        }
    }
}
