using System.Collections.Generic;

namespace TidyPDF
{
    public static class Constants
    {
        public static string WorkingDirectoryPath { get; } = @"E:\SynologyDrive\Chants chrétiens\#A trier\Chants chrétiens";
        public static string TargetDirectoryPath { get; } = @"E:\SynologyDrive\Chants chrétiens\#A trier\Chants chrétiens";
        public static string TrashDirectoryPath { get; } = @"E:\SynologyDrive\Chants chrétiens\#A trier\Trash";
        public static string DuplicateDirectoryPath { get; } = @"E:\SynologyDrive\Chants chrétiens\#A trier\Duplicate";

        public static Dictionary<string, string> ReplaceWords { get; } = new Dictionary<string, string>()
        {
            {"jesus", "Jésus"},
            {"alleluia", "alléluia"},
            {"noel", "Noël"},
            {"paque", "Pâque"},
            {"evangile", "Évangile"},
            {" pere", " Père"},
            {"anamnese", "Anamnèse" },
            {"esperance", "espérance" },
            {"emmaus", "emmaüs" },
            {"priere", "prière" },
            {"lumiere", "lumière" },
            {" fete", " fête" },
        };

        public static List<string> ProperCaseWords { get; } = new List<string>()
        {
            "jésus", " dieu", "god", "marie", "seigneur",
            "noël", "pâque", "évangile", "père", "sanctus",
            "anamnèse", "kyrie", "agnus", "gloria","alléluia",
            " st ", "saint ", "emmaüs", "messe", "esprit", "christ",
            "lord","emmanuel", "nazareth",
        };
    }
}