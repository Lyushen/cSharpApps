internal class Day05
{
    static void DayFive()
    { // Improvements to day four code
        /* if the function returns true or false, it should start with isPolledPeople?
        
        //categoryID = personAge switch
        //{
        //    < 13 => CategoryID.Child,
        //    >= 13 and <= 19 => CategoryID.Teenager,
        //    > 19 and <= 25 => CategoryID.YoungAdult,
        //    > 25 and < 65 => CategoryID.Adult,
        //    _ => CategoryID.Older
        //};

        Find our company's Code Convention.
        Pick a style and stick to it
        Do not be ambiguous with naming

        Overloading is when a method can be called with different arguments and scenarios.

        string myName = "Tom";
        string firstLetter = myName[0]; ?? char explanation

        int 1someVar = 0; // invalid
        int _1 = 0; // valid
        int them-all = 0; // invalid
        int ______ = 0; // valid
        int _size = 0; // valid

        char some = 'a'; == string some = 'a';

        float pi = 3.14f;
        Console.WriteLine(pi);

        */
        //DayFiveTeacherApproach2();
        //RollDiceApproach1(); // Task RollDice
        //RollDiceApproach2(); // Task RollDice
        //RollDiceValidation();
        RollDiceValidationThreads();

        static void RollDiceValidationThreads()
        {
            // Define the number of iterations and an array to hold counts for each possible value.
            long totalIterations = 100000000; // 100m is safe can be finished for 8.4s, 1b takes 82,5 seconds
            int[] counts = new int[6];

            // Run the loop in parallel.
            Parallel.For(0, totalIterations, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, _ =>
            {
                Random rnd = new Random(); // Local Random instance to avoid thread contention.
                int index = rnd.Next(1, 7); // Get a random value between 1 and 6(5).
                Interlocked.Increment(ref counts[index - 1]); // Safely increment the count for this index.
            });

            // Calculate percentages.
            double[] percentages = new double[6];
            for (int i = 0; i < counts.Length; i++)
            {
                percentages[i] = Math.Round((counts[i] / (double)totalIterations) * 100, 2);
            }
            for (int i = 0; i < percentages.Length; i++)
            {
                Console.WriteLine($"The probability chance for number {i + 1} is {percentages[i]}%");
            }
        }
        static void RollDiceValidation()
        {
            int[] counter = new int[6];
            int testRuns = 10000000;
            double persentRuns = 100 / testRuns; // added to test

            for (int i = 0; i < testRuns; i++)
            {
                switch (RollDice())
                {
                    case 1: counter[0]++; break;
                    case 2: counter[1]++; break;
                    case 3: counter[2]++; break;
                    case 4: counter[3]++; break;
                    case 5: counter[4]++; break;
                    case 6: counter[5]++; break;
                }
            }
            Console.WriteLine($"Through {testRuns} runs:");
            for (int i = 0; i < counter.Length; i++)
            {
                Console.WriteLine($"The probability chance for number {i + 1} is {(double)counter[i] / testRuns * 100:0.00}%");
                //Console.WriteLine($"The probability chance for number {i + 1} is {(double)counter[i] * persentRuns}%");
            }
            static int RollDice()
            {
                Random randomNumber = new Random();
                return randomNumber.Next(1, 6);
            }
        }
        static void RollDiceApproach2()
        {

            int counter = 0;
            while (counter < 10)
            {
                counter++;
                Console.WriteLine($"Roll # {counter} => Dice value: {getDiceValue()}");
            }
            static int getDiceValue()
            {
                Random randomNumber = new Random();
                switch (randomNumber.NextDouble())
                {
                    case < 0.16666: return 1;
                    case <= 0.33333: return 2;
                    case <= 0.50000: return 3;
                    case <= 0.66666: return 4;
                    case <= 0.83333: return 5;
                    default: return 6;
                }
            }
        }
        static void RollDiceApproach1()
        {
            bool isRollDice = true;
            Random randomNumber = new Random();
            do
            {
                Console.Write("Do you wish to roll the dice? (Y/N) > ");
                isRollDice = Console.ReadLine().ToLower() == "y";
                Console.WriteLine($"Your dice is {randomNumber.Next(1, 6)}");
            }
            while (isRollDice);
        }
        static void DayFiveTeacherApproach2()
        {
            int[] numberPolled = new int[6];
            int[] totalAges = new int[6];
            int[] youngestAges = new int[6];
            int[] oldestAges = new int[6];
            string[] Categories = new string[] { "General", "Child", "Teenager", "Young Adult", "Adult", "Older Citizen" };
            while (PersonPoll())
            {
                Console.Write($"What is the age of person >");
                int personAge = Convert.ToInt32(Console.ReadLine());
                UpdateCategoryValues(CategoryID.General, personAge, numberPolled, totalAges, youngestAges, oldestAges);
                // Find the spceific category based on the age entered and update values
                UpdateCategoryValues(AgeCategory(personAge), personAge, numberPolled, totalAges, youngestAges, oldestAges);
            }

            static CategoryID AgeCategory(int age)
            {
                switch (age)
                {
                    case < 13: return CategoryID.Child;
                    case <= 19: return CategoryID.Teenager;
                    case <= 25: return CategoryID.YoungAdult;
                    case < 65: return CategoryID.Adult;
                    default: return CategoryID.Older;
                }
            } // AgeCategory();

            DisplayResults(Categories, totalAges, numberPolled, youngestAges, oldestAges);

            static void DisplayResults(string[] category, int[] totalAge, int[] pollCount, int[] youngestAge, int[] oldestAge)
            {
                for (int i = 0; i < pollCount.Length; i++)
                {
                    Console.WriteLine($"========== {category[i]} ==============");
                    if (pollCount[i] > 0)
                    {
                        Console.WriteLine($"Number polled is {pollCount[i]}");
                        Console.WriteLine($"The youngest person is aged {youngestAge[i]}");
                        Console.WriteLine($"The oldest person is aged {oldestAge[i]}");
                        float meanAge = (float)totalAge[i] / pollCount[i];
                        Console.WriteLine($"The mean age is {meanAge}");
                    }
                    Console.WriteLine("===============================}");
                }
            } //DisplayResults
            static bool PersonPoll()
            {
                Console.Write("have you someone to poll? (Y/N) >");
                return Console.ReadLine().ToUpper() == "Y";

            } //PersonPoll
            static void UpdateCategoryValues(CategoryID categoryID, int ageEntered, int[] numberPolled, int[] totalAges, int[] youngestAges, int[] oldestAges)
            {

                numberPolled[(int)categoryID]++;
                totalAges[(int)(categoryID)] += ageEntered;

                if (numberPolled[(int)categoryID] == 1)
                {
                    youngestAges[(int)categoryID] = ageEntered;
                }
                if (ageEntered > oldestAges[(int)categoryID])
                {
                    oldestAges[(int)(categoryID)] = ageEntered;
                }
                if ((ageEntered < youngestAges[(int)categoryID]))
                {
                    youngestAges[(int)categoryID] = ageEntered;
                }
            } // UpdateCategoryValues()
        } // DayThreeTeacherApproach2()
    }// DayFive()
}