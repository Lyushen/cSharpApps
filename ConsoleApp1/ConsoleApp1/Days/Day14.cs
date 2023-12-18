internal class Day14
{
    public static void Day14()
    {

        /*TASK: Calculate and display

        The pharmacy with the highest sales in each county
        The average per county
        The total sales per county*/
        OnlyArrays();
        static void OnlyArrays()
        {
            string directoryPath = @"..\..\..\InputAndOutputFiles\";
            string fileName = "pharmacy";
            string filePath = directoryPath + fileName + ".CSV";

            string[] lines = new string[] { };
            try
            {
                lines = File.ReadAllLines(filePath).Skip(1).Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            if (lines.Length < 2)
            {
                Console.WriteLine("The file doesn't contain enough data to proceed");
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
                output += Calculate(county, pharmacies, counties, totalSales);
            }
            Console.WriteLine(output);
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
            output += $"County: {county}\n";
            output += $"Highest Sales Pharmacy: {highestSalesPharmacy}\n";
            output += $"Total Sales: {totalCountySales:F0}\n";
            output += $"Average Sales: {averageSales:F2}\n";
            return output;
        } //DisplayStatisticsForCounty()

        //ShortestNotComplex();
        static void ShortestNotComplex()
        {
            string directoryPath = @"..\..\..\InputAndOutputFiles\";
            string fileName = "pharmacy";
            string filePath = directoryPath + fileName + ".CSV";

            string[] lines = new string[] { };
            try
            {
                lines = File.ReadAllLines(filePath).Skip(1).Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            if (lines.Length < 2)
            {
                Console.WriteLine("The file doesn't contain enough data to proceed");
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
        static void ShortestComplexWay()
        {
            string directoryPath = @"..\..\..\InputAndOutputFiles\";
            string fileName = "pharmacy";
            string filePath = directoryPath + fileName + ".CSV";
            string[] lines = File.ReadAllLines(filePath).Skip(1).Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();

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

        static void MyWay()
        {


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
}