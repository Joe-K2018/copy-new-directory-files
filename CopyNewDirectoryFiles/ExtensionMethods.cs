using System.IO;

namespace COpyNewDirectoryFiles
{
    static public class ExtensionMethods
    {
        public static FileInfo IncrementFileCount(this FileInfo file, DirectoryInfo destination)
        {
            var destinationFile = Path.Combine(destination.FullName, file.Name);
            for (int i = 2; File.Exists(destinationFile); i++)
            {
                var nameWOExtension = Path.GetFileNameWithoutExtension(file.Name);
                destinationFile = $"{destination.FullName}\\{nameWOExtension} ({i}){file.Extension}";
            }

            return new FileInfo(destinationFile);
        }
    }
}
