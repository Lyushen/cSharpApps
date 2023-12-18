internal class Day06
{
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
            static bool WriteMyFile(string path, string text, bool add = true)
            {
                string? directoryPath = Path.GetDirectoryName(path);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    Console.WriteLine($"Directory doesnt' exist, we created {directoryPath}");
                }
                try
                {
                    using (StreamWriter fileWriter = new StreamWriter(path, add))
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
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
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


            //fileWriter.Close(); //unnessesarry but could be understandable

        } // MyStreamWriter()
        static void CreateTextFile()
        {
            int lines = 10;
            string helloText = "Hello World!";
            string fileText = "";

            for (int i = 1; i <= lines; i++)
            {
                fileText += "Line #" + i + ": " + helloText + "\n";
            }
            Console.WriteLine(fileText);
            File.WriteAllText("helloworld10.txt", fileText);
        } // CreateTextFile()
    } // DaySix()
}