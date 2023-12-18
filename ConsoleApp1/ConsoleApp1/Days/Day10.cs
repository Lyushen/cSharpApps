internal class Day10
{
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
}