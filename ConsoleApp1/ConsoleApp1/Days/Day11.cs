internal class Day11
{
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
                    outputString = $"\t{new string('=', country.Length)}\n\t{country.ToUpper()}\n\t{new string('=', country.Length)}\nTotal games: {total}\nTop player: '{topScorer}' scored {topScore} goals\nAverage goals: {average}\n" +
                        $"Average Per January: {averageJan}\nAverage Per February: {averageFeb}\nAverage Per March: {averageMar}\nAverage Per April: {averageApr}\nAverage Per May: {averageMay}\n";
                    return outputString;
                }

                static string[] ReadMyFile(string csvFilePath)
                {
                    string[] dataLines;
                    try
                    {
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
}