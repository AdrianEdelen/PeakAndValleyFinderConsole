using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoneticsChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = "C:/Users/adria/source/repos/AutoneticsChallenge/Files/Code Test Eight Round.csv"; //insert the file path here
            // call the parse points method
            Point[] filePoints = ParsePoints(path).ToArray();
            //Instantiate a PeakAndValleyFinder object, gibing it our array of point objects
            PeakAndValleyFinder peakAndValleyFinder = new PeakAndValleyFinder(filePoints);
            
            //call the DeterminePeaks and DetermineValleys methods (see definition in PeakAndValleyFinder.cs)
            IEnumerable<Point> peaks = peakAndValleyFinder.DeterminePeaks();
            IEnumerable<Point> valleys = peakAndValleyFinder.DetermineValleys();

            // concat and order the two Ienumerables for console output.
            var peaksAndValleys = peaks.Concat(valleys).OrderBy(s => s.X);
            //iterate over and then console out the the data
            foreach (var dataPoint in peaksAndValleys)
            {
                Console.WriteLine("for Position: " + dataPoint.X + " the value is " + dataPoint.Y);
            }
        }
        

        //Parse the dataset
        private static IEnumerable<Point> ParsePoints(string filePath)
        {
            //instantiate a StreamReader to read the lines of the CSV
            using (StreamReader reader = new StreamReader(filePath))
            {
                //keep going until we reach the end
                while (!reader.EndOfStream)
                {
                    
                    //in each loop, we read the next line, and instantiate a new point object
                    var dataLine = reader.ReadLine();
                    var isNumber = false;

                    //here we determine when the actual numbers start in the csv by reading lines and trying to parse them as doubles
                    //if the parse succeeds we can continue on.
                    while (!isNumber)
                    {
                        try
                        {
                            double.Parse(dataLine.Split(',')[0]);
                            isNumber = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            dataLine = reader.ReadLine();
                        }
                    }
                    Point point = new Point()
                    {
                        //split the two colums parse the strings into doubles and set the props of the point object
                        X = double.Parse(dataLine.Split(',')[0]),
                        Y = double.Parse(dataLine.Split(',')[1])
                    };
                    //return the point object
                    yield return point;
                }
            }
        }
    }
}
