using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Data.Common;
using System.Text;
using System.Linq;
using System;
using System.Diagnostics.Metrics;
using System.Collections;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Xml.Linq;
using static Program.TimeOnlineSurv;
using static TableBuilder;

internal class Program
{
    public enum CategoryID : int 
    {
        General,
        Child,
        Teenager,
        YoungAdult,
        Adult,
        Older
    } //enum approach
    const int IEGeneral = (int)CategoryID.General;
    const int IEChild = (int)CategoryID.Child;
    const int IETeenager = (int)CategoryID.Teenager;
    const int IEYoungAdult = (int)CategoryID.YoungAdult;
    const int IEAdult = (int)CategoryID.Adult;
    const int IEOlder = (int)CategoryID.Older;
    // my const approach    
    public const int IGeneral = 0;
    public const int IChild = 1;
    public const int ITeenager = 2;
    public const int IYoungAdult = 3;
    public const int IAdult = 4;
    public const int IOlder = 5;

    int[] catGeneral = new int[6];
    int[] catChild = new int[6];
    int[] catTeenager = new int[6];
    int[] catYoungAdult = new int[6];
    int[] catAdult = new int[6];
    int[] catOlder = new int[6];
    string[] catName = new string[] { "General", "Child", "Teenager", "YoungAdult", "Adult", "Older" };

    const int CLASSSIZE = 10;
    public static Stopwatch stopwatch = Stopwatch.StartNew(); //Diagnostic measures. Start measuring time
    private static void Main(string[] args)
    {
        stopwatch.Start(); //strarting our measurment for program running
         Day01.DayOne();
        /* Day02.DayTwo();
           Day03.DayThree();
           Day04.DayFour();
           Day05.DayFive();
           Day06.DaySix();
           Day07.DaySeven();
           Day08.DayEight();
           Day09.DayNine();
           Day10.DayTen();
           Day11.DayEleven();
           Day12.DayTwelve();
           Day13.Day13();*/
        Day14.Day14();


        stopwatch.Stop();
        Console.WriteLine($"\nPress any key to exit...\tProcessing time: {stopwatch.ElapsedMilliseconds} ms");
        Console.ReadKey();
        Environment.Exit(0);
    }

    public class DataFromFile
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Category Category { get; set; }
        public double Hours_Mon { get; set; }
        public double Hours_Tue { get; set; }
        public double Hours_Wed { get; set; }
        public double Hours_Thu { get; set; }
        public double Hours_Fri { get; set; }
        public double Hours_Sat { get; set; }
        public double Hours_Sun { get; set; }
    }
    public class TimeOnlineSurv
    {
        public static List<DataFromFile> dataList = new List<DataFromFile>();
        public enum Category
        {
            Child,
            Teenage,
            YoungAdult,
            Older,
            Adult
        }
        private static Category GetCategory(int age)
        {
            switch (age)
            {
                case < 13: return Category.Child;
                case <= 13: return Category.Teenage;
                case <= 20: return Category.YoungAdult;
                case <= 31: return Category.Older;
                default: return Category.Adult;
            }
        }
      static public string Calculate()
        {
            var orderedGroups = dataList.GroupBy(data => data.Category)
                            .OrderBy(group => group.Key); 
            var groupedByCategory = dataList.GroupBy(data => data.Category);
            var outputText = "";
            foreach (var categoryGroup in groupedByCategory)
            {
                outputText += $"Category: {categoryGroup.Key}\n";
                var counter = 0;
                var total = 0.0;
                var topSub = "";
                var topSubHours = 0.0;
                foreach (var data in categoryGroup)
                {
                    double sum = data.Hours_Sat + data.Hours_Tue + data.Hours_Wed + data.Hours_Thu + data.Hours_Fri + data.Hours_Sat + data.Hours_Sun;
                    total += sum;
                    if (topSubHours < sum)
                    {
                        topSubHours = sum;
                        topSub = data.Name;
                    }
                    //Console.WriteLine($"    Hours - Mon: {data.Hours_Mon}, Tue: {data.Hours_Tue}, Wed: {data.Hours_Wed}, Thu: {data.Hours_Thu}, Fri: {data.Hours_Fri}, Sat: {data.Hours_Sat}, Sun: {data.Hours_Sun}");
                    counter++;
                }
                outputText += $"Total: {total}\n";
                outputText += $"Average: {total / counter:F2}\n";
                outputText += $"Top Subject: {topSub} with {topSubHours} hours\n\n";
            }
            return outputText;
        }
        static public List<DataFromFile> ParseCSV (string path)
        {

            string[] dataLines = new string[] { };
            try
            {
                dataLines = File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => !string.IsNullOrWhiteSpace(line)) // Skip empty lines
                    .ToArray();
                if (dataLines.Length < 2)
                {
                    throw new Exception("Data Lines is not enough to process");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            foreach (string line in dataLines)
            {
                string[] splitted = line.Split(',');
                int.TryParse(splitted[1], out int ageInt);
                double.TryParse(splitted[2], out double hoursMon);
                double.TryParse(splitted[3], out double hoursTue);
                double.TryParse(splitted[4], out double hoursWed);
                double.TryParse(splitted[5], out double hoursThu);
                double.TryParse(splitted[6], out double hoursFri);
                double.TryParse(splitted[7], out double hoursSat);
                double.TryParse(splitted[8], out double hoursSun);

                dataList.Add(new DataFromFile
                {
                    Name = splitted[0],
                    Age = ageInt,
                    Category = GetCategory(ageInt),
                    Hours_Mon = hoursMon,
                    Hours_Tue = hoursTue,
                    Hours_Wed = hoursWed,
                    Hours_Thu = hoursThu,
                    Hours_Fri = hoursFri,
                    Hours_Sat = hoursSat,
                    Hours_Sun = hoursSun
                }) ;
            }
            return dataList;
        }
    }
} // class Program

class PharmacySales
{
    public string Pharmacy { get; set; }
    public string County { get; set; }
    public double SalesQ1 { get; set; }
    public double SalesQ2 { get; set; }
    public double SalesQ3 { get; set; }
    public double SalesQ4 { get; set; }
    public double TotalSales => SalesQ1 + SalesQ2 + SalesQ3 + SalesQ4;
}
class PharmacySales2
{
    public string Pharmacy;
    public string County;
    public double SalesQ1;
    public double SalesQ2;
    public double SalesQ3;
    public double SalesQ4;

    public double TotalSales()
    {
        return SalesQ1 + SalesQ2 + SalesQ3 + SalesQ4;
    }
}