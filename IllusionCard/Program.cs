using System;
using System.Collections.Generic;
using System.IO;

namespace IllusionCard
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "ph_2020_0125_1542_39_661.png";
            var studio = new StudioPH.SceneInfo();
            if (studio.Load(filePath))
            {
                System.Diagnostics.Debug.WriteLine("Success");
            }

        }
    }
}
