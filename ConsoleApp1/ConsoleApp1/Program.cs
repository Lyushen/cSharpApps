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
    static Stopwatch stopwatch = Stopwatch.StartNew(); //Diagnostic measures. Start measuring time
    private static void Main(string[] args)
    {
        stopwatch.Start(); //strarting our measurment for program running
        /* Program.DayOne();
           Program.DayTwo();
           Program.DayThree();
           Program.DayFour();
           Program.DayFive();
           Program.DaySix();
           Program.DaySeven();
           Program.DayEight();
           Program.DayNine();
           Program.DayTen();
           Program.DayEleven();
           Program.DayTwelve();
           Program.Day13();*/
           Program.Day14();


        stopwatch.Stop();
        Console.WriteLine($"\nPress any key to exit...\tProcessing time: {stopwatch.ElapsedMilliseconds} ms");
        Console.ReadKey();
        Environment.Exit(0);
    }

    static void Day14()
    {

                /*TASK: Calculate and display

                The pharmacy with the highest sales in each county
                The average per county
                The total sales per county*/
                OnlyArrays();
                static void OnlyArrays() {
                        string directoryPath = @"..\..\..\InputAndOutputFiles\";
                        string fileName = "pharmacy";
                        string filePath = directoryPath + fileName + ".CSV";

                        string[] lines = new string[] { };
                        try {
                                lines = File.ReadAllLines(filePath).Skip(1).Where(line => ! string.IsNullOrWhiteSpace(line)).ToArray();
                        }catch(Exception ex) { Console.WriteLine(ex.Message); }
                        if(lines.Length < 2) {
                                Console.WriteLine( "The file doesn't contain enough data to proceed" );
                                System.Environment.Exit(1);
                        }

                        string[] pharmacies = new string[lines.Length];
                        string[] counties = new string[lines.Length];
                        double[] totalSales = new double[lines.Length];

                        // Parse the CSV data
                        for (int i = 0; i < lines.Length; i++)
                        {
                            string[] columns = lines[i].Split(',');
                            pharmacies[i] = columns[0];
                            counties[i] = columns[1];
                            totalSales[i] = double.Parse(columns[2]) + double.Parse(columns[3]) + double.Parse(columns[4]) + double.Parse(columns[5]);
                        }

                        // Calculate and display the required statistics
                        HashSet<string> uniqueCounties = new HashSet<string>(counties); // HashSet returns only unique values from the array.
                        string output = "";
                        foreach (string county in uniqueCounties)
                        {
                            output+=Calculate(county, pharmacies, counties, totalSales);
                        }
                        Console.WriteLine (output);
                        //FiieWrite(output);
                    }


    static string Calculate(string county, string[] pharmacies, string[] counties, double[] totalSales)
    {
        double highestSales = 0;
        string highestSalesPharmacy = "";
        double totalCountySales = 0;
        int countySalesCount = 0;

        for (int i = 0; i < counties.Length; i++)
        {
            if (counties[i] == county)
            {
                if (totalSales[i] > highestSales)
                {
                    highestSales = totalSales[i];
                    highestSalesPharmacy = pharmacies[i];
                }
                totalCountySales += totalSales[i];
                countySalesCount++;
            }
        }

        double averageSales = totalCountySales / countySalesCount;
        string output = "";
        output+=$"County: {county}\n";
        output+=$"Highest Sales Pharmacy: {highestSalesPharmacy}\n";
        output+=$"Total Sales: {totalCountySales:F0}\n";
        output+=$"Average Sales: {averageSales:F2}\n";
        return output;
    } //DisplayStatisticsForCounty()

                //ShortestNotComplex();
                static void ShortestNotComplex() {
                        string directoryPath = @"..\..\..\InputAndOutputFiles\";
                        string fileName = "pharmacy";
                        string filePath = directoryPath + fileName + ".CSV";

                        string[] lines = new string[] { };
                        try {
                              lines = File.ReadAllLines(filePath).Skip(1).Where(line => ! string.IsNullOrWhiteSpace(line)).ToArray();
                        }catch(Exception ex) { Console.WriteLine(ex.Message); }
                        if(lines.Length < 2) {
                                Console.WriteLine( "The file doesn't contain enough data to proceed" );
                                System.Environment.Exit(1);
                        }

                        List<PharmacySales2> salesData = new List<PharmacySales2>();

                        for (int i = 1; i < lines.Length; i++) // Skip header line
                        {
                            string[] columns = lines[i].Split(',');
                            PharmacySales2 sale = new PharmacySales2
                            {
                                Pharmacy = columns[0],
                                County = columns[1],
                                SalesQ1 = double.Parse(columns[2]),
                                SalesQ2 = double.Parse(columns[3]),
                                SalesQ3 = double.Parse(columns[4]),
                                SalesQ4 = double.Parse(columns[5])
                            };
                            salesData.Add(sale);
                        }

                        Dictionary<string, (string Pharmacy, double TotalSales)> highestSalesPerCounty = new Dictionary<string, (string, double)>();
                        Dictionary<string, double> totalSalesPerCounty = new Dictionary<string, double>();
                        Dictionary<string, int> countySalesCount = new Dictionary<string, int>();

                        foreach (PharmacySales2 sale in salesData)
                        {
                            // Total and Average Sales Per County
                            if (!totalSalesPerCounty.ContainsKey(sale.County))
                            {
                                totalSalesPerCounty[sale.County] = 0;
                                countySalesCount[sale.County] = 0;
                            }
                            totalSalesPerCounty[sale.County] += sale.TotalSales();
                            countySalesCount[sale.County]++;

                            // Highest Sales Per County
                            if (!highestSalesPerCounty.ContainsKey(sale.County) || highestSalesPerCounty[sale.County].TotalSales < sale.TotalSales())
                            {
                                highestSalesPerCounty[sale.County] = (sale.Pharmacy, sale.TotalSales());
                            }
                        }

                        foreach (string county in totalSalesPerCounty.Keys)
                        {
                            Console.WriteLine($"County: {county}");
                            Console.WriteLine($"Highest Sales Pharmacy: {highestSalesPerCounty[county].Pharmacy}");
                            Console.WriteLine($"Total Sales: {totalSalesPerCounty[county]}");
                            Console.WriteLine($"Average Sales: {totalSalesPerCounty[county] / countySalesCount[county]}");
                            Console.WriteLine();
                        }
                }
                //ShortestComplexWay();
                static void ShortestComplexWay( ) {
                        string directoryPath = @"..\..\..\InputAndOutputFiles\";
                        string fileName = "pharmacy";
                        string filePath = directoryPath + fileName + ".CSV";
                        string[] lines = File.ReadAllLines(filePath).Skip(1).Where(line => ! string.IsNullOrWhiteSpace(line)).ToArray();

                        List<PharmacySales> salesData = lines.Select(line =>
                        {
                            string[] columns = line.Split(',');
                            return new PharmacySales
                            {
                                Pharmacy = columns[0],
                                County = columns[1],
                                SalesQ1 = double.Parse(columns[2]),
                                SalesQ2 = double.Parse(columns[3]),
                                SalesQ3 = double.Parse(columns[4]),
                                SalesQ4 = double.Parse(columns[5]),
                            };
                        }).ToList();

                        IEnumerable<IGrouping<string, PharmacySales>> groupedData = salesData.GroupBy(s => s.County);

                         foreach (IGrouping<string, PharmacySales> group in groupedData)
                        {
                                string topPharmacy = group.OrderByDescending(s => s.TotalSales).First().Pharmacy;
                                double totalSales = group.Sum(s => s.TotalSales);
                                double averageSales = group.Average(s => s.TotalSales);

                                Console.WriteLine($"County: {group.Key}");
                                Console.WriteLine($"Highest Sales Pharmacy: {topPharmacy}");
                                Console.WriteLine($"Total Sales: {totalSales}");
                                Console.WriteLine($"Average Sales: {averageSales}");
                                Console.WriteLine();
                        }
                }
                
                static void MyWay( ) {
                
                
                        string fileName = "pharmacy";
                        string inputFile = fileName + ".csv";
                        string outputFile = fileName + ".txt";

                        string[] dataLines = ReadMyFile(inputFile);
                        //foreach (string line in dataLines) Print(line);
                        int linesCount = dataLines.Length;

                        string[] pharmacyStores = new string[linesCount];
                        string[] counties = new string[linesCount];
                        double[] q1 = new double[linesCount];
                        double[] q2 = new double[linesCount];
                        double[] q3 = new double[linesCount];
                        double[] q4 = new double[linesCount];

                        double[] topSales = new double[linesCount];
                        string[] topSeller = new string[linesCount];
                        double[] totalSales = new double[linesCount];

                        // Slipt and process the data
                        for (int i = 0; i < dataLines.Length; i++) 
                        {
                            string[] splittedData = dataLines[i].Split(",");
                            pharmacyStores[i] = splittedData[0];
                            counties[i] = splittedData[1];
                            double.TryParse(splittedData[2], out q1[i]);
                            double.TryParse(splittedData[3], out q2[i]);
                            double.TryParse(splittedData[4], out q3[i]);
                            double.TryParse(splittedData[5], out q4[i]);
                        }


                        //Console.WriteLine(ProcessData());
                        //foreach (string s in uniqueCounties) { Print(s); }
                        string outputString = ProcessData(pharmacyStores, counties, q1, q2, q3, q4);

                        // Process data and build a string to output
                        static string ProcessData(string[] pharmacyStores, string[] counties, double[] q1, double[] q2, double[] q3, double[] q4)
                        {
                            string outputString = "";
                            HashSet<string> uniqueCounties = new HashSet<string>(counties);

                            foreach (string county in counties)
                            {
                
                            }
                            return outputString;
                        }


                        static string[] ReadMyFile(string path)
                        {
                            string[] dataLines = new string[] { };
                            try
                            {
                                dataLines = File.ReadAllLines(path).Skip(1).Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
                                if (dataLines.Length < 2) { throw new Exception($"Error: The file '{path}' doesn't exist or contain enough data."); }
                                return dataLines;
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message); }
                            return dataLines;
                        }

                }// myWay( )

     }
    static void Day13()
    {
        /*
        Categorise subjects by age as follows

        Child < 13
        Teenage 13-19 inclusive
        Young Adult: 20-30 inclusive
        Adult 31-60 inclusive
        Older person 61 and older
        For each category, calculate and report (on screen and in file) the following:

        Total time spent online 
        Average time a subject in this category spends online
        Identify the subject that spends the most time online
        Provide Design (Flow Chart and Pseudocode)
        Code solution
        */
        string localPath = @"..\..\..\..\..\inputFiles\"; // we get the parent solution directory 
        string csvFileName = "TimeOnlineByAge";
        string csvFilePath = Path.Combine(localPath, csvFileName + ".csv");

        List<DataFromFile> dataList = TimeOnlineSurv.ParseCSV(csvFilePath);
        string outputString = TimeOnlineSurv.Calculate();
        Print(outputString);

        //Write(outputText); ?
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
    static void DayTwelve()
    {
        /*
        Allow users to input their salary (weekly/monthly/yearly) before or after tax
            o Before tax: allow users to input their tax credits and handle the tax deductions
              then output take home pay based on current Irish tax brackets
            o After tax: allow users to optionally specify their tax for calculation into final report
        • Allow users to input any expenses they may have by specifying a category tag and amount
        • Display a final ‘report’ detailing the users starting salary, all expenses broken down in
          categories and a final take home pay showing how much they earn weekly, monthly and
          yearly
        Nice to have extras/Advanced features:
        • The ability to specify custom tax brackets to handle potential non-Irish tax residents to use
          the application
        • The ability to save the final ‘report’ into a text file
        */

        /*      #Design#
        #1.      INPUT (with number checks)
        #2.      Ask if it before tax or after tax
        #2.1     BEFORE taxes: (case)
        #2.1.1.  Input Tax credit
        #2.1.2.  Current Irish Tax rates? 20% before 40000, 40% after
                 (output them for clearity and ask additional questions like married)
        2.2.    AFTER taxes: (case)
        2.2.1.  Optional paid taxes?
        >3.      EXPENSES
        3.1.    Ask tag of category (1-2-3) and sum
        4.      REPORT (Should include:)
        4.1.    First salary (before and after taxes)
        4.2.    Expenses sum by category and in general
        4.3.    FINAL SALARY
        4.3.1.  Montly, Weekly, Yearly

        5.      (Optional)
        5.1.    Non-resident of Ireland (custom tax rates)
        5.2.    Write Report to the file
        */

        string welcomeMessage = "Welcome to your personal salary calculator";
        Print(welcomeMessage);
        Print(new string('=', welcomeMessage.Length));
        // Weekly(1)/Montly(2)/Annual(3)
        double salaryPeriod = ReAskRange("Would you like to enter your salary Weekly(1)/Montly(2)/Annual(3)?", 1, 3);
        double salary = ReAskRange("Please enter your salary");
        bool isBeforeTaxes = ReAskYesNo($"Is your salary {salary} before taxes?");
        double taxCredit = ReAskRange("Please enter your Tax Credit");
        double currentTaxRate = 40000.00; // less than 40k, it will be 20% deduction, if more 40%.
        double[] percantageTaxRates = new double[] { 20.00, 40.00 };
        Print($"Current Tax Rate is {currentTaxRate}. Before {currentTaxRate}, deduction will be {percantageTaxRates[0]}%, all above {percantageTaxRates[1]}%");

        //if after tages, paid taxes?

        bool isExpenses = ReAskYesNo("Would you like to add expenses?");
        Dictionary<string, double> expenses = new Dictionary<string, double>();
        if (isExpenses)
            expenses = ExpensesCat();



        static Dictionary<string,double> ExpensesCat()
        {
            Dictionary<string,double> expenses = new Dictionary<string,double>();
            Print("Please enter category");
            if (expenses.Count > 0)
            {
                object result = IdentifyAnswer("Please enter category or chose existing:");
                if (result is int intValue)
                {
                    Console.WriteLine($"The input is an integer: {intValue}");
                }
                else if (result is double doubleValue)
                {
                    Console.WriteLine($"The input is a double: {doubleValue}");
                }
                else
                {
                    Console.WriteLine($"The input is a string: {result}");
                    expenses.Add(result.ToString() ?? "", 0);
                }
            }
            // previous category ask?
            return expenses;
        }

        static bool ReAskYesNo(string message)
        {
            Console.Write($"{message} (Y/N) > ");
            string[] possiblePositiveAnswers = new string[] { "yes", "ye", "y" };
            string[] possibleNegativeAnswers = new string[] { "no", "n" };
            do
            {
                stopwatch.Stop();
                string asnwer = Console.ReadLine() ?? "".ToLower();
                stopwatch.Start();
                if (possiblePositiveAnswers.Contains(asnwer))
                {
                    return true;
                }
                else if (possibleNegativeAnswers.Contains(asnwer))
                {
                    return false;
                }
                else
                {
                    Console.Write($"Error. You answer is not clear, please asnwer (Y)es or (N)o. {message} > ");
                }
            } while (true);
        }
        static int ReAskIntRange(string message, int min=1, int max=10_000_000)
        {
            while (true)
            {
                Console.Write($"{message} [{min}-{max}] > ");
                stopwatch.Stop();
                string input = Console.ReadLine() ?? "";
                stopwatch.Start();
                int number = SimplifyInput<int>(input);
                if (number >= min && number <= max)
                {
                    return number;
                }
                Console.WriteLine($"Error: Please enter a number between {min} and {max}.");
            }
        } // ReAskIntRange()
        static double ReAskRange(string message, double min = 0.01, double max = 10_000_000.00)
        {
            while (true)
            {
                Console.Write($"{message} [{min}-{max}] > ");
                stopwatch.Stop();
                string input = Console.ReadLine() ?? "";
                stopwatch.Start();
                input = input.Replace(" ", "");
                double number = 0;
                input = Regex.Replace(input, @"(\d+(\.\d+)?)(k|K)", m =>
                {
                    if (double.TryParse(m.Groups[1].Value, out double localNumber))
                    {
                        localNumber *= 1000; // Multiply by 1000 for 'k'
                        number = localNumber; // Assign the calculated value to the outer variable
                    }
                    return m.Value;
                });
                if (number >= min && number <= max)
                {
                    return Math.Round(number, 2);
                }
                Console.WriteLine($"Error: Please enter a number between {min} and {max}.");
            }
        } // ReAskDobuleRange()

        static T SimplifyInput<T>(string input) where T : struct
        {
            input = input.Replace(" ", "");
            string pattern = @"(\d+(\.\d+)?)(k|K)";
            input = Regex.Replace(input, pattern, m =>
            {
                if (double.TryParse(m.Groups[1].Value, out double number))
                {
                    number *= 1000; // Multiply by 1000 for 'k'
                    return typeof(T) == typeof(int) ? ((int)number).ToString() : number.ToString();
                }
                return m.Value;
            });

            // Depending on T, parse the input as int or double
            if (typeof(T) == typeof(int) && int.TryParse(input, out int intValue))
            {
                return (T)(object)intValue;
            }
            else if (typeof(T) == typeof(double) && double.TryParse(input, out double doubleValue))
            {
                return (T)(object)doubleValue;
            }
            else 
                return (T)(object)input;
        }
        static void Print(string text, bool isNewLine=true)
        {
            if (isNewLine)
                Console.WriteLine(text);
            else
                Console.Write(text);
        }
        static object IdentifyAnswer(string message)
        {
            Console.WriteLine(message);
            string userInput = Console.ReadLine() ?? "";

            if (int.TryParse(userInput, out int intValue))
            {
                return intValue;
            }
            else if (double.TryParse(userInput, out double doubleValue))
            {
                return doubleValue;
            }
            else
            {
                return userInput;
            }
        }
    } // DayTwelve() in progress
    static void DayEleven()
    {
        AssessmentTask();
        static void AssessmentTask()
        {
            //static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //instead of using your desktop path, where the file could not exist, I use insude project folder
            string localPath = @"..\..\..\InputAndOutputFiles\"; // we get the parent solution directory 
            string csvFileName = "rugby_tournament_scores";
            string csvFilePath = Path.Combine(localPath, csvFileName + ".csv");
            string outputFile = Path.Combine(localPath, "Stats_" + csvFileName + ".txt");
            GoMain(csvFilePath, outputFile);
            static void GoMain(string csvFilePath, string outputFile)
            {
                /*    
                Analyse the provided CSV file and
                * Calculate the total number of tries scored for each country
                * Display the top scorer from each country
                * What is the average monthly score for each country
                * All results are outputted to the screen and a text file
                * Produce Flow Chart(s) and Pseudocode for your solution's logic
                * Code your solution using C#
                * Justify your design approach
                Bonus Tasks (A try is worth 5 points)
                * What is the average number of points scored by each country for each month? Display to the nearest point.
                * Rank the countries, highest scoring to lowest for each month of the tournament - showing total tries and points
                * Rank the countries highest to lowest for the overall tournament - show total tries and points */

                string[]? dataLines = ReadMyFile(csvFilePath);
                try
                {
                    if (dataLines == null || dataLines.Length < 2)
                    {
                        throw new Exception($"File '{csvFilePath}' does not contain enough data or the file was not found");
                    }
                }
                catch (Exception ex)
                {
                    System.Environment.Exit(0);
                }
                int dataLinesCounter = dataLines.Length;
                string[] firstName = new string[dataLinesCounter];
                string[] lastName = new string[dataLinesCounter];
                string[] countryData = new string[dataLinesCounter];
                int[] triesJan = new int[dataLinesCounter];
                int[] triesFeb = new int[dataLinesCounter];
                int[] triesMar = new int[dataLinesCounter];
                int[] triesApr = new int[dataLinesCounter];
                int[] triesMay = new int[dataLinesCounter];

                for (int iLine = 0; iLine < dataLinesCounter; iLine++)
                {
                    if (dataLines[iLine] != "")
                    {
                        string[] separatedData = dataLines[iLine].Split(",");
                        firstName[iLine] = separatedData[0];
                        lastName[iLine] = separatedData[1];
                        countryData[iLine] = separatedData[2];
                        try
                        {
                            triesJan[iLine] = int.Parse(separatedData[3]);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        try
                        {
                            triesFeb[iLine] = int.Parse(separatedData[4]);

                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        try
                        {
                            triesMar[iLine] = int.Parse(separatedData[5]);

                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        try
                        {
                            triesApr[iLine] = int.Parse(separatedData[6]);

                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        try
                        {
                            triesMay[iLine] = int.Parse(separatedData[7]);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                    }
                }

                //string[] countries = { "France", "Italy", "England", "Ireland", "Wales", "Scotland" };
                HashSet<string> countries = new HashSet<string>(countryData); // instead of hardcoding contruies, we create unique list from read contryData column

                string outputString = "";
                foreach (string country in countries)
                {
                    outputString += CalculateData(country, firstName, lastName, countryData, triesJan, triesFeb, triesMar, triesApr, triesMay);
                }
                Console.WriteLine(outputString);
                WriteMyFile(outputFile, outputString);

                static string CalculateData(string country, string[] firstName, string[] lastName, string[] countryData, int[] triesJan,
                    int[] triesFeb, int[] triesMar, int[] triesApr, int[] triesMay)
                {
                    string outputString = "";
                    int counterPerCountry = 0;
                    int total = 0;
                    string topScorer = "";
                    int topScore = 0;
                    int totalJan = 0, totalFeb = 0, totalMar = 0, totalApr = 0, totalMay = 0; //bonus task 1

                    for (int i = 0; i < countryData.Length; i++)
                    {
                        if (country == countryData[i])
                        {
                            totalJan += triesJan[i];
                            totalFeb += triesFeb[i];
                            totalMar += triesMar[i];
                            totalApr += triesApr[i];
                            totalMay += triesMay[i];
                            int currentTotal = triesJan[i] + triesFeb[i] + triesMar[i] + triesApr[i] + triesMay[i];
                            total += currentTotal;
                            if (topScore < currentTotal)
                            {
                                topScorer = firstName[i] + " " + lastName[i];
                                topScore = currentTotal;
                            }
                            counterPerCountry++;
                        }
                    }
                    double average = Math.Round((double)total / counterPerCountry, 2);
                    double averageJan = Math.Round(totalJan / (double)counterPerCountry, 2);
                    double averageFeb = Math.Round(totalFeb / (double)counterPerCountry, 2);
                    double averageMar = Math.Round(totalMar / (double)counterPerCountry, 2);
                    double averageApr = Math.Round(totalApr / (double)counterPerCountry, 2);
                    double averageMay = Math.Round(totalMay / (double)counterPerCountry, 2);
                    outputString = $"\t{new string('=',country.Length)}\n\t{country.ToUpper()}\n\t{new string('=',country.Length)}\nTotal games: {total}\nTop player: '{topScorer}' scored {topScore} goals\nAverage goals: {average}\n" +
                        $"Average Per January: {averageJan}\nAverage Per February: {averageFeb}\nAverage Per March: {averageMar}\nAverage Per April: {averageApr}\nAverage Per May: {averageMay}\n";
                    return outputString;
                }

                static string[] ReadMyFile(string csvFilePath)
                {
                    string[] dataLines;
                    try {
                        dataLines = File.ReadAllLines(csvFilePath)
                                    .Skip(1) // Skip the first line (header)
                                    .Where(line => !string.IsNullOrWhiteSpace(line)) // Skip empty lines
                                    .ToArray();
                        return dataLines;
                    }
                    catch (Exception ex) { Console.WriteLine("Error:\n" + ex.Message); }
                    return null;
                }

                static void WriteMyFile(string path, string content, bool isAdd = false)
                {
                    try
                    {
                        if (!isAdd)
                        {
                            File.WriteAllText(path, content);
                        }
                        else if (File.Exists(path))
                        {
                            File.AppendAllText(path, content);
                        }
                        else
                        {
                            File.Create(path);
                            File.AppendAllText(path, content);
                        }
                    }
                    catch
                    (Exception ex)
                    { Console.WriteLine(ex.Message); }

                }
            }
        } //AssessmentTask()
    } //DayEleven()
    static void DayTen() // SupportTickets MVP
    {
        PlayerAttendace();
        static void PlayerAttendace()
        {
            /*
             * Task Player Attendace
            For each region
            1.What is the total number of training sessions attend ?
            2.What is the average number of sessions attended ?
            3.Who attend the most sessions?
            Design using Flow Chart
            Code
            Write Results to text File
            */
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
            string csvFilePath = desktopPath + @"Players.csv";
            string[] regions = new string[] { "East", "West", "North", "South" };
            string[] dataFile = ReadFile(csvFilePath).Skip(1).ToArray();
            string[] dataLines = new string[dataFile.Length];

            foreach (string region in regions)
            {
                ProcessData(region, dataLines);
            }

            static void ProcessData(string region, string[] dataLines)
            {
                int[] regionTotalAttendance = new int[dataLines.Length];
                string topName = "";
                double average;
                int total;
                int countPlayers = 0;

                for (int i = 0; i < dataLines.Length; i++)
                {
                    string[] splitData = dataLines[i].Split(',');
                    Console.Write(splitData[0]);
                }



                Print();
            }

            static void Print()
            {

            }

            static string[] ReadFile(string path)
            {
                try
                {
                    string[] fileContent = File.ReadAllLines(path);
                    if (fileContent.Length < 2) // Checking if there's at least one data line apart from the header
                    {
                        throw new Exception($"File '{path}' does not contain enough data");
                    }
                    Console.WriteLine($"Read successfully {fileContent.Length - 1} lines (excluding header).");

                    return fileContent;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error:\n{ex.Message}");
                    return null;
                }
            }
        }

        //SupportTicketsMVP();
        static void SupportTicketsMVP()
        {
            // name division (Research, Management, Sales, Production) week1 week2 week3 week4 week5 (tickets, number)
            // total number of tickets per week [ ]
            // total number of tickets by department per week [ ]
            // person per department with most tickets per week [ ]
            // write results to a file [ ]
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
            string csvFilePath = desktopPath + @"SupportTickets.csv";
            string[] departments = new string[] { "Research", "Management", "Sales", "Production" };


            string[] fileContent = ReadAndStoreFile(csvFilePath);

            string[] ticketsPerDepartment = new string[] { };

            ParseTheData(ticketsPerDepartment);

            static void ParseTheData(string[] ticketsPerDepartment)
            {

            }

            static string[] ReadAndStoreFile(string path)
            {
                try
                {
                    string[] fileContent = File.ReadAllLines(path).Skip(1).ToArray();
                    if (fileContent.Length < 2) // Checking if there's at least one data line apart from the header
                    {
                        throw new Exception($"File '{path}' does not contain enough data");
                    }
                    Console.WriteLine($"Read successfully {fileContent.Length - 1} lines (excluding header).");

                    return fileContent;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error:\n{ex.Message}");
                    return null;
                }
            }
        } // SupportTicketsMVP()
    } //DayTen()
    static void DayNine()
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
        string csvFilePath = desktopPath + @"SupportTickets.csv";

        // Day Nine Task
        // name division (Research, Management, Sales, Production) week1 week2 week3 week4 week5 (tickets, number)
        // total number of tickets per week [done]
        // total number of tickets by department per week [done]
        // person per department with most tickets per week [done]
        // write results to a file [done]
        //
        // Conclusion: Keep this simple and hardcode as possible!!! // Task rewrite is 
        // instead of saving the names we will keep the index instead and it could be just int[,] array2D

        // ReadFile
        (string[,]? namesAndDivisions, int[,]? weeks) = ReadMyFile(csvFilePath);

        string textToFile = "";

        // extract results
        if (namesAndDivisions != null && weeks != null)
            textToFile = ExtractResults(namesAndDivisions, weeks);
        else
            Console.WriteLine("Data is null.");
        // display results
        Console.WriteLine(textToFile);
        // write to File
        WriteMyFile(desktopPath + "SummarySupportTickets.txt", textToFile, false);

        static string ExtractResults(string[,] namesAndDivisions, int[,] weeks)
        {
            string textToFile = "";
            // Display table
            textToFile+="Name\tDivision\tWeek 1\tWeek 2\tWeek 3\tWeek 4\tWeek 5\n";
            for (int row = 0; row < namesAndDivisions.GetLength(0); row++)
            {
                // Start with the name and division
                string output = $"{namesAndDivisions[row, 0]}\t{namesAndDivisions[row, 1]}";

                // Append week data
                for (int col = 0; col < weeks.GetLength(1); col++)
                {
                    output += $"\t{weeks[row, col]}";
                }
                textToFile += output + "\n";
            }
            // Console.Clear();
            // total number of tickets per week
            for (int week = 0; week < weeks.GetLength(1); week++)
            {
                int weekIndex = week;
                int total = 0;

                for (int row = 0; row < weeks.GetLength(0); row++)
                {
                    total += weeks[row, weekIndex];
                }

                textToFile += $"Total for Week {weekIndex + 1}: {total}\n";
            }
            // total number of tickets by department per week
            HashSet<string> departments = new HashSet<string>();
            for (int row = 0; row < namesAndDivisions.GetLength(0); row++)
            {
                departments.Add(namesAndDivisions[row, 1]);
            }
            for (int week = 0; week < weeks.GetLength(1); week++)
            {
                textToFile += $"Week {week + 1}:\n";

                foreach (string department in departments)
                {
                    int totalTickets = 0;
                    for (int row = 0; row < weeks.GetLength(0); row++)
                    {
                        if (namesAndDivisions[row, 1] == department)
                        {
                            totalTickets += weeks[row, week];
                        }
                    }
                    textToFile += $"\t{department}: {totalTickets}\n";
                }
            }
            // person per department with most tickets per week
            // Tuple structure: (Person's Name, Number of Tickets)
            Dictionary<string, (string, int)>[] maxTicketsPerDepartmentPerWeek =
                new Dictionary<string, (string, int)>[weeks.GetLength(1)];
            for (int week = 0; week < weeks.GetLength(1); week++)
            {
                maxTicketsPerDepartmentPerWeek[week] = new Dictionary<string, (string, int)>();
            }
            for (int week = 0; week < weeks.GetLength(1); week++)
            {
                for (int row = 0; row < weeks.GetLength(0); row++)
                {
                    string department = namesAndDivisions[row, 1];
                    string personName = namesAndDivisions[row, 0];
                    int tickets = weeks[row, week];

                    if (!maxTicketsPerDepartmentPerWeek[week].ContainsKey(department) ||
                        tickets > maxTicketsPerDepartmentPerWeek[week][department].Item2)
                    {
                        maxTicketsPerDepartmentPerWeek[week][department] = (personName, tickets);
                    }
                }
            }
            for (int week = 0; week < maxTicketsPerDepartmentPerWeek.Length; week++)
            {
                textToFile += $"Week {week + 1}:\n";
                foreach (var departmentRecord in maxTicketsPerDepartmentPerWeek[week])
                {
                    textToFile += $"\tDepartment {departmentRecord.Key}: " +
                                      $"{departmentRecord.Value.Item1} with " +
                                      $"{departmentRecord.Value.Item2} tickets\n";
                }
            }
            return textToFile;
        } //DisplayResults();
        static (string[,]?, int[,]?) ReadMyFile(string path)
        {
            try
            {
                string[] dataLines = File.ReadAllLines(path);
                if (dataLines.Length < 2) // Checking if there's at least one data line apart from the header
                {
                    throw new Exception($"File '{path}' does not contain enough data");
                }
                Console.WriteLine($"Read successfully {dataLines.Length - 1} lines (excluding header).");

                // Assuming the first row is the header row
                int numberOfDataLines = dataLines.Length - 1;
                string[,] namesAndDivisions = new string[numberOfDataLines, 2];
                int[,] weeks = new int[numberOfDataLines, 5];

                for (int row = 1; row < dataLines.Length; row++) // Start from 1 to skip the header
                {
                    string[] splitData = dataLines[row].Split(',');

                    // Assigning names and divisions
                    namesAndDivisions[row - 1, 0] = splitData[0]; // Name
                    namesAndDivisions[row - 1, 1] = splitData[1]; // Division

                    // Parsing and assigning week data
                    for (int week = 0; week < 5; week++) // Assuming there are 5 week columns
                    {
                        if (int.TryParse(splitData[week + 2], out int weekValue)) // +2 to skip name and division
                        {
                            weeks[row - 1, week] = weekValue;
                        }
                        else
                        {
                            // Handle invalid data; set to 0 or appropriate default
                            weeks[row - 1, week] = 0;
                        }
                    }
                }

                return (namesAndDivisions, weeks);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:\n{ex.Message}");
                return (null, null);
            }
        } //ReadMyFile()
        static bool WriteMyFile(string path, string textToFile, bool isToAdd = true)
        {
            string? directoryPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Console.WriteLine($"Directory doesnt' exist, we created {directoryPath}");
            }
            try
            {
                if (!isToAdd)
                    File.WriteAllText(path, textToFile);
                else
                    File.AppendAllText(path, textToFile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        } // WriteMyFile()
    } // DayNine()
    static void DayEight()
    {
        // ComeBackToDaySeven();
        // ReWriteCSV();

        static void ReWriteCSV()
        {
            const int IDX_FN = 0;
            const int IDX_LN = 1;
            const int IDX_AGE = 2;
            const int IDX_GROUP = 3;
            const int IDX_EMPLOYER = 4;

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
            string csvFilePath = desktopPath + @"SampleCSV.csv";
            try
            {
                string[]? dataLines = new string[] { };
                dataLines = File.ReadAllLines(csvFilePath);
                if (dataLines.Length == 0 ) {
                    throw new Exception($"File '{csvFilePath}' does not contain data");
                }
                string[]? apprentices = dataLines.Skip(1).ToArray();
                string[] headers = dataLines[0].Split(',');
                dataLines = null;
                int numberApprentices = apprentices.Length;
                string[] firstName = new string[numberApprentices];
                string[] lastName = new string[numberApprentices];
                int[] apprentAge = new int[numberApprentices];
                string[] group = new string[numberApprentices];
                string[] employer = new string[numberApprentices];

                for (int i = 0; i < numberApprentices; i++) // skip the headers starting from index 1
                {
                    string[] dataFields = apprentices[i].Split(',');
                    apprentAge[i] = Convert.ToInt32(dataFields[IDX_AGE]);
                    firstName[i] = dataFields[IDX_FN];
                    lastName[i] = dataFields[IDX_LN];
                    group[i] = dataFields[IDX_GROUP];
                    employer[i] = dataFields[IDX_EMPLOYER];
                }
                DisplayAgeInfo(firstName, lastName, apprentAge);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error:\n{ex.Message}");
            }
            
            static void DisplayAgeInfo(string[] firstName, string[] lastName,int[] apprentAge)
            {
                Console.WriteLine($"There are {apprentAge.Length} students");
                Console.WriteLine($"The youngest student is {apprentAge.Min()}");
                Console.WriteLine($"The oldest student is {apprentAge.Max()}");
                Console.WriteLine($"The average age of all students {apprentAge.Average():F2}");
                for (int i = 0; i < apprentAge.Length; i++)
                {
                    Console.WriteLine($"Student #{i + 1} '{firstName[i]} {lastName[i]}' aged '{apprentAge[i]}'");

                }
            }
        }
        static void ComeBackToDaySeven()
        { 
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
        string csvFilePath = desktopPath + @"SampleCSV.csv";
        string[] importedCSV = File.Exists(csvFilePath) ? File.ReadAllLines(csvFilePath) : new string[] { };
        int totalLines = importedCSV.Length;
        bool isHeader=true;

        List<int> ages = new List<int>();
        List<string> names = new List<string>();

        for (int i = 1; i < totalLines; i++)
        {
            string[] dataFields = importedCSV[i].Split(",");
            ages.Add(int.Parse(dataFields[2]));
        }
        } // comeBackToDaySeven()
    } // DayEight()
    static void DaySeven()
    {
        // CSV
        // 16bits = 2 bytes to store one character
        //ReadViaStreamReaderMethod();
        ReadViaFileMethod();

        static void ReadViaFileMethod()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
            string csvFilePath = desktopPath + @"SampleCSV.csv";

            string[] importedCSV = File.ReadAllLines(csvFilePath);
            int totalLines = importedCSV.Length;

            /* var table = new Table();
            table.LoadFromCsvArray(importedCSV);
            Console.WriteLine(table.ToString()); 
            
            for (int i = 0;i < importedCSV.Length; i++) {
            Console.WriteLine($"Line {i+1}: {importedCSV[i]}");
            }*/
            int linecount = 0;
            int totalAge = 0;
            
            int skipNumberLines = 0;
            bool isHeader = false;
            if (isHeader) {
                skipNumberLines = 1;
            }
            // never use CristianName or SurName, always use FirstName and LastName.
            string[] studentNames = new string[totalLines];
            int[] ages = new int[totalLines];
            string rawHeader;

            foreach (string line in importedCSV.Skip(skipNumberLines))
            {
                Console.WriteLine($"Line #{linecount + 1}: {line}");
                string[] dataFields = line.Split(",");
                //Console.WriteLine(dataFields.Length);
                if (!isHeader)
                {
                    int age = Convert.ToInt32(dataFields[2]);
                    totalAge += age;
                    studentNames[linecount] = dataFields[0] + " " + dataFields[1];
                    ages[linecount] = age;
                }
                else
                {
                    if (linecount == 0)
                        rawHeader = line;
                    isHeader = false;
                }
                linecount++;
            }

            for (int i=0;  i < totalLines; i++)
            {
                Console.WriteLine($"Student #{i+1} '{studentNames[i]}' aged '{ages[i]}'");
            }

            //double averageAge = Math.Round((double)totalAge / linecount ,2);
            //string averageAge = String.Format("{0:F2}", (double)totalAge / linecount); // wromg return

            //Console.WriteLine($"Total age: '{totalAge}', Mean age: {averageAge}");

            Console.WriteLine($"There are {ages.Length} students");
            Console.WriteLine($"The youngest student is {ages.Min()}");
            Console.WriteLine($"The oldest student is {ages.Max()}");
            Console.WriteLine($"The average age of all students {ages.Average():F2}");
            Console.WriteLine($"The average age of all students {Math.Round(ages.Average(),2)}");


        } // ReadViaFileMethod()
        static void ReadViaStreamReaderMethod() { 
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
        string csvFilePath = desktopPath + @"SampleCSV.csv";
        int lineCount = File.ReadLines(csvFilePath).Count();
        string[] importedCSV = new string[lineCount];
        importedCSV = ImporterSR(csvFilePath);
        string[] outputToTable = new string[importedCSV.Length];
        string outputToString = "";

        for (int i = 0; i < importedCSV.Length; i++)
        {
            string tmp = importedCSV[i].Replace(",", "\t");
            outputToTable[i] = tmp;
            outputToString += tmp + "\n";
        }

        Console.WriteLine($"The header line is: '{outputToTable[0]}'");
        Console.WriteLine($"Tenth data line is: '{outputToTable[10]}'");
        Console.WriteLine($"Number of data lines: '{outputToTable.Length}'");
        //Console.WriteLine($"\nWhole table here:\n{outputToString}");

        var table = new TableBuilder();
        table.LoadFromCsvArray(importedCSV);
        Console.WriteLine(table.ToString());

        static string[] ImporterSR(string path)
        {
            StreamReader sr;
            if (File.Exists(path))
            {
                var lineCount = File.ReadLines(path).Count();
                string[] array = new string[lineCount];

                sr = new StreamReader(path);
                int counter = 0;
                while (!sr.EndOfStream)
                {
                    try
                    {
                        array[counter] = sr.ReadLine();
                        counter++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                sr.Close();
                return array;
            }
            else
                return null;
        } // Importer();
        } //ReadViaStreamReaderMethod()
    } // DaySeven()
    static void DaySix()
    {
        //CreateTextFile ();
        //MyStreamWriter();
        MyStreamWriterPrompt();

        static void MyStreamWriterPrompt()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
            string desktopFilePath = desktopPath + @"simple.txt";
            string helloText = "Hello World!";

            Console.Write("Do you want to (R)ead or (W)rite the file? (R/W) > ");
            string? answer = Console.ReadLine().ToLower();

            if (answer == "r")
            {
                if (ReadMyFile(desktopFilePath))
                {
                    Console.WriteLine($"We successfully read the file: {desktopFilePath}");
                }
                else
                {
                    Console.WriteLine($"By mystery mistake we couldn't not read the file");
                }
            }
            else if (answer == "w")
            {
                if (WriteMyFile(desktopFilePath, helloText))
                {
                    Console.WriteLine($"We successfully added text to the file: {desktopFilePath}");
                }
                else
                {
                    Console.WriteLine($"By mystery mistake we couldn't write the file");
                }
            }
            static bool ReadMyFile(string path)
            {
                Console.Clear();
                string? directoryPath = Path.GetDirectoryName(path);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    Console.WriteLine($"Directory doesnt' exist, we created {directoryPath}");
                }
                if (!File.Exists(path))
                {
                    File.Create(path);
                    Console.WriteLine($"File doesnt' exist, we created {path}");
                }
                try
                {
                    using (StreamReader fileReader = new StreamReader(path))
                    {
                        Console.WriteLine("Do you want to read the first (3) lines or (F)ull file? (3/F)");
                        string answerReader = Console.ReadLine().ToLower();
                        if (answerReader == "3")
                        {
                            string str = "";
                            Console.WriteLine($"File content:\n");
                            for (int i = 1; i < 4; i++)
                            { 
                                str += fileReader.ReadLine() + "\n";
                            }
                            Console.WriteLine(str);
                        }
                        else if (answerReader == "f")
                        {
                            Console.WriteLine($"File content:\n{fileReader.ReadToEnd()}");
                        }
                        fileReader.Close();
                        return true; 
                    }
                }
                catch (DirectoryNotFoundException ed)
                {
                    Console.WriteLine($"Error: '{ed.Message}'\nFile doesn't exist");
                }
                catch (FileNotFoundException ef)
                {
                    Console.WriteLine($"Error: '{ef.Message}'\nFile doesn't exist");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return false;
            }
            static bool WriteMyFile(string path, string text, bool add=true)
            {
                string? directoryPath = Path.GetDirectoryName(path);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    Console.WriteLine($"Directory doesnt' exist, we created {directoryPath}");
                }
                try
                {
                    using (StreamWriter fileWriter = new StreamWriter(path,add))
                    {
                        fileWriter.Write(text, add);
                        fileWriter.Close();
                        return true;
                    }
                }
                catch (DirectoryNotFoundException ed)
                {
                    Console.WriteLine($"Error: '{ed.Message}'\nFile doesn't exist");
                }
                catch (FileNotFoundException ef)
                {
                    Console.WriteLine($"Error: '{ef.Message}'\nFile doesn't exist");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return false;
            } // WriteMyFile()
        } // MyStreamWriterPrompt()
        static void MyStreamWriter()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\";
            string desktopFilePath = desktopPath + @"simple.txt";
            string helloText = "Hello World!";
            
            Console.WriteLine(desktopFilePath);

            // creating directory if it doesn't exist
            string? directoryPath = Path.GetDirectoryName(desktopFilePath);
            if (false) //(!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Console.WriteLine($"Directory doesnt' exist, we created {directoryPath}");
            }
            try
            {
                using (StreamWriter fileWriter = new StreamWriter(desktopFilePath))
                { 
                    fileWriter.WriteLine(helloText, true);
                    fileWriter.Close();
                }
            }
            catch (DirectoryNotFoundException ed)
            { Console.WriteLine($"Error: '{ed.Message}'\nFile doesn't exist"); 
            }
            catch (FileNotFoundException ef)
            {
                Console.WriteLine($"Error: '{ef.Message}'\nFile doesn't exist");
            }
            catch (Exception e)
            {  
                Console.WriteLine(e.Message);
            }


            //fileWriter.Close(); //unnessesarry but could be understandable

        } // MyStreamWriter()
        static void CreateTextFile()
        {
            int lines = 10;
            string helloText = "Hello World!";
            string fileText = "";

            for (int i = 1; i <= lines; i++)
            {
                fileText += "Line #"+i+": "+ helloText + "\n";
            }
            Console.WriteLine(fileText);
            File.WriteAllText("helloworld10.txt", fileText);
        } // CreateTextFile()
    } // DaySix()
    static void DayFive()
    { // Improvements to day four code
        /* if the function returns true or false, it should start with isPolledPeople?
        
        //categoryID = personAge switch
        //{
        //    < 13 => CategoryID.Child,
        //    >= 13 and <= 19 => CategoryID.Teenager,
        //    > 19 and <= 25 => CategoryID.YoungAdult,
        //    > 25 and < 65 => CategoryID.Adult,
        //    _ => CategoryID.Older
        //};

        Find our company's Code Convention.
        Pick a style and stick to it
        Do not be ambiguous with naming

        Overloading is when a method can be called with different arguments and scenarios.

        string myName = "Tom";
        string firstLetter = myName[0]; ?? char explanation

        int 1someVar = 0; // invalid
        int _1 = 0; // valid
        int them-all = 0; // invalid
        int ______ = 0; // valid
        int _size = 0; // valid

        char some = 'a'; == string some = 'a';

        float pi = 3.14f;
        Console.WriteLine(pi);

        */
        //DayFiveTeacherApproach2();
        //RollDiceApproach1(); // Task RollDice
        //RollDiceApproach2(); // Task RollDice
        //RollDiceValidation();
        RollDiceValidationThreads();

        static void RollDiceValidationThreads()
        {
            // Define the number of iterations and an array to hold counts for each possible value.
            long totalIterations = 100000000; // 100m is safe can be finished for 8.4s, 1b takes 82,5 seconds
            int[] counts = new int[6];

            // Run the loop in parallel.
            Parallel.For(0, totalIterations, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, _ =>
            {
                Random rnd = new Random(); // Local Random instance to avoid thread contention.
                int index = rnd.Next(1, 7); // Get a random value between 1 and 6(5).
                Interlocked.Increment(ref counts[index - 1]); // Safely increment the count for this index.
            });

            // Calculate percentages.
            double[] percentages = new double[6];
            for (int i = 0; i < counts.Length; i++)
            {
                percentages[i] = Math.Round((counts[i] / (double)totalIterations) * 100, 2);
            }
            for (int i = 0 ; i < percentages.Length ; i++)
            {
                Console.WriteLine($"The probability chance for number {i + 1} is {percentages[i]}%");
            }
        }
        static void RollDiceValidation()
        {
            int[] counter = new int[6];
            int testRuns = 10000000;
            double persentRuns = 100 / testRuns; // added to test

            for (int i = 0; i < testRuns; i++)
            {
                switch (RollDice())
                {
                    case 1: counter[0]++; break;
                    case 2: counter[1]++; break;
                    case 3: counter[2]++; break;
                    case 4: counter[3]++; break;
                    case 5: counter[4]++; break;
                    case 6: counter[5]++; break;
                }
            }
            Console.WriteLine($"Through {testRuns} runs:");
            for (int i = 0; i < counter.Length; i++)
            {
                Console.WriteLine($"The probability chance for number {i + 1} is {(double)counter[i] / testRuns * 100:0.00}%");
                //Console.WriteLine($"The probability chance for number {i + 1} is {(double)counter[i] * persentRuns}%");
            }
            static int RollDice()
            {
                Random randomNumber = new Random();
                return randomNumber.Next(1, 6);
            }
        }
        static void RollDiceApproach2()
        {

            int counter = 0;
            while (counter < 10)
            {
                counter++;
                Console.WriteLine($"Roll # {counter} => Dice value: {getDiceValue()}");
            }
            static int getDiceValue()
            {
                Random randomNumber = new Random();
                switch (randomNumber.NextDouble())
                {
                    case < 0.16666: return 1;
                    case <= 0.33333: return 2;
                    case <= 0.50000: return 3;
                    case <= 0.66666: return 4;
                    case <= 0.83333: return 5;
                    default: return 6;
                }
            }
        }
        static void RollDiceApproach1()
        {
            bool isRollDice = true;
            Random randomNumber = new Random();
            do
            {
                Console.Write("Do you wish to roll the dice? (Y/N) > ");
                isRollDice = Console.ReadLine().ToLower() == "y";
                Console.WriteLine($"Your dice is {randomNumber.Next(1, 6)}");
            }
            while (isRollDice);
        }
        static void DayFiveTeacherApproach2()
        {
            int[] numberPolled = new int[6];
            int[] totalAges = new int[6];
            int[] youngestAges = new int[6];
            int[] oldestAges = new int[6];
            string[] Categories = new string[] { "General", "Child", "Teenager", "Young Adult", "Adult", "Older Citizen" };
            while (PersonPoll())
            {
                Console.Write($"What is the age of person >");
                int personAge = Convert.ToInt32(Console.ReadLine());
                UpdateCategoryValues(CategoryID.General, personAge, numberPolled, totalAges, youngestAges, oldestAges);
                // Find the spceific category based on the age entered and update values
                UpdateCategoryValues(AgeCategory(personAge), personAge, numberPolled, totalAges, youngestAges, oldestAges);
            }

            static CategoryID AgeCategory(int age)
            {
                switch (age)
                {
                    case < 13: return CategoryID.Child;
                    case <= 19: return CategoryID.Teenager;
                    case <= 25: return CategoryID.YoungAdult;
                    case < 65: return CategoryID.Adult;
                    default: return CategoryID.Older;
                }
            } // AgeCategory();

            DisplayResults(Categories, totalAges, numberPolled, youngestAges, oldestAges);

            static void DisplayResults(string[] category, int[] totalAge, int[] pollCount, int[] youngestAge, int[] oldestAge)
            {
                for (int i = 0; i < pollCount.Length; i++)
                {
                    Console.WriteLine($"========== {category[i]} ==============");
                    if (pollCount[i] > 0)
                    {
                        Console.WriteLine($"Number polled is {pollCount[i]}");
                        Console.WriteLine($"The youngest person is aged {youngestAge[i]}");
                        Console.WriteLine($"The oldest person is aged {oldestAge[i]}");
                        float meanAge = (float)totalAge[i] / pollCount[i];
                        Console.WriteLine($"The mean age is {meanAge}");
                    }
                    Console.WriteLine("===============================}");
                }
            } //DisplayResults
            static bool PersonPoll()
            {
                Console.Write("have you someone to poll? (Y/N) >");
                return Console.ReadLine().ToUpper() == "Y";

            } //PersonPoll
            static void UpdateCategoryValues(CategoryID categoryID, int ageEntered, int[] numberPolled, int[] totalAges, int[] youngestAges, int[] oldestAges)
            {

                numberPolled[(int)categoryID]++;
                totalAges[(int)(categoryID)] += ageEntered;

                if (numberPolled[(int)categoryID] == 1)
                {
                    youngestAges[(int)categoryID] = ageEntered;
                }
                if (ageEntered > oldestAges[(int)categoryID])
                {
                    oldestAges[(int)(categoryID)] = ageEntered;
                }
                if ((ageEntered < youngestAges[(int)categoryID]))
                {
                    youngestAges[(int)categoryID] = ageEntered;
                }
            } // UpdateCategoryValues()
        } // DayThreeTeacherApproach2()
    }// DayFive()
    static void DayFour()
    {
        // DayFourApproach3();
        // DayFourApproach4();
        DayThreeTeacherApproach2();
        static void DayFourApproach3()
        {
            int[] age = new int[ReAskNumber("Please enter the number of people")];
            for (int i = 0; i < age.Length; i++)
            {
                age[i] = ReAskNumber($"Please enter age for person {i + 1}");
            }
            int totalPeople = age.Length;

            int[] catChild = new int[totalPeople];
            int[] catTeenager = new int[totalPeople];
            int[] catYoungAdult = new int[totalPeople];
            int[] catAdult = new int[totalPeople];
            int[] catOlderCitizen = new int[totalPeople];

            for (int i = 0; i < totalPeople; i++)
            {
                if (age[i] <= 0)
                {
                    Console.WriteLine($"Error. The age for person {i + 1} is could not be 0 or less");
                }
                else if (age[i] > 0 && age[i] <= 12) // Child Category
                {
                    catChild[i] = age[i];
                }
                else if (age[i] >= 13 && age[i] <= 19) // Teenager Category
                {
                    catTeenager[i] = age[i];
                }
                else if (age[i] >= 20 && age[i] <= 24) // Young Adult Category
                {
                    catYoungAdult[i] = age[i];
                }
                else if (age[i] >= 25 && age[i] <= 65) // Adult Category
                {
                    catAdult[i] = age[i];
                }
                else if (age[i] >= 66) // Older Citizen Category
                {
                    catOlderCitizen[i] = age[i];
                }
            }
            Console.Clear();
            PrintStats("", age);
            PrintStats("Child", catChild);
            PrintStats("Teenager", catTeenager);
            PrintStats("Young Adult", catYoungAdult);
            PrintStats("Adult", catAdult);
            PrintStats("Older Citizen", catOlderCitizen);
        }
        static void DayFourApproach4()
        {

            int totalPeople = ReAskNumber("Please enter the number of users");
            int[] age = new int[totalPeople];
            int count = 0;
            while (count <= totalPeople)
            {
                age[count] = ReAskNumber($"Please enter age for student {age.Length + 1}");
                count++;
            }
            int[] catChild = new int[totalPeople];
            int[] catTeenager = new int[totalPeople];
            int[] catYoungAdult = new int[totalPeople];
            int[] catAdult = new int[totalPeople];
            int[] catOlderCitizen = new int[totalPeople];
            count = 0;
            while (count <= totalPeople)
            {
                if (age[count] <= 0)
                {
                    Console.WriteLine($"Error. The age for person {count + 1} is could not be 0 or less");
                }
                else if (age[count] > 0 && age[count] <= 12) // Child Category
                {
                    catChild[count] = age[count];
                }
                else if (age[count] >= 13 && age[count] <= 19) // Teenager Category
                {
                    catTeenager[count] = age[count];
                }
                else if (age[count] >= 20 && age[count] <= 24) // Young Adult Category
                {
                    catYoungAdult[count] = age[count];
                }
                else if (age[count] >= 25 && age[count] <= 65) // Adult Category
                {
                    catAdult[count] = age[count];
                }
                else if (age[count] >= 66) // Older Citizen Category
                {
                    catOlderCitizen[count] = age[count];
                }
            }

        }
        static void DayFourApproach5()
        {
            Program.CategoryID CategoryID;

            bool anyOtherPeople = true;
            int ageCount = 0;
            int totalAge = 0;
            int meanAge = 0;

            int personAge = 0;
            int oldestAge = 0;
            int youngestAge = 0;

            int personAgeChild = 0;
            int oldestAgeChild = 0;
            int youngestAgeChild = 0;

            int personAgeTeenage = 0;
            int oldestAgeTeenage = 0;
            int youngestAgeTeenage = 0;

            int personAgeYoungerAdult = 0;
            int oldestAgeYoungerAdult = 0;
            int youngestAgeYoungerAdult = 0;

            int personAgeAdult = 0;
            int oldestAgeAdult = 0;
            int youngestAgeAdult = 0;

            int personAgeOlder = 0;
            int oldestAgeOlder = 0;
            int youngestAgeOlder = 0;

            while (anyOtherPeople)
            {
                personAge = ReAskNumber($"Please enter age for Person {ageCount + 1}");
                ageCount++;
                totalAge += personAge;

                if (personAge > oldestAge)
                {
                    oldestAge = personAge;
                }
                if (personAge != 0 && personAge > youngestAge)
                {
                    youngestAge = personAge;
                }
                if (anyOtherPeople && ageCount > 0)
                {
                    meanAge = totalAge / ageCount;
                    Console.WriteLine($"Person {ageCount} age is {personAge}" +
                        $"\nMean age is {meanAge}" +
                        $"\nTotal age is {totalAge}" +
                        $"\nOldest Person {oldestAge}" +
                        $"\nYoungest Person {youngestAge}");
                }
                //Exit function                 //if (Console.ReadKey().Key == ConsoleKey.N) anyOtherPeople = false;
                Console.Write("Have you someone else to enter? (Y/N) > ");
                if (Console.ReadLine().ToLower() != "y") anyOtherPeople = false;
            }

            /*[] catChild = new int[totalPeople];
            int[] catTeenager = new int[totalPeople];
            int[] catYoungAdult = new int[totalPeople];
            int[] catAdult = new int[totalPeople];
            int[] catOlderCitizen = new int[totalPeople];
            ageCount = 0;
             while(ageCount <= totalPeople)
            {
                if (age[ageCount] <= 0)
                {
                    Console.WriteLine($"Error. The age for person {ageCount + 1} is could not be 0 or less");
                }
                else if (age[ageCount] > 0 && age[ageCount] <= 12) // Child Category
                {
                    catChild[ageCount] = age[ageCount];
                }
                else if (age[ageCount] >= 13 && age[ageCount] <= 19) // Teenager Category
                {
                    catTeenager[ageCount] = age[ageCount];
                }
                else if (age[ageCount] >= 20 && age[ageCount] <= 24) // Young Adult Category
                {
                    catYoungAdult[ageCount] = age[ageCount];
                }
                else if (age[ageCount] >= 25 && age[ageCount] <= 65) // Adult Category
                {
                    catAdult[ageCount] = age[ageCount];
                }
                else if (age[ageCount] >= 66) // Older Citizen Category
                {
                    catOlderCitizen[ageCount] = age[ageCount];
                }
            }
            
            Console.Clear();
            PrintStats("", age);
            PrintStats("Child", catChild);
            PrintStats("Teenager", catTeenager);
            PrintStats("Young Adult", catYoungAdult);
            PrintStats("Adult", catAdult);
            PrintStats("Older Citizen", catOlderCitizen);*/
        }
        static void DayThreeTeacherApproach1()
        {
            int numberPeoplePolled = 0; // Number of People Polled
            int totalAge = 0;
            int oldestAge = 0;
            int youngestAge = 0;
            int numberPeoplePolledChild = 0; // Number of People Polled
            int totalAgeChild = 0;
            int oldestAgeChild = 0;
            int youngestAgeChild = 0;
            int numberPeoplePolledTeenager = 0; // Number of People Polled
            int totalAgeTeenager = 0;
            int oldestAgeTeenager = 0;
            int youngestAgeTeenager = 0;
            int numberPeoplePolledYoungAdult = 0; // Number of People Polled
            int totalAgeYoungAdult = 0;
            int oldestAgeYoungAdult = 0;
            int youngestAgeYoungAdult = 0;
            int numberPeoplePolledAdult = 0; // Number of People Polled
            int totalAgeAdult = 0;
            int oldestAgeAdult = 0;
            int youngestAgeAdult = 0;
            int numberPeoplePolledOlderCitizen = 0; // Number of People Polled
            int totalAgeOlderCitizen = 0;
            int oldestAgeOlderCitizen = 0;
            int youngestAgeOlderCitizen = 0;
            Console.Write("have you someone to poll? (Y/N) >");
            bool isPersonPolled = Console.ReadLine().ToUpper() == "Y";

            while (isPersonPolled)
            {
                numberPeoplePolled++;
                Console.Write($"What is the age of person #{numberPeoplePolled} >");
                int personAge = Convert.ToInt32(Console.ReadLine());
                totalAge += personAge;
                if (numberPeoplePolled == 1) // First person is the youngest and oldest
                {
                    oldestAge = personAge;
                    youngestAge = personAge;
                }
                else
                {
                    if (personAge > oldestAge)
                    {
                        oldestAge = personAge;
                    }
                    if (personAge < youngestAge)
                    {
                        youngestAge = personAge;
                    }
                }

                // A child
                if (personAge < 13)
                {
                    totalAgeChild += personAge;
                    numberPeoplePolledChild++;
                    if (numberPeoplePolledChild == 1) // First person is the youngest and oldest
                    {
                        oldestAgeChild = personAge;
                        youngestAgeChild = personAge;
                    }
                    else
                    {
                        if (personAge > oldestAgeChild)
                        {
                            oldestAgeChild = personAge;
                        }
                        if (personAge < youngestAgeChild)
                        {
                            youngestAgeChild = personAge;
                        }
                    }
                }
                else if (personAge <= 19) // Teenager
                {
                    totalAgeTeenager += personAge;
                    numberPeoplePolledTeenager++;
                    if (numberPeoplePolledTeenager == 1) // First person is the youngest and oldest
                    {
                        oldestAgeTeenager = personAge;
                        youngestAgeTeenager = personAge;
                    }
                    else
                    {
                        if (personAge > oldestAgeTeenager)
                        {
                            oldestAgeTeenager = personAge;
                        }
                        if (personAge < youngestAgeTeenager)
                        {
                            youngestAgeTeenager = personAge;
                        }
                    }
                }
                else if (personAge <= 25)
                {
                    totalAgeYoungAdult += personAge;
                    numberPeoplePolledYoungAdult++;
                    if (numberPeoplePolledYoungAdult == 1) // First person is the youngest and oldest
                    {
                        oldestAgeYoungAdult = personAge;
                        youngestAgeYoungAdult = personAge;
                    }
                    else
                    {
                        if (personAge > oldestAgeYoungAdult)
                        {
                            oldestAgeYoungAdult = personAge;
                        }
                        if (personAge < youngestAgeYoungAdult)
                        {
                            youngestAgeYoungAdult = personAge;
                        }
                    }
                }
                else if (personAge < 65)
                {
                    totalAgeAdult += personAge;
                    numberPeoplePolledAdult++;
                    if (numberPeoplePolledAdult == 1) // First person is the youngest and oldest
                    {
                        oldestAgeAdult = personAge;
                        youngestAgeAdult = personAge;
                    }
                    else
                    {
                        if (personAge > oldestAgeAdult)
                        {
                            oldestAgeAdult = personAge;
                        }
                        if (personAge < youngestAgeAdult)
                        {
                            youngestAgeAdult = personAge;
                        }
                    }
                }
                else
                {
                    totalAgeOlderCitizen += personAge;
                    numberPeoplePolledOlderCitizen++;
                    if (numberPeoplePolledOlderCitizen == 1) // First person is the youngest and oldest
                    {
                        oldestAgeOlderCitizen = personAge;
                        youngestAgeOlderCitizen = personAge;
                    }
                    else
                    {
                        if (personAge > oldestAgeOlderCitizen)
                        {
                            oldestAgeOlderCitizen = personAge;
                        }
                        if (personAge < youngestAgeOlderCitizen)
                        {
                            youngestAgeOlderCitizen = personAge;
                        }
                    }
                }
                Console.Write("have you someone to poll? (Y/N) >");
                isPersonPolled = Console.ReadLine().ToUpper() == "Y";

            }

            Console.WriteLine($"The final total of age {totalAge}");
            Console.WriteLine($"The total number of perople polled {numberPeoplePolled}");
            Console.WriteLine($"The youngest person is aged {youngestAge}");
            Console.WriteLine($"The oldest person is aged {oldestAge}");
            float meanAge = totalAge / numberPeoplePolled;
            Console.WriteLine($"The mean age is {meanAge}");
            Console.WriteLine($"The final total of age {totalAgeChild}");
            Console.WriteLine($"The total number of perople polled {numberPeoplePolledChild}");
            Console.WriteLine($"The youngest person is aged {youngestAgeChild}");
            Console.WriteLine($"The oldest person is aged {oldestAgeChild}");
            float meanAgeChild = totalAgeChild / numberPeoplePolledChild;
            Console.WriteLine($"The mean age is {meanAgeChild}");
            Console.WriteLine($"The final total of age {totalAgeTeenager}");
            Console.WriteLine($"The total number of perople polled {numberPeoplePolledTeenager}");
            Console.WriteLine($"The youngest person is aged {youngestAgeTeenager}");
            Console.WriteLine($"The oldest person is aged {oldestAgeTeenager}");
            float meanAgeTeenager = totalAgeTeenager / numberPeoplePolledTeenager;
            Console.WriteLine($"The mean age is {meanAgeTeenager}");
            Console.WriteLine($"The final total of age {totalAgeYoungAdult}");
            Console.WriteLine($"The total number of perople polled {numberPeoplePolledYoungAdult}");
            Console.WriteLine($"The youngest person is aged {youngestAgeYoungAdult}");
            Console.WriteLine($"The oldest person is aged {oldestAgeYoungAdult}");
            float meanAgeYoungAdult = totalAgeYoungAdult / numberPeoplePolledYoungAdult;
            Console.WriteLine($"The mean age is {meanAgeYoungAdult}");
            Console.WriteLine($"The final total of age {totalAgeAdult}");
            Console.WriteLine($"The total number of perople polled {numberPeoplePolledAdult}");
            Console.WriteLine($"The youngest person is aged {youngestAgeAdult}");
            Console.WriteLine($"The oldest person is aged {oldestAgeAdult}");
            float meanAgeAdult = totalAgeAdult / numberPeoplePolledAdult;
            Console.WriteLine($"The mean age is {meanAgeAdult}");
            Console.WriteLine($"The final total of age {totalAgeOlderCitizen}");
            Console.WriteLine($"The total number of perople polled {numberPeoplePolledOlderCitizen}");
            Console.WriteLine($"The youngest person is aged {youngestAgeOlderCitizen}");
            Console.WriteLine($"The oldest person is aged {oldestAgeOlderCitizen}");
            float meanAgeOlderCitizen = totalAgeOlderCitizen / numberPeoplePolledOlderCitizen;
            Console.WriteLine($"The mean age is {meanAgeOlderCitizen}");

        }
        static void DayThreeTeacherApproach2()
        {
            int[] numberPolled = new int[6];
            int[] totalAges = new int[6];
            int[] youngestAges = new int[6];
            int[] oldestAges = new int[6];
            CategoryID categoryID;
            string[] Categories = new string[] { "General", "Child", "Teenager", "Young Adult", "Adult", "Older Citizen" };
            while (PersonPoll())
            {
                Console.Write($"What is the age of person >");
                int personAge = Convert.ToInt32(Console.ReadLine());
                UpdateCategoryValues(CategoryID.General, personAge, numberPolled, totalAges, youngestAges, oldestAges);
                // Find the spceific category based on the age entered

                if (personAge < 13)
                {
                    categoryID = CategoryID.Child;
                }
                else if (personAge <= 19) // Teenager
                {
                    categoryID = CategoryID.Teenager;
                }
                else if (personAge <= 25)
                {
                    categoryID = CategoryID.YoungAdult;
                }
                else if (personAge < 65)
                {
                    categoryID = CategoryID.Adult;
                }
                else
                {
                    categoryID = CategoryID.Older;
                }
                UpdateCategoryValues(categoryID, personAge, numberPolled, totalAges, youngestAges, oldestAges);
            } //while()
            for (int i = 0; i < numberPolled.Length; i++)
            {
                DisplayResults(Categories[i], totalAges[i], numberPolled[i], youngestAges[i], oldestAges[i]);
            }
            static void DisplayResults(string category, int totalAge, int pollCount, int youngestAge, int oldestAge)
            {
                Console.WriteLine($"========== {category} ==============");
                if (pollCount > 0)
                {
                    Console.WriteLine($"Number polled is {pollCount}");
                    Console.WriteLine($"The youngest person is aged {youngestAge}");
                    Console.WriteLine($"The oldest person is aged {oldestAge}");
                    float meanAge = totalAge / pollCount;
                    Console.WriteLine($"The mean age is {meanAge}");
                }
                Console.WriteLine("===============================}");
            }//DisplayResults
            static bool PersonPoll()
            {
                Console.Write("have you someone to poll? (Y/N) >");
                return Console.ReadLine().ToUpper() == "Y";

            } //PersonPoll
            static void UpdateCategoryValues(CategoryID categoryID, int ageEntered, int[] numberPolled, int[] totalAges, int[] youngestAges, int[] oldestAges)
            {
                numberPolled[(int)categoryID]++;
                totalAges[(int)(categoryID)] += ageEntered;
                if (numberPolled[(int)categoryID] == 1)
                {
                    youngestAges[(int)categoryID] = ageEntered;
                }
                if (ageEntered > oldestAges[(int)categoryID])
                {
                    oldestAges[(int)(categoryID)] = ageEntered;
                }
                if ((ageEntered < youngestAges[(int)categoryID]))
                {
                    youngestAges[(int)categoryID] = ageEntered;
                }
            } // UpdateCategoryValues()
        } // DayThreeTeacherApproach2()
    } // DayFour()
    static void DayThree()
    {
        // DayThreeApproach1();
        DayThreeApproach2();

        static void DayThreeApproach1()
        {
            List<int> age = new List<int>();
            age.Add(ReAskNumber("Please enter the number of users"));
            do
            {
                age.Add(ReAskNumber($"Please enter age for student{age.Count}"));
            }
            while (age.Count <= age[0]);
            Console.Write($"Age count is '{age.Count}' and total students are '{age[0]}', and the list values are ");
            age.ForEach(p => Console.WriteLine(p + " "));
        }
        static void DayThreeApproach2()
        {
            int[] age = new int[ReAskNumber("Please enter the number of people")];
            int[] catChild = new int[] { };
            int[] catTeenager = new int[] { };
            int[] catYoungAdult = new int[] { };
            int[] catAdult = new int[] { };
            int[] catOlderCitizen = new int[] { };

            for (int i = 0; i < age.Length; i++)
            {
                age[i] = ReAskNumber($"Please enter age for person {i + 1}");
                if (age[i] <= 0)
                {
                    Console.WriteLine($"Error. The age for person {i + 1} is could not be 0 or less");
                }
                else if (age[i] >= 0 && age[i] <= 12) // Child Category
                {
                    catChild = catChild.Concat(new int[] { age[i] }).ToArray();
                }
                else if (age[i] >= 13 && age[i] <= 19) // Teenager Category
                {
                    catTeenager = catTeenager.Concat(new int[] { age[i] }).ToArray();
                }
                else if (age[i] >= 20 && age[i] <= 24) // Young Adult Category
                {
                    catYoungAdult = catYoungAdult.Concat(new int[] { age[i] }).ToArray();
                }
                else if (age[i] >= 25 && age[i] <= 65) // Adult Category
                {
                    catAdult = catAdult.Concat(new int[] { age[i] }).ToArray();
                }
                else if (age[i] >= 66) // Older Citizen Category
                {
                    catOlderCitizen = catOlderCitizen.Concat(new int[] { age[i] }).ToArray();
                }
            }

            Console.Clear();
            //Console.WriteLine(); //Cleared screen
            //Console.WriteLine($"Category Child: [{{0}}]", string.Join(", ", catChild));
            //Console.Write($"Age average is '{age.Average()}' of total students are '{age.Length}', and the list values are [{{0}}]", string.Join(", ", age));
            PrintStats("", age);
            PrintStats("Child", catChild);
            PrintStats("Teenager", catTeenager);
            PrintStats("Young Adult", catYoungAdult);
            PrintStats("Adult", catAdult);
            PrintStats("Older Citizen", catOlderCitizen);
            //Console.WriteLine($"The mean Age is {age} years and {meanAgeDays} days");
        }
    } // DayThree()
    static void DayTwo()
    {
        // Task1();
        // Task2();
        // Task3();
        // Task4();
        // Task5();
        // Task6();
        // Task7();
        // Task8();
        // Task81();
        // Task9();
        Task10();
        static void Task1()
        {
            Console.WriteLine($"Max of a 16bit sined integer:\nMax value is {Int16.MaxValue}\nMin value is {Int16.MinValue}");
            // ASCHII Table - https://upload.wikimedia.org/wikipedia/commons/d/dd/ASCII-Table.svg
            // Extended ASCHII Table - https://sqljunkieshare.files.wordpress.com/2012/01/extended-ascii-table.jpg
            // 2^(32) = 4,284B 16bits = 2 bytes to store one character
            Console.WriteLine($"Int32=2^(32) = 4,284B. Precise number is {Math.Pow(2, 32)}");
            Console.WriteLine($"{(long)Math.Pow(2, 128)}");
            Console.Clear();
        }
        static void Task2()
        {
            //Task 2 the program asks to enter the number of students in the room
            int students = ReAskNumber("Please the number of students");
            Console.WriteLine($"There are {students} students attending today.");
        }
        static void Task3()     //Task 3 Comparison
        {
            int students = ReAskNumber("Please the number of students");
            Console.WriteLine($"We compare with 10 and it's {(students == 10 ? "myTrue" : "myFalse")}"); // upgraded it to "ternary conditional operator"
        }
        static void Task4()     //Task 4 variable boolian 
        {
            int students = ReAskNumber("Please the number of students");
            bool fullAttencance = (students == 10);
            Console.WriteLine($"Is the class in the Full Attendance? {fullAttencance}");
        }
        static void Task5() //Task 5 constant into the program
        {
            int students = ReAskNumber("Please the number of students");
            bool fullAttencance5 = (students == CLASSSIZE);
            Console.WriteLine($"Is the class in the Full Attendance? {fullAttencance5}");
        }
        static void Task6() //Task 6 IF statement
        {
            int students = ReAskNumber("Please the number of students");
            bool fullAttencance5 = (students == CLASSSIZE);
            if (fullAttencance5)
                Console.WriteLine(":)");
            else
                Console.WriteLine(":(");
        }
        static void Task7() //Task 7 IF not equal
        {
            int students = ReAskNumber("Please the number of students");
            bool fullAttencance5 = (students == CLASSSIZE);
            if (!fullAttencance5)
                Console.WriteLine(":(");
            else
                Console.WriteLine(":)");
            Console.Clear();
        }
        static void Task8() //Task 8 IF student passed the test
        {
            Console.WriteLine(@"7. IF student passed the test, Pass Mark is 50%");
            int passMark = 50;
            int enteredMark = ReAskNumber("Please enter the marks achieved");
            bool resultMakr = enteredMark >= passMark;
            Console.WriteLine($"Student scored {enteredMark} and {(resultMakr ? "Passed the test" : "was Unseccessful in the test")}");
            Console.Clear();
        }
        static void Task81() //Task 81 IF 
        {
            Console.WriteLine("8. If student passed the test and give them a grade");
            const int passMarkDistinction8 = 80;
            const int passMarkMerit8 = 65;
            const int passMark8 = 50;
            int enteredMark8;
            do
            {
                enteredMark8 = ReAskNumber("Please enter the marks achieved between 0 and 100");
            }
            while (0 <= enteredMark8 && enteredMark8 <= 100);

            string greadAchieved8;
            if (enteredMark8 >= passMarkDistinction8)
                greadAchieved8 = "Passed the test with Distinction greade";
            else if (enteredMark8 >= passMarkMerit8 && enteredMark8 < passMark8)
                greadAchieved8 = "Passed the test with Merit greade";
            else if (enteredMark8 >= passMark8)
                greadAchieved8 = "Passed the test with Mark greade";
            else
                greadAchieved8 = "and was Unsuccessful";
            Console.WriteLine($"Student scored {enteredMark8} and {greadAchieved8}");

            Console.Clear();
        }
        static void Task9()     // Task 9 repeat question for the marks
        {
            //write a task that should repeat until user press N
        }
        static void Task10()    //Task 10 for i=CLASSSIZE
        {
            Console.WriteLine("10. Task E and then all output");
            const int passMarkDistinction10 = 80;
            const int passMarkMerit10 = 65;
            const int passMarkPass10 = 50;
            int enteredMark;

            List<int> studentGrades = new List<int>();
            List<string> studentResults = new List<string>();
            int studentCount = ReAskNumber("Please enter the class size");

            int countsMarkDistinction10 = 0;
            int countsMarkMerit10 = 0;
            int countsMarkPass10 = 0;
            int countsUnsuccessfuls10 = 0;

            for (int i = 0; i < studentCount; i++)
            {
                do
                {
                    enteredMark = ReAskNumber($"Please enter the grade for student {i + 1} between 0 and 100: ");
                }
                while (enteredMark < 0 || enteredMark > 100);

                studentGrades.Add(enteredMark);

                if (enteredMark >= passMarkDistinction10)
                {
                    studentResults.Add($"Student {i + 1} scored {enteredMark}% and passed the test with Distinction grade.");
                    countsMarkDistinction10++;
                }
                else if (enteredMark >= passMarkMerit10)
                {
                    studentResults.Add($"Student {i + 1} scored {enteredMark}% and passed the test with Merit grade.");
                    countsMarkMerit10++;
                }
                else if (enteredMark >= passMarkPass10)
                {
                    studentResults.Add($"Student {i + 1} scored {enteredMark}% and passed the test with Pass grade.");
                    countsMarkPass10++;
                }
                else
                {
                    studentResults.Add($"Student {i + 1} scored {enteredMark}% and was unsuccessful.");
                    countsUnsuccessfuls10++;
                }
            }
            Console.Clear();

            var modeOutput10 = new int[] { countsMarkDistinction10, countsMarkMerit10, countsMarkPass10, countsUnsuccessfuls10 };
            int maxModeOutput10 = modeOutput10.Max();

            // Output the results
            foreach (string result in studentResults)
            {
                Console.WriteLine(result);
            }
            double averageGrade = studentGrades.Average();
            int highestGrade = studentGrades.Max();
            Console.WriteLine($"\nAverage grade of the students is {averageGrade}, Highest mark is {highestGrade} for student {studentGrades.IndexOf(studentGrades.Max()) + 1}");
            Console.WriteLine($"The average mode grades are {modeOutput10}");
        }
    } // DayTwo()
    static void DayOne()
    {
        Console.WriteLine("1. Hello, World!\nGoodbye, World! same lane with \\n");
        Console.WriteLine("2. Goodbye, World! at new lane");
        Console.Write("3. We use Write with \\n\n");
        Console.Write("4. We Write another lane");
        Console.WriteLine("5. Hello, World!\rGoodbye, World!");
        Console.Write("6. Hello, World!\nGoodbye, World! same\n\rGoodbye, World! same");
        Console.WriteLine("7. Tab character\tcould be done by \\t");
        Console.WriteLine("8. File is C:\test.txt\t but to litteral using \\\\t ");
        Console.WriteLine(@"9 Use @ before string """" as litteral path in the string"); //we use @ before string to treat the string symbols literally
        /*
         comment some code
         * some comment
        */
        string myMessage = "10. Use of string"; //https://www.google.com/search?q=hungarian+notation+C%23
        Console.WriteLine(myMessage);
        string endMessage = "10. Our end-message varriable";
        Console.WriteLine(endMessage);

        string myMessage11 = "11. Use of Read method";
        Console.WriteLine(myMessage11);
        Console.ReadLine();
        string endMessage11 = "11. Our end-message varriable of Read Method";
        Console.WriteLine(endMessage11);

        Console.WriteLine("12. Testing ReadLine method and wait only for Enter key");
        string myMessage12 = "Press any Enter to continue...";
        Console.WriteLine(myMessage12);
        Console.ReadLine();
        Console.WriteLine("12. Thank you and good buy for now");

        Console.WriteLine("13. Testing ReadKey method and replicating any key wait");
        string myMessage13 = "Press any key to continue...";
        Console.WriteLine(myMessage13);
        Console.ReadKey();
        Console.WriteLine("\r13. Thank you and good buy for now");

        Console.WriteLine("14. ReadKey method with Clear method");
        string myMessage14 = "Press any key to continue...";
        Console.WriteLine(myMessage14);
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("\r14. Thank you and good buy for now");
        Console.Clear();

        Console.WriteLine("15. Reading User Name and welcoming him");
        string myMessage15 = "Please enter your name and press Enter > ";
        Console.Write(myMessage15);
        string? readName15 = Console.ReadLine();
        Console.Clear();
        Console.WriteLine("Hello " + readName15);
        // with a proper formatting
        Console.WriteLine($"Hello {readName15} and welcome");
        Console.WriteLine($@"Hello\ {readName15} and C:\welcome");
        Console.Clear();

        Console.WriteLine("16. User inputs two numbers and the output is the sum of them.");
        int readFirstNumber16 = 0;
        bool firstNumberSuccess16 = false;
        while (!firstNumberSuccess16)
        { // Ask user again to input correct number if error appears
            Console.Write("Please enter the first number and press Enter > ");
            try
            {
                readFirstNumber16 = Convert.ToInt32(Console.ReadLine());
                firstNumberSuccess16 = true; // Exit the loop if the conversion was successful.
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}. Please enter a valid number.");
            }
        }
        int readSecondNumber16 = 0;
        bool secondNumberSuccess16 = false;
        while (!secondNumberSuccess16)
        {
            Console.Write("Please enter the second number and press Enter > ");
            try
            {
                readSecondNumber16 = Convert.ToInt32(Console.ReadLine());
                secondNumberSuccess16 = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}. Please enter a valid number.");
            }
        }
        int numberAnswers16 = readFirstNumber16 + readSecondNumber16;
        Console.WriteLine($"The sum of {readFirstNumber16} and {readSecondNumber16} is {numberAnswers16}.");
        Console.Clear();

        Console.WriteLine("17. User inputs two numbers and the output is sum, subtract and multiply.\n" +
            "Adjusted it to use as a function that read until correct value will be entered.");
        int readFirstNumber17 = ReAskNumber("Please enter the first number and press Enter");
        int readSecondNumber17 = ReAskNumber("Please enter the second number and press Enter");
        Console.WriteLine($"The sum of {readFirstNumber17} and {readSecondNumber17} is {readFirstNumber17 + readSecondNumber17}.");
        Console.WriteLine($"The subtract is {readFirstNumber17 - readSecondNumber17}.");
        Console.WriteLine($"The product is {readFirstNumber17 * readSecondNumber17}.");
        Console.WriteLine($"The difference is {Math.Abs(readFirstNumber17 - readSecondNumber17)}.");

        Console.WriteLine("18. Divistion answer requires to use the convertation to float, double and decimal numbers");
        int divisionAnswerInt = readFirstNumber17 / readSecondNumber17; //intitial type doesn't metter, at least one of them should be in init 
        Console.WriteLine($"The division is {divisionAnswerInt.ToString()}."); // output still is not float
        float divisionAnswerFloat = (float)readFirstNumber17 / readSecondNumber17;
        divisionAnswerFloat = readFirstNumber17 / (float)readSecondNumber17;
        double divisionAnswerDouble = (double)readFirstNumber17 / (double)readSecondNumber17; //convert at least one number to desitable format
        // int divisionAnswer3 = readFirstNumber17 / (float)readSecondNumber17;
        decimal divisionAnswerDecimal = readFirstNumber17 / (decimal)readSecondNumber17;
        Console.WriteLine($"The division for different types are:\nDouble: {divisionAnswerDouble}.\nDecimal: {divisionAnswerDecimal}.\nFloat: {divisionAnswerFloat}.");
    } // DayOne()
    static int  ReAskNumber(string messageToUser)
    {
        int number = 0;
        bool numberSuccess = false;
        while (!numberSuccess)
        {
            Console.Write($"{messageToUser} > ");
            try
            {
                stopwatch.Stop();
                number = Convert.ToInt32(Console.ReadLine());
                stopwatch.Start();
                numberSuccess = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}. Please enter a valid number.");
            }
        }
        return number;
    } // ReAskNumber()
    static void Print(string text, bool isNewLine = true)
    {
        if (isNewLine)
            Console.WriteLine(text);
        else
            Console.Write(text);
    }// Print()
    static void PrintStats(string category, params int[] myArray)
    {
        if (myArray.Length > 0 && myArray.Sum() != 0)
        {
            if (category != "") Console.WriteLine($"Output for category {category}:"); // (category != $"for {category}" ? "" : "")
            /* Console.WriteLine($"Total People {myArray.Length}, Oldest Person: {myArray.Max()}, Youngest Person: {myArray.Min()}");
             Console.WriteLine($"Oldest Person's age is {myArray.Max()} for person {myArray.ToList().IndexOf(myArray.Max())}");
             Console.WriteLine($"Youngest Person's age is {myArray.Min()} for person {myArray.ToList().IndexOf(myArray.Min())}");
             Console.WriteLine($"The mean age is '{myArray.Average()}'");*/
            // My method
            double meanAge = myArray.Average();
            int meanAgeYears = (int)meanAge;
            int meanAgeDays = Convert.ToInt16((meanAge - meanAgeYears) * 365);

            Console.WriteLine($"The mean Age is {meanAgeYears} years and {meanAgeDays} days");
            // Modulus operator
            double meanAgeDaysTotal = myArray.Average() * 365;
            int meanAgeYearsM = (int)(meanAgeDaysTotal / 365);
            int meanAgeDaysM = (int)(meanAgeDaysTotal % 365);

            Console.WriteLine($"The Modeular mean of Age is {meanAgeYearsM} years and {meanAgeDaysM} days");
            Console.WriteLine();
        }
    } // PrintStats()
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