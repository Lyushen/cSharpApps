using System.Text.RegularExpressions;

internal class Day12
{
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



        static Dictionary<string, double> ExpensesCat()
        {
            Dictionary<string, double> expenses = new Dictionary<string, double>();
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
        static int ReAskIntRange(string message, int min = 1, int max = 10_000_000)
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
        static void Print(string text, bool isNewLine = true)
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
}