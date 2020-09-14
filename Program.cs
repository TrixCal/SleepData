using System;
using System.IO;
using System.Linq;

namespace SleepData
{
    class Program
    {
        static void Main(string[] args)
        {
            // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();

            if (resp == "1")
            {
                // create data file

                 // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                int weeks = int.Parse(Console.ReadLine());

                 // determine start and end date
                DateTime today = DateTime.Now;
                // we want full weeks sunday - saturday
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                // subtract # of weeks from endDate to get startDate
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
                
                // random number generator
                Random rnd = new Random();

                // create file
                StreamWriter sw = new StreamWriter("data.txt");
                // loop for the desired # of weeks
                while (dataDate < dataEndDate)
                {
                    // 7 days in a week
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++)
                    {
                        // generate random number of hours slept between 4-12 (inclusive)
                        hours[i] = rnd.Next(4, 13);
                    }
                    // M/d/yyyy,#|#|#|#|#|#|#
                    //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                    // add 1 week to date
                    dataDate = dataDate.AddDays(7);
                }
                sw.Close();
            }
            else if (resp == "2")
            {
                // TODO: parse data file
                if(File.Exists("data.txt"))
                {
                    StreamReader sr = new StreamReader("data.txt");
                    while(!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] arr = line.Split(",");

                        DateTime date = DateTime.Parse(arr[0]);
                        int[] hours = Array.ConvertAll(arr[1].Split("|"), int.Parse);

                        Console.WriteLine($"Week of {date:MMM}, {date:dd}, {date:yyyy}");
                        Console.WriteLine(" Su Mo Tu We Th Fr Sa Tot Avg");
                        Console.WriteLine(" -- -- -- -- -- -- -- --- ---");
                        for(int i = 0; i < 7; i++)
                        {
                            Console.Write($"{hours[i], 3}");
                        }
                        Console.Write($"{hours.Sum(), 4} {Math.Round((double)hours.Sum() / 7, 1), 3}");
                        Console.WriteLine("\n");
                    }
                    sr.Close();
                }
            }
        }
    }
}
