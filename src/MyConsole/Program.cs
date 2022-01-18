using System;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using FFMpegCore;

namespace MyConsole
{
    class Program
    {
        static string GetRootDirectory() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        static async Task Main(string[] args)
        {
            var basePath = GetRootDirectory();
            var outputFile = Path.Combine(basePath, "thumbnail.png");

            Console.WriteLine("Starting get thumbnail...");
            var success = await FFMpegArguments
                .FromUrlInput(new Uri("https://d2qcrcf01ey77p.cloudfront.net/Service/Development/Posts/2073/Videos/Video_20210422083648254.mp4"))
                .OutputToFile(outputFile, true, options => options
                    .Seek(TimeSpan.FromMilliseconds(500))
                    .WithFrameOutputCount(1)
                )
                .ProcessAsynchronously();
            Console.WriteLine($"Path: {outputFile}, Status: {success}");
        }
    }
}