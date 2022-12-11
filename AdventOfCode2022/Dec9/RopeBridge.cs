using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Dec9
{
    public class RopeBridge
    {
        public static int TailCalculation(string path)
        {
            var headRow = 0;
            var headCollumn = 0;
            var tailRow = 0;
            var tailCollumn = 0;

            var tailPositions = new HashSet<(int, int)>();

            foreach (var line in File.ReadLines(path))
            {
                var split = line.Split(" ");

                var direction = split[0];
                var distance = int.Parse(split[1]);

                switch (direction)
                {
                    case "R":
                        headRow += distance;
                        break;
                    case "L":
                        headRow -= distance;
                        break;
                    case "U":
                        headCollumn += distance;
                        break;
                    case "D":
                        headCollumn -= distance;
                        break;
                }

                while (headRow > tailRow || headRow == tailRow && headCollumn > tailCollumn)
                {
                    if (headCollumn > tailCollumn)
                    {
                        tailRow++;
                        tailCollumn++;
                    }
                    else if (headCollumn < tailCollumn)
                    {
                        tailRow++;
                        tailCollumn--;
                    }
                    else
                    {
                        tailRow++;
                    }

                    tailPositions.Add((tailRow, tailCollumn));
                }

                while (headRow < tailRow || headRow == tailRow && headCollumn < tailCollumn)
                {
                    if (headCollumn > tailCollumn)
                    {
                        tailRow--;
                        tailCollumn++;
                    }
                    else if (headCollumn < tailCollumn)
                    {
                        tailRow--;
                        tailCollumn--;
                    }
                    else
                    {
                        tailRow--;
                    }

                    tailPositions.Add((tailRow, tailCollumn));
                }

                while (headCollumn > tailCollumn || headRow == tailRow && headCollumn > tailCollumn)
                {
                    if (headRow > tailRow)
                    {
                        tailRow++;
                        tailCollumn++;
                    }
                    else if (headRow < tailRow)
                    {
                        tailRow--;
                        tailCollumn++;
                    }
                    else
                    {
                        tailCollumn++;
                    }
                    tailPositions.Add((tailRow, tailCollumn));
                }

                while (headCollumn < tailCollumn || headRow == tailRow && headCollumn < tailCollumn)
                {
                    if (headRow > tailRow)
                    {
                        tailRow++;
                        tailCollumn--;
                    }
                    else if (headRow < tailRow)
                    {
                        tailRow--;
                        tailCollumn--;
                    }
                    else
                    {
                        tailCollumn--;
                    }

                    tailPositions.Add((tailRow, tailCollumn));
                }
            }

            // Return the number of positions occupied by the tail.
            return tailPositions.Count();
        }
        }
}
