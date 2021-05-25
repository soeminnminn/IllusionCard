using System;
using System.Collections.Generic;
using System.IO;

namespace IllusionCard
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirPath = @"PlayHome\Cards\scene";
            string filePath = Path.Combine(dirPath, "1ca38206b0cbb3a3ee816f20bf521f435837115f.png");

            var game = new GameSystem();
            var studio = game.StudioPH_Instance();
            if (studio.Load(filePath))
            {
                System.Diagnostics.Debug.WriteLine("Success");
            }

        }
    }
}
