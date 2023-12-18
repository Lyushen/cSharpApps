internal class Day09
{
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
            textToFile += "Name\tDivision\tWeek 1\tWeek 2\tWeek 3\tWeek 4\tWeek 5\n";
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
}