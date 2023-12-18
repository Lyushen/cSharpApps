internal class Day02
{
    static void DayTwo()
    {
        // Task1();
        // Task2();
        // Task3();
        // Task4();
        // Task5();
        // Task6();
        // Task7();
        // Task8();
        // Task81();
        // Task9();
        Task10();
        static void Task1()
        {
            Console.WriteLine($"Max of a 16bit sined integer:\nMax value is {Int16.MaxValue}\nMin value is {Int16.MinValue}");
            // ASCHII Table - https://upload.wikimedia.org/wikipedia/commons/d/dd/ASCII-Table.svg
            // Extended ASCHII Table - https://sqljunkieshare.files.wordpress.com/2012/01/extended-ascii-table.jpg
            // 2^(32) = 4,284B 16bits = 2 bytes to store one character
            Console.WriteLine($"Int32=2^(32) = 4,284B. Precise number is {Math.Pow(2, 32)}");
            Console.WriteLine($"{(long)Math.Pow(2, 128)}");
            Console.Clear();
        }
        static void Task2()
        {
            //Task 2 the program asks to enter the number of students in the room
            int students = ReAskNumber("Please the number of students");
            Console.WriteLine($"There are {students} students attending today.");
        }
        static void Task3()     //Task 3 Comparison
        {
            int students = ReAskNumber("Please the number of students");
            Console.WriteLine($"We compare with 10 and it's {(students == 10 ? "myTrue" : "myFalse")}"); // upgraded it to "ternary conditional operator"
        }
        static void Task4()     //Task 4 variable boolian 
        {
            int students = ReAskNumber("Please the number of students");
            bool fullAttencance = (students == 10);
            Console.WriteLine($"Is the class in the Full Attendance? {fullAttencance}");
        }
        static void Task5() //Task 5 constant into the program
        {
            int students = ReAskNumber("Please the number of students");
            bool fullAttencance5 = (students == CLASSSIZE);
            Console.WriteLine($"Is the class in the Full Attendance? {fullAttencance5}");
        }
        static void Task6() //Task 6 IF statement
        {
            int students = ReAskNumber("Please the number of students");
            bool fullAttencance5 = (students == CLASSSIZE);
            if (fullAttencance5)
                Console.WriteLine(":)");
            else
                Console.WriteLine(":(");
        }
        static void Task7() //Task 7 IF not equal
        {
            int students = ReAskNumber("Please the number of students");
            bool fullAttencance5 = (students == CLASSSIZE);
            if (!fullAttencance5)
                Console.WriteLine(":(");
            else
                Console.WriteLine(":)");
            Console.Clear();
        }
        static void Task8() //Task 8 IF student passed the test
        {
            Console.WriteLine(@"7. IF student passed the test, Pass Mark is 50%");
            int passMark = 50;
            int enteredMark = ReAskNumber("Please enter the marks achieved");
            bool resultMakr = enteredMark >= passMark;
            Console.WriteLine($"Student scored {enteredMark} and {(resultMakr ? "Passed the test" : "was Unseccessful in the test")}");
            Console.Clear();
        }
        static void Task81() //Task 81 IF 
        {
            Console.WriteLine("8. If student passed the test and give them a grade");
            const int passMarkDistinction8 = 80;
            const int passMarkMerit8 = 65;
            const int passMark8 = 50;
            int enteredMark8;
            do
            {
                enteredMark8 = ReAskNumber("Please enter the marks achieved between 0 and 100");
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

            Console.Clear();
        }
        static void Task9()     // Task 9 repeat question for the marks
        {
            //write a task that should repeat until user press N
        }
        static void Task10()    //Task 10 for i=CLASSSIZE
        {
            Console.WriteLine("10. Task E and then all output");
            const int passMarkDistinction10 = 80;
            const int passMarkMerit10 = 65;
            const int passMarkPass10 = 50;
            int enteredMark;

            List<int> studentGrades = new List<int>();
            List<string> studentResults = new List<string>();
            int studentCount = ReAskNumber("Please enter the class size");

            int countsMarkDistinction10 = 0;
            int countsMarkMerit10 = 0;
            int countsMarkPass10 = 0;
            int countsUnsuccessfuls10 = 0;

            for (int i = 0; i < studentCount; i++)
            {
                do
                {
                    enteredMark = ReAskNumber($"Please enter the grade for student {i + 1} between 0 and 100: ");
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
            Console.WriteLine($"\nAverage grade of the students is {averageGrade}, Highest mark is {highestGrade} for student {studentGrades.IndexOf(studentGrades.Max()) + 1}");
            Console.WriteLine($"The average mode grades are {modeOutput10}");
        }
    } // DayTwo()
}