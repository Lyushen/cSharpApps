using System.Collections.Generic;
using System.Linq;

internal class Program
{
    const int CLASSSIZE = 10;
    private static void Main(string[] args)
    {
        // DayOne();
        DayTwo();
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
        Environment.Exit(0);
    }
    static void DayTwo()
    {
        //Task 1
        Console.WriteLine($"Max of a 16bit sined integer:\nMax value is {Int16.MaxValue}\nMin value is {Int16.MinValue}");
        // ASCHII Table - https://upload.wikimedia.org/wikipedia/commons/d/dd/ASCII-Table.svg
        // Extended ASCHII Table - https://sqljunkieshare.files.wordpress.com/2012/01/extended-ascii-table.jpg
        // 2^(32) = 4,284B
        Console.WriteLine($"Int32=2^(32) = 4,284B. Precise number is {Math.Pow(2, 32)}");
        Console.WriteLine($"{(long)Math.Pow(2, 128)}");
        Console.Clear();

        //Task 2 the program asks to enter the number of students in the room
        int students = ReAskUser("Please the number of students");
        Console.WriteLine($"There are {students} students attending today.");

        //Task 3 Comparison
        Console.WriteLine($"We compare with 10 and it's {(students == 10 ? "myTrue" : "myFalse")}"); // upgraded it to "ternary conditional operator"
        //Task 4 variable boolian 
        bool fullAttencance = (students==10);
        Console.WriteLine($"Is the class in the Full Attendance? {fullAttencance}");
        //Task 5 constant into the program
        bool fullAttencance5 = (students == CLASSSIZE);
        Console.WriteLine($"Is the class in the Full Attendance? {fullAttencance5}"); 
        //Task 6 IF statement
        if (fullAttencance5) 
            Console.WriteLine(":)");
        else
            Console.WriteLine(":(");
        //Task 7 IF not equal
        if (!fullAttencance5)
            Console.WriteLine(":(");
        else
            Console.WriteLine(":)");
        Console.Clear();

        //Task 7 IF student passed the test
        Console.WriteLine(@"7. IF student passed the test, Pass Mark is 50%");
        int passMark = 50;
        int enteredMark = ReAskUser("Please enter the marks achieved");
        bool resultMakr = enteredMark >= passMark;
        Console.WriteLine($"Student scored {enteredMark} and {(resultMakr ? "Passed the test" : "was Unseccessful in the test")}");
        Console.Clear();

        //Task 8 IF 
        Console.WriteLine("8. If student passed the test and give them a grade");
        const int passMarkDistinction8 = 80;
        const int passMarkMerit8 = 65;
        const int passMark8 = 50;
        int enteredMark8;
        /* do
         {
             enteredMark8 = ReAskUser("Please enter the marks achieved between 0 and 100");
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
        */
        Console.Clear();
        //Task 9 repeat question for the marks
        //write a task that should repeat until user press N

        //Task 10 for i=CLASSSIZE
        Console.WriteLine("10. Task E and then all output");

        const int passMarkDistinction10 = 80;
        const int passMarkMerit10 = 65;
        const int passMarkPass10 = 50;

        List<int> studentGrades = new List<int>();
        List<string> studentResults = new List<string>();
        int studentCount = ReAskUser("Please enter the class size");

        int countsMarkDistinction10 = 0;
        int countsMarkMerit10 = 0;
        int countsMarkPass10 = 0;
        int countsUnsuccessfuls10 = 0;

        for (int i = 0; i < studentCount; i++)
        {
            do
            {
                enteredMark = ReAskUser($"Please enter the grade for student {i + 1} between 0 and 100: ");
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
        Console.WriteLine($"\nAverage grade of the students is {averageGrade}, Highest mark is {highestGrade} for student {studentGrades.IndexOf(studentGrades.Max())+1}");
        Console.WriteLine($"The average mode grades are {modeOutput10}");
    }
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
        int readFirstNumber17 = ReAskUser("Please enter the first number and press Enter");
        int readSecondNumber17 = ReAskUser("Please enter the second number and press Enter");
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
    }

    static int ReAskUser(string messageToUser, int? range=100)
    {
        int number = 0;
        bool numberSuccess = false;
        while (!numberSuccess)
        {
            Console.Write($"{messageToUser} > ");
            try
            {
                number = Convert.ToInt32(Console.ReadLine());
                numberSuccess = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}. Please enter a valid number.");
            }
        }
        return number;
    }
    static int ReAskUserTwo(string message)
    {
        int result;
        Console.WriteLine(message);
        while (!int.TryParse(Console.ReadLine(), out result))
        {
            Console.WriteLine("Invalid input. " + message);
        }
        return result;
    }
}
