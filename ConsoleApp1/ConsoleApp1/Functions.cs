internal class Functions
{
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

    static int ReAskNumber(string messageToUser)
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
}