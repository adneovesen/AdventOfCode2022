using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Dec7
{
    public class NoSpaceLeftOnDevice
    {
        public static int SumOfDirectories(string path, bool partTwo = false)
        {
            var filesystem = new MyFileSystem();
            var state = new Stack<MyFolder>();
            foreach (var line in File.ReadLines(path))
            {
                LoadData(line,ref filesystem, ref state);
            }

            if (!partTwo)
            {
                var throwAway = filesystem.Size;
                return filesystem.RealSize;
            }
            
            var TotalSize = 70000000;
            var NeededSize = 30000000;
            var ToBeDeleted = filesystem.folders[0].Size -(TotalSize - NeededSize);

            var flatList = filesystem.folders.SelectRecursive(f => f.folders).ToList();

            foreach(var folder in flatList.OrderBy(x => x.Size))
            {
                if(folder.Size > ToBeDeleted)
                {
                    return (folder.Size);
                }
            }

            return 0;
        }

        private static void LoadData(string line, ref MyFileSystem filesystem, ref Stack<MyFolder> state)
        {
            if (line == "$ cd ..")
            {
                state.Pop();
                return;
            }
            if (line.StartsWith("$ cd /"))
            {
                var folder = new MyFolder
                {
                    Name = line.Split(" ")[2]
                };
                filesystem.folders.Add(folder);
                state.Push(folder);

                return;
            }
            if (line.StartsWith("$ cd"))
            {
                var folder = new MyFolder
                {
                    Name = line.Split(" ")[2]
                };
                var currentFolder = state.Peek();
                if (!currentFolder.folders.Any(x => x.Name == folder.Name))
                {
                    currentFolder.folders.Add(folder);
                    state.Push(folder);
                }
                else
                {
                    var statefolder = currentFolder.folders.First(x => x.Name == folder.Name);
                    state.Push(statefolder);
                }
                return;
            }
            if (line.StartsWith("dir"))
            {
                var folder = new MyFolder
                {
                    Name = line.Split(" ")[1]
                };
                var currentFolder = state.Peek();
                if (!currentFolder.folders.Any(x => x.Name == folder.Name))
                {
                    currentFolder.folders.Add(folder);
                }
                return;
            }
            
            if (isFile(line))
            {
                var file = new MyFile
                {
                    Size = int.Parse(line.Split(" ")[0]),
                    Name = line.Split(" ")[1]
                };
                var folder = state.Peek();
                if(!folder.files.Any(x => x.Name == file.Name))
                {
                    folder.files.Add(file);
                }
                return;
            }
        }

        public static bool isFile(string line)
        {
            var number = line.Split(" ")[0];
            return int.TryParse(number, out _);
        }
    }
}
