using System.Collections.Generic;

namespace TidyPDF
{
    public static class Constants
    {
        public static string WorkingDirectoryPath { get; } = @"C:\Users\MFORGET\Documents\Miguel\Test";
        public static string TargetDirectoryPath { get; } = @"C:\Users\MFORGET\Documents\Miguel\Test";

        public static Dictionary<string, string> ReplaceWords { get; } = new Dictionary<string, string>()
        {
            {"jésus", "Jésus"},
            {"jesus", "Jesus"},
            {"dieu", "Dieu"},
            {"god", "God"},
        };
    }
}