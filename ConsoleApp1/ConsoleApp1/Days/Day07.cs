internal class Day07
{
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
            if (isHeader)
            {
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

            for (int i = 0; i < totalLines; i++)
            {
                Console.WriteLine($"Student #{i + 1} '{studentNames[i]}' aged '{ages[i]}'");
            }

            //double averageAge = Math.Round((double)totalAge / linecount ,2);
            //string averageAge = String.Format("{0:F2}", (double)totalAge / linecount); // wromg return

            //Console.WriteLine($"Total age: '{totalAge}', Mean age: {averageAge}");

            Console.WriteLine($"There are {ages.Length} students");
            Console.WriteLine($"The youngest student is {ages.Min()}");
            Console.WriteLine($"The oldest student is {ages.Max()}");
            Console.WriteLine($"The average age of all students {ages.Average():F2}");
            Console.WriteLine($"The average age of all students {Math.Round(ages.Average(), 2)}");


        } // ReadViaFileMethod()
        static void ReadViaStreamReaderMethod()
        {
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
}