using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Dec7
{
    public class MyFileSystem
    {
        public List<MyFolder> folders = new();
        public int Size => SumAllFolders(folders.First());
        public int RealSize {get; set;}

        public int SumAllFolders(MyFolder root)
        {
            int totalScore = 0;
            foreach(var folder in root.folders)
            {
                if (folder.Size <= 100000 && folder.folders.Count > 0)
                {
                    RealSize += folder.Size;
                }
                var temp = SumAllFolders(folder);
                if(temp <= 100000)
                {
                    RealSize += temp;
                }
            }
            if (root.Size <= 100000 && root.folders.Count == 0)
            {
                RealSize += root.Size;
            }
            return totalScore;
        }
    }

    public class MyFolder
    {
        public string Name { get; set; }
        public List<MyFolder> folders { get; set; } = new();
        public List<MyFile> files { get; set; } = new();
        public int Size => files.Sum(x => x.Size) + folders.Sum(x => x.Size);

    }

    public class MyFile
    {
        public string Name { get; set; }
        public int Size { get; set; }
    }

    public static class yolo
    {
        public static IEnumerable<T> SelectRecursive<T>(this List<T> source, Func<T, List<T>> recursiveSelector)
        {
            foreach (var i in source)
            {
                yield return i;

                var directChildren = recursiveSelector(i);
                var allChildren = SelectRecursive(directChildren, recursiveSelector);

                foreach (var c in allChildren)
                {
                    yield return c;
                }
            }
        }
    }
        
}
