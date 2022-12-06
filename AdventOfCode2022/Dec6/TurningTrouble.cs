using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace AdventOfCode2022.Dec6
{
    public class TurningTrouble
    {
        public static int StartOfPacketMarker(string path, bool partTwo = false)
        {
            foreach(var line in File.ReadLines(path))
            {
                if (partTwo)
                {
                    return CharacterProcessing(line, 14);
                }
                return CharacterProcessing(line);
            }
            return 0;
        }

        private static int CharacterProcessing(string line, int length = 4)
        {
            for(int i = length - 1; i < line.Length; i++)
            {
                var marker = line.Substring(i - (length - 1), length);
                if (isUnique(marker))
                {
                    return i+1;
                }
            }
            return 0;
        }

        private static bool isUnique(string marker)
        {
            for(var i = 0; i < marker.Length; i++)
            {
                for (var j = i + 1; j < marker.Length; j++)
                {
                    if (marker[i] == marker[j])
                        return false;
                }
            }
            return true;
        }
    }
}
