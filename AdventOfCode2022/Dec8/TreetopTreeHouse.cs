using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Dec8
{
    public class TreetopTreeHouse
    {
        public static int NumberOfVisibleTrees(string path)
        {
            var lines = File.ReadAllLines(path);

            var numberOfVisibleTrees = (lines[0].Length*2) + ((lines.Length-2)*2);
            
            for(var i = 1; i < lines[0].Length - 1; i++)
            {
                for (var j = 1; j < lines.Length -1; j++)
                {
                    var leftMover = j - 1;
                    var rightMover = j + 1;
                    var topMover = i - 1;
                    var downMover = i + 1;

                    var currentTree = int.Parse(lines[i][j].ToString());
                    var visibleTree = false;

                    while (currentTree > int.Parse(lines[i][leftMover].ToString()))
                    {
                        if (leftMover == 0)
                        {
                            numberOfVisibleTrees++;
                            visibleTree = true;
                            break;
                        }
                        leftMover--;
                    }
                    if (visibleTree) continue;
                    while (currentTree > int.Parse(lines[i][rightMover].ToString()))
                    {
                        if (rightMover == lines[0].Length - 1)
                        {
                            numberOfVisibleTrees++;
                            visibleTree = true;
                            break;
                        }
                        rightMover++;
                    }
                    if (visibleTree) continue;
                    while (currentTree > int.Parse(lines[topMover][j].ToString()))
                    {
                        if (topMover == 0)
                        {
                            numberOfVisibleTrees++;
                            visibleTree = true;
                            break;
                        }
                        topMover--;
                    }
                    if (visibleTree) continue;
                    while (currentTree > int.Parse(lines[downMover][j].ToString()))
                    {
                        if (downMover == lines.Length - 1)
                        {
                            numberOfVisibleTrees++;
                            break;
                        }
                        downMover++;
                    }
                    
                }
            }

            return numberOfVisibleTrees;
        }

        public static int GetScenicScore(string path)
        {
            var lines = File.ReadAllLines(path);
            var bestScenicScore = 0;

            for (var i = 1; i < lines[0].Length - 1; i++)
            {
                for (var j = 1; j < lines.Length - 1; j++)
                {
                    var leftMover = j - 1;
                    var rightMover = j + 1;
                    var topMover = i - 1;
                    var downMover = i + 1;

                    var currentTree = int.Parse(lines[i][j].ToString());
                    var visibleTree = false;

                    while (currentTree > int.Parse(lines[i][leftMover].ToString()))
                    {
                        if (leftMover == 0)
                        {
                            break;
                        }
                        leftMover--;
                    }
                    if (visibleTree) continue;
                    while (currentTree > int.Parse(lines[i][rightMover].ToString()))
                    {
                        if (rightMover == lines[0].Length - 1)
                        {
                            break;
                        }
                        rightMover++;
                    }
                    if (visibleTree) continue;
                    while (currentTree > int.Parse(lines[topMover][j].ToString()))
                    {
                        if (topMover == 0)
                        {
                            break;
                        }
                        topMover--;
                    }
                    if (visibleTree) continue;
                    while (currentTree > int.Parse(lines[downMover][j].ToString()))
                    {
                        if (downMover == lines.Length - 1)
                        {
                            break;
                        }
                        downMover++;
                    }

                    var currentScenicSore = (j - leftMover) * (rightMover - j) * (i - topMover) * (downMover - i);

                    if(currentScenicSore > bestScenicScore)
                    {
                        bestScenicScore = currentScenicSore;
                    }
                }
            }

            return bestScenicScore;
        }
    }
}
