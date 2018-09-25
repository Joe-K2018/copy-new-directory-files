using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace CopyNewDirectory
{
    class Program
    {
        static DirectoryInfo SourceDirectory
        {
            get
            {
                var path = ConfigurationManager.AppSettings["sourceDirectory"];
                if (string.IsNullOrWhiteSpace(path))
                {
                    Console.WriteLine("Missing configuration name: sourceDirectory");
                    throw new ArgumentNullException();
                }

                if (!Directory.Exists(path))
                {
                    Console.WriteLine("Source directory not found");
                    throw new DirectoryNotFoundException();
                }

                return new DirectoryInfo(path);
            }
        }
        static DirectoryInfo DestinationDirectory
        {
            get
            {
                var path = ConfigurationManager.AppSettings["destinationDirectory"];
                if (string.IsNullOrWhiteSpace(path))
                {
                    Console.WriteLine("Missing configuration name: destinationDirectory");
                    throw new ArgumentNullException();
                }
                return new DirectoryInfo(path);
            }
        }
        static string FileExtension
        {
            get
            {
                var extension = ConfigurationManager.AppSettings["fileExtension"];
                if (string.IsNullOrWhiteSpace(extension))
                {
                    Console.WriteLine("Missing configuration name: fileExtension");
                    throw new ArgumentNullException();
                }
                return extension;
            }
        }
        static void Main(string[] args)
        {
            var fm = new FileManager();
            var folders = (SourceDirectory.GetDirectories()).OrderByDescending(x => x.Name);
            DirectoryInfo newestDirectory = folders.First();
            var photos = newestDirectory.GetFiles(FileExtension);

            Console.WriteLine("Starting");
            Console.WriteLine($"Copying from \"{newestDirectory.FullName}\" to \"{DestinationDirectory.FullName}\"");
            if (DestinationDirectory.Exists)
            {
                DestinationDirectory.Delete(true);
            }

            var numberOfPhotos = photos.Count();
            var i = 1;
            foreach (var photo in photos)
            {

                Console.WriteLine($"Copying {photo}: {i} of {numberOfPhotos}"); 
                fm.CopyFile(photo, DestinationDirectory);
                i++;
            }

            Console.WriteLine("Complete. Press any key to close.");
            Console.ReadKey();
        }
    }
}
