using System.Collections.Generic;

namespace TidyPDF
{
    public static class Constants
    {
        public static string WorkingDirectoryPath { get; } = @"E:\SynologyDrive\Chants chrétiens\#A trier\Chants chrétiens";
        public static string TargetDirectoryPath { get; } = @"E:\SynologyDrive\Chants chrétiens\#A trier\Chants chrétiens";

        public static Dictionary<string, string> ReplaceWords { get; } = new Dictionary<string, string>()
        {
            {"jésus", "Jésus"},
            {"jesus", "Jésus"},
            {"dieu", "Dieu"},
            {"god", "God"},
            {"seigneur", "Seigneur"},
            {"marie", "Marie"},
            {"alleluia", "alléluia"},
            {"noël", "Noël"},
            {"noel", "Noël"},
            {"paque", "Pâque"},
            {"pâque", "Pâque"},
            {"évangile", "Évangile"},
            {"evangile", "Évangile"},
            {" père ", " Père "},
            {" pere ", " Père "},
            {" Pere ", " Père "},
        };
    }
}