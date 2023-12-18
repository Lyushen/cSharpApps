internal class Day08
{
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
                if (dataLines.Length == 0)
                {
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

            static void DisplayAgeInfo(string[] firstName, string[] lastName, int[] apprentAge)
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
            bool isHeader = true;

            List<int> ages = new List<int>();
            List<string> names = new List<string>();

            for (int i = 1; i < totalLines; i++)
            {
                string[] dataFields = importedCSV[i].Split(",");
                ages.Add(int.Parse(dataFields[2]));
            }
        } // comeBackToDaySeven()
    } // DayEight()
}