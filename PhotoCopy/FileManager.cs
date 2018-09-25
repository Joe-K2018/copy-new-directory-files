using System;
using System.IO;

namespace CopyNewDirectory
{
    class FileManager
    {
        public void CopyFile(FileInfo file, DirectoryInfo destination)
        {
            try
            {
                if (!file.Exists)
                {
                    Console.WriteLine($"File '{file}' cannot be found\n");
                    return;
                }

                Directory.CreateDirectory(destination.FullName);
                var destinationFile = Path.Combine(destination.FullName, file.Name);
                if (File.Exists(destinationFile))
                {
                    var newDestFile = file.IncrementFileCount(destination);
                    file.CopyTo(newDestFile.FullName);
                }
                else
                {
                    file.CopyTo(destinationFile);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }
    }
}
