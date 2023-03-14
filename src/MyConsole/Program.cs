using System;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using FFMpegCore;
using FFMpegCore.Enums;

namespace MyConsole
{
    class Program
    {
        static string GetRootDirectory() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        static async Task ConvertVideoToWebMFile()
        {
            var basePath = GetRootDirectory();
            var inputFilePath = Path.Combine(basePath, "files", "sample_1920x1080.wmv");
            var outputFilePath = Path.Combine(basePath, "files", "output.webm");

            var success = await FFMpegArguments
                .FromFileInput(inputFilePath)
                .OutputToFile(outputFilePath, true, a => a
                    .Seek(TimeSpan.FromSeconds(10))
                    .WithDuration(TimeSpan.FromSeconds(30))
                    .WithFramerate(30)
                    .Resize(500, 500)
                    .WithVideoCodec(VideoCodec.LibVpx)
                    .WithConstantRateFactor(30)
                    .WithVideoBitrate(4500)
                    .DisableChannel(Channel.Audio)
                ).ProcessAsynchronously();

            Console.WriteLine($"Convert Success?: {success}");
        }

        static async Task ResizeImage()
        {
            var basePath = GetRootDirectory();
            var inputFilePath = Path.Combine(basePath, "files", "background.jpg");
            var outputFilePath = Path.Combine(basePath, "files", "resize_background.jpg");

            var success = await FFMpegArguments
                .FromFileInput(inputFilePath)
                .OutputToFile(outputFilePath, true, a => a
                    .Resize(500, 500)
                ).ProcessAsynchronously();

            Console.WriteLine($"Resize Success?: {success}");
        }

        static async Task GetThunbnailFileAsync()
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

        static async Task Main(string[] args)
        {
            // await GetThunbnailFileAsync();

            // await ConvertVideoToWebMFile();
            await ResizeImage();
        }
    }
}