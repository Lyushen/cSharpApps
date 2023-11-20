using System.Collections.Generic;
using System.Diagnostics;
using static Program;

internal class Program
{
    public enum CategoryID : int //enum approach
    {
        General,
        Child,
        Teenager,
        YoungAdult,
        Adult,
        Older
    }
    const int IEGeneral = (int)CategoryID.General;
    const int IEChild = (int)CategoryID.Child;
    const int IETeenager = (int)CategoryID.Teenager;
    const int IEYoungAdult = (int)CategoryID.YoungAdult;
    const int IEAdult = (int)CategoryID.Adult;
    const int IEOlder = (int)CategoryID.Older;
    // my const approach    
    public const int IGeneral = 0;
    public const int IChild = 1;
    public const int ITeenager = 2;
    public const int IYoungAdult = 3;
    public const int IAdult = 4;
    public const int IOlder = 5;

    int[] catGeneral = new int[6];
    int[] catChild = new int[6];
    int[] catTeenager = new int[6];
    int[] catYoungAdult = new int[6];
    int[] catAdult = new int[6];
    int[] catOlder = new int[6];
    string[] catName = new string[] { "General", "Child", "Teenager", "YoungAdult", "Adult", "Older" };

    const int CLASSSIZE = 10;
    static Stopwatch stopwatch = Stopwatch.StartNew(); //Diagnostic measures. Start measuring time
    private static void Main(string[] args)
    {
        stopwatch.Start();
        // Program.DayOne();
        // Program.DayTwo();
        // Program.DayThree();
        // Program.DayFour();
        Program.DayFive();

        stopwatch.Stop();
        Console.WriteLine($"\nPress any key to exit...\tProcessing time: {stopwatch.ElapsedMilliseconds} ms");
        Console.ReadKey();
        Environment.Exit(0);
    }
    static void DayFive() // Improvements to day four code
    {
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
        RollDiceValidation();

        static void RollDiceValidation()
        {
            int[] counter = new int[6];
            int testRuns = 10000000;
            double persentRuns = 100 / testRuns; // added to test

            for (int i = 0; i < testRuns; i++)
            {
                switch (RollDice())
                {
                    case 1:counter[0]++;break;
                    case 2:counter[1]++;break;
                    case 3:counter[2]++;break;
                    case 4:counter[3]++;break;
                    case 5:counter[4]++;break;
                    case 6:counter[5]++;break;
                }
            }
            Console.WriteLine($"Through {testRuns} runs:");
            for (int i = 0; i < counter.Length; i++)
            {
                //Console.WriteLine($"The probability chance for number {i + 1} is {(double)counter[i] / testRuns * 100:0.00}%");
                Console.WriteLine($"The probability chance for number {i + 1} is {(double)counter[i] * persentRuns}%");
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
                switch (age)  {
                    case < 13:  return CategoryID.Child;
                    case <= 19: return CategoryID.Teenager;
                    case <= 25: return CategoryID.YoungAdult;
                    case < 65:  return CategoryID.Adult;
                    default:    return CategoryID.Older;
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
    }//   DayFive()
    static void DayFour()
    {
        // DayFourApproach3();
        // DayFourApproach4();
        DayThreeTeacherApproach2();
        static void DayFourApproach3()
        {
            int[] age = new int[ReAskNumber("Please enter the number of people")];
            for (int i = 0; i < age.Length; i++)
            {
                age[i] = ReAskNumber($"Please enter age for person {i + 1}");
            }
            int totalPeople = age.Length;

            int[] catChild = new int[totalPeople];
            int[] catTeenager = new int[totalPeople];
            int[] catYoungAdult = new int[totalPeople];
            int[] catAdult = new int[totalPeople];
            int[] catOlderCitizen = new int[totalPeople];

            for (int i = 0; i < totalPeople; i++)
            {
                if (age[i] <= 0)
                {
                    Console.WriteLine($"Error. The age for person {i + 1} is could not be 0 or less");
                }
                else if (age[i] > 0 && age[i] <= 12) // Child Category
                {
                    catChild[i] = age[i];
                }
                else if (age[i] >= 13 && age[i] <= 19) // Teenager Category
                {
                    catTeenager[i] = age[i];
                }
                else if (age[i] >= 20 && age[i] <= 24) // Young Adult Category
                {
                    catYoungAdult[i] = age[i];
                }
                else if (age[i] >= 25 && age[i] <= 65) // Adult Category
                {
                    catAdult[i] = age[i];
                }
                else if (age[i] >= 66) // Older Citizen Category
                {
                    catOlderCitizen[i] = age[i];
                }
            }
            Console.Clear();
            PrintStats("", age);
            PrintStats("Child", catChild);
            PrintStats("Teenager", catTeenager);
            PrintStats("Young Adult", catYoungAdult);
            PrintStats("Adult", catAdult);
            PrintStats("Older Citizen", catOlderCitizen);
        }
        static void DayFourApproach4()
        {

            int totalPeople = ReAskNumber("Please enter the number of users");
            int[] age = new int[totalPeople];
            int count = 0;
            while (count <= totalPeople)
            {
                age[count] = ReAskNumber($"Please enter age for student {age.Length + 1}");
                count++;
            }
            int[] catChild = new int[totalPeople];
            int[] catTeenager = new int[totalPeople];
            int[] catYoungAdult = new int[totalPeople];
            int[] catAdult = new int[totalPeople];
            int[] catOlderCitizen = new int[totalPeople];
            count = 0;
            while (count <= totalPeople)
            {
                if (age[count] <= 0)
                {
                    Console.WriteLine($"Error. The age for person {count + 1} is could not be 0 or less");
                }
                else if (age[count] > 0 && age[count] <= 12) // Child Category
                {
                    catChild[count] = age[count];
                }
                else if (age[count] >= 13 && age[count] <= 19) // Teenager Category
                {
                    catTeenager[count] = age[count];
                }
                else if (age[count] >= 20 && age[count] <= 24) // Young Adult Category
                {
                    catYoungAdult[count] = age[count];
                }
                else if (age[count] >= 25 && age[count] <= 65) // Adult Category
                {
                    catAdult[count] = age[count];
                }
                else if (age[count] >= 66) // Older Citizen Category
                {
                    catOlderCitizen[count] = age[count];
                }
            }

        }
        static void DayFourApproach5()
        {
            Program.CategoryID CategoryID;

            bool anyOtherPeople = true;
            int ageCount = 0;
            int totalAge = 0;
            int meanAge = 0;

            int personAge = 0;
            int oldestAge = 0;
            int youngestAge = 0;

            int personAgeChild = 0;
            int oldestAgeChild = 0;
            int youngestAgeChild = 0;

            int personAgeTeenage = 0;
            int oldestAgeTeenage = 0;
            int youngestAgeTeenage = 0;

            int personAgeYoungerAdult = 0;
            int oldestAgeYoungerAdult = 0;
            int youngestAgeYoungerAdult = 0;

            int personAgeAdult = 0;
            int oldestAgeAdult = 0;
            int youngestAgeAdult = 0;

            int personAgeOlder = 0;
            int oldestAgeOlder = 0;
            int youngestAgeOlder = 0;



            while (anyOtherPeople)
            {
                personAge = ReAskNumber($"Please enter age for Person {ageCount + 1}");
                ageCount++;
                totalAge += personAge;

                if (personAge > oldestAge)
                {
                    oldestAge = personAge;
                }
                if (personAge != 0 && personAge > youngestAge)
                {
                    youngestAge = personAge;
                }
                if (anyOtherPeople && ageCount > 0)
                {
                    meanAge = totalAge / ageCount;
                    Console.WriteLine($"Person {ageCount} age is {personAge}" +
                        $"\nMean age is {meanAge}" +
                        $"\nTotal age is {totalAge}" +
                        $"\nOldest Person {oldestAge}" +
                        $"\nYoungest Person {youngestAge}");
                }
                //Exit function                 //if (Console.ReadKey().Key == ConsoleKey.N) anyOtherPeople = false;
                Console.Write("Have you someone else to enter? (Y/N) > ");
                if (Console.ReadLine().ToLower() != "y") anyOtherPeople = false;
            }

            /*[] catChild = new int[totalPeople];
            int[] catTeenager = new int[totalPeople];
            int[] catYoungAdult = new int[totalPeople];
            int[] catAdult = new int[totalPeople];
            int[] catOlderCitizen = new int[totalPeople];
            ageCount = 0;
             while(ageCount <= totalPeople)
            {
                if (age[ageCount] <= 0)
                {
                    Console.WriteLine($"Error. The age for person {ageCount + 1} is could not be 0 or less");
                }
                else if (age[ageCount] > 0 && age[ageCount] <= 12) // Child Category
                {
                    catChild[ageCount] = age[ageCount];
                }
                else if (age[ageCount] >= 13 && age[ageCount] <= 19) // Teenager Category
                {
                    catTeenager[ageCount] = age[ageCount];
                }
                else if (age[ageCount] >= 20 && age[ageCount] <= 24) // Young Adult Category
                {
                    catYoungAdult[ageCount] = age[ageCount];
                }
                else if (age[ageCount] >= 25 && age[ageCount] <= 65) // Adult Category
                {
                    catAdult[ageCount] = age[ageCount];
                }
                else if (age[ageCount] >= 66) // Older Citizen Category
                {
                    catOlderCitizen[ageCount] = age[ageCount];
                }
            }
            
            Console.Clear();
            PrintStats("", age);
            PrintStats("Child", catChild);
            PrintStats("Teenager", catTeenager);
            PrintStats("Young Adult", catYoungAdult);
            PrintStats("Adult", catAdult);
            PrintStats("Older Citizen", catOlderCitizen);*/
        }
        static void DayThreeTeacherApproach1()
        {
            int numberPeoplePolled = 0; // Number of People Polled
            int totalAge = 0;
            int oldestAge = 0;
            int youngestAge = 0;
            int numberPeoplePolledChild = 0; // Number of People Polled
            int totalAgeChild = 0;
            int oldestAgeChild = 0;
            int youngestAgeChild = 0;
            int numberPeoplePolledTeenager = 0; // Number of People Polled
            int totalAgeTeenager = 0;
            int oldestAgeTeenager = 0;
            int youngestAgeTeenager = 0;
            int numberPeoplePolledYoungAdult = 0; // Number of People Polled
            int totalAgeYoungAdult = 0;
            int oldestAgeYoungAdult = 0;
            int youngestAgeYoungAdult = 0;
            int numberPeoplePolledAdult = 0; // Number of People Polled
            int totalAgeAdult = 0;
            int oldestAgeAdult = 0;
            int youngestAgeAdult = 0;
            int numberPeoplePolledOlderCitizen = 0; // Number of People Polled
            int totalAgeOlderCitizen = 0;
            int oldestAgeOlderCitizen = 0;
            int youngestAgeOlderCitizen = 0;
            Console.Write("have you someone to poll? (Y/N) >");
            bool isPersonPolled = Console.ReadLine().ToUpper() == "Y";

            while (isPersonPolled)
            {
                numberPeoplePolled++;
                Console.Write($"What is the age of person #{numberPeoplePolled} >");
                int personAge = Convert.ToInt32(Console.ReadLine());
                totalAge += personAge;
                if (numberPeoplePolled == 1) // First person is the youngest and oldest
                {
                    oldestAge = personAge;
                    youngestAge = personAge;
                }
                else
                {
                    if (personAge > oldestAge)
                    {
                        oldestAge = personAge;
                    }
                    if (personAge < youngestAge)
                    {
                        youngestAge = personAge;
                    }
                }

                // A child
                if (personAge < 13)
                {
                    totalAgeChild += personAge;
                    numberPeoplePolledChild++;
                    if (numberPeoplePolledChild == 1) // First person is the youngest and oldest
                    {
                        oldestAgeChild = personAge;
                        youngestAgeChild = personAge;
                    }
                    else
                    {
                        if (personAge > oldestAgeChild)
                        {
                            oldestAgeChild = personAge;
                        }
                        if (personAge < youngestAgeChild)
                        {
                            youngestAgeChild = personAge;
                        }
                    }
                }
                else if (personAge <= 19) // Teenager
                {
                    totalAgeTeenager += personAge;
                    numberPeoplePolledTeenager++;
                    if (numberPeoplePolledTeenager == 1) // First person is the youngest and oldest
                    {
                        oldestAgeTeenager = personAge;
                        youngestAgeTeenager = personAge;
                    }
                    else
                    {
                        if (personAge > oldestAgeTeenager)
                        {
                            oldestAgeTeenager = personAge;
                        }
                        if (personAge < youngestAgeTeenager)
                        {
                            youngestAgeTeenager = personAge;
                        }
                    }
                }
                else if (personAge <= 25)
                {
                    totalAgeYoungAdult += personAge;
                    numberPeoplePolledYoungAdult++;
                    if (numberPeoplePolledYoungAdult == 1) // First person is the youngest and oldest
                    {
                        oldestAgeYoungAdult = personAge;
                        youngestAgeYoungAdult = personAge;
                    }
                    else
                    {
                        if (personAge > oldestAgeYoungAdult)
                        {
                            oldestAgeYoungAdult = personAge;
                        }
                        if (personAge < youngestAgeYoungAdult)
                        {
                            youngestAgeYoungAdult = personAge;
                        }
                    }
                }
                else if (personAge < 65)
                {
                    totalAgeAdult += personAge;
                    numberPeoplePolledAdult++;
                    if (numberPeoplePolledAdult == 1) // First person is the youngest and oldest
                    {
                        oldestAgeAdult = personAge;
                        youngestAgeAdult = personAge;
                    }
                    else
                    {
                        if (personAge > oldestAgeAdult)
                        {
                            oldestAgeAdult = personAge;
                        }
                        if (personAge < youngestAgeAdult)
                        {
                            youngestAgeAdult = personAge;
                        }
                    }
                }
                else
                {
                    totalAgeOlderCitizen += personAge;
                    numberPeoplePolledOlderCitizen++;
                    if (numberPeoplePolledOlderCitizen == 1) // First person is the youngest and oldest
                    {
                        oldestAgeOlderCitizen = personAge;
                        youngestAgeOlderCitizen = personAge;
                    }
                    else
                    {
                        if (personAge > oldestAgeOlderCitizen)
                        {
                            oldestAgeOlderCitizen = personAge;
                        }
                        if (personAge < youngestAgeOlderCitizen)
                        {
                            youngestAgeOlderCitizen = personAge;
                        }
                    }
                }
                Console.Write("have you someone to poll? (Y/N) >");
                isPersonPolled = Console.ReadLine().ToUpper() == "Y";

            }

            Console.WriteLine($"The final total of age {totalAge}");
            Console.WriteLine($"The total number of perople polled {numberPeoplePolled}");
            Console.WriteLine($"The youngest person is aged {youngestAge}");
            Console.WriteLine($"The oldest person is aged {oldestAge}");
            float meanAge = totalAge / numberPeoplePolled;
            Console.WriteLine($"The mean age is {meanAge}");
            Console.WriteLine($"The final total of age {totalAgeChild}");
            Console.WriteLine($"The total number of perople polled {numberPeoplePolledChild}");
            Console.WriteLine($"The youngest person is aged {youngestAgeChild}");
            Console.WriteLine($"The oldest person is aged {oldestAgeChild}");
            float meanAgeChild = totalAgeChild / numberPeoplePolledChild;
            Console.WriteLine($"The mean age is {meanAgeChild}");
            Console.WriteLine($"The final total of age {totalAgeTeenager}");
            Console.WriteLine($"The total number of perople polled {numberPeoplePolledTeenager}");
            Console.WriteLine($"The youngest person is aged {youngestAgeTeenager}");
            Console.WriteLine($"The oldest person is aged {oldestAgeTeenager}");
            float meanAgeTeenager = totalAgeTeenager / numberPeoplePolledTeenager;
            Console.WriteLine($"The mean age is {meanAgeTeenager}");
            Console.WriteLine($"The final total of age {totalAgeYoungAdult}");
            Console.WriteLine($"The total number of perople polled {numberPeoplePolledYoungAdult}");
            Console.WriteLine($"The youngest person is aged {youngestAgeYoungAdult}");
            Console.WriteLine($"The oldest person is aged {oldestAgeYoungAdult}");
            float meanAgeYoungAdult = totalAgeYoungAdult / numberPeoplePolledYoungAdult;
            Console.WriteLine($"The mean age is {meanAgeYoungAdult}");
            Console.WriteLine($"The final total of age {totalAgeAdult}");
            Console.WriteLine($"The total number of perople polled {numberPeoplePolledAdult}");
            Console.WriteLine($"The youngest person is aged {youngestAgeAdult}");
            Console.WriteLine($"The oldest person is aged {oldestAgeAdult}");
            float meanAgeAdult = totalAgeAdult / numberPeoplePolledAdult;
            Console.WriteLine($"The mean age is {meanAgeAdult}");
            Console.WriteLine($"The final total of age {totalAgeOlderCitizen}");
            Console.WriteLine($"The total number of perople polled {numberPeoplePolledOlderCitizen}");
            Console.WriteLine($"The youngest person is aged {youngestAgeOlderCitizen}");
            Console.WriteLine($"The oldest person is aged {oldestAgeOlderCitizen}");
            float meanAgeOlderCitizen = totalAgeOlderCitizen / numberPeoplePolledOlderCitizen;
            Console.WriteLine($"The mean age is {meanAgeOlderCitizen}");

        }
        static void DayThreeTeacherApproach2() {
            int[] numberPolled = new int[6];
            int[] totalAges = new int[6];
            int[] youngestAges = new int[6];
            int[] oldestAges = new int[6];
            CategoryID categoryID;
            string[] Categories = new string[] { "General", "Child", "Teenager", "Young Adult", "Adult", "Older Citizen" };
            while (PersonPoll())
            {
                Console.Write($"What is the age of person >");
                int personAge = Convert.ToInt32(Console.ReadLine());
                UpdateCategoryValues(CategoryID.General, personAge, numberPolled, totalAges, youngestAges, oldestAges);
                // Find the spceific category based on the age entered

                if (personAge < 13)
                {
                    categoryID = CategoryID.Child;
                }
                else if (personAge <= 19) // Teenager
                {
                    categoryID = CategoryID.Teenager;
                }
                else if (personAge <= 25)
                {
                    categoryID = CategoryID.YoungAdult;
                }
                else if (personAge < 65)
                {
                    categoryID = CategoryID.Adult;
                }
                else
                {
                    categoryID = CategoryID.Older;
                }
                UpdateCategoryValues(categoryID, personAge, numberPolled, totalAges, youngestAges, oldestAges);
            } //while()
            for (int i = 0; i < numberPolled.Length; i++)
            {
                DisplayResults(Categories[i], totalAges[i], numberPolled[i], youngestAges[i], oldestAges[i]);
            }
            static void DisplayResults(string category, int totalAge, int pollCount, int youngestAge, int oldestAge)
            {
                Console.WriteLine($"========== {category} ==============");
                if (pollCount > 0)
                {
                    Console.WriteLine($"Number polled is {pollCount}");
                    Console.WriteLine($"The youngest person is aged {youngestAge}");
                    Console.WriteLine($"The oldest person is aged {oldestAge}");
                    float meanAge = totalAge / pollCount;
                    Console.WriteLine($"The mean age is {meanAge}");
                }
                Console.WriteLine("===============================}");
            }//DisplayResults
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
    } // DayFour()
    static void DayThree()
    {
        // DayThreeApproach1();
        DayThreeApproach2();

        static void DayThreeApproach1()
        {
            List<int> age = new List<int>();
            age.Add(ReAskNumber("Please enter the number of users"));
            do
            {
                age.Add(ReAskNumber($"Please enter age for student{age.Count}"));
            }
            while (age.Count <= age[0]);
            Console.Write($"Age count is '{age.Count}' and total students are '{age[0]}', and the list values are ");
            age.ForEach(p => Console.WriteLine(p + " "));
        }
        static void DayThreeApproach2()
        {
            int[] age = new int[ReAskNumber("Please enter the number of people")];
            int[] catChild = new int[] { };
            int[] catTeenager = new int[] { };
            int[] catYoungAdult = new int[] { };
            int[] catAdult = new int[] { };
            int[] catOlderCitizen = new int[] { };

            for (int i = 0; i < age.Length; i++)
            {
                age[i] = ReAskNumber($"Please enter age for person {i + 1}");
                if (age[i] <= 0)
                {
                    Console.WriteLine($"Error. The age for person {i + 1} is could not be 0 or less");
                }
                else if (age[i] >= 0 && age[i] <= 12) // Child Category
                {
                    catChild = catChild.Concat(new int[] { age[i] }).ToArray();
                }
                else if (age[i] >= 13 && age[i] <= 19) // Teenager Category
                {
                    catTeenager = catTeenager.Concat(new int[] { age[i] }).ToArray();
                }
                else if (age[i] >= 20 && age[i] <= 24) // Young Adult Category
                {
                    catYoungAdult = catYoungAdult.Concat(new int[] { age[i] }).ToArray();
                }
                else if (age[i] >= 25 && age[i] <= 65) // Adult Category
                {
                    catAdult = catAdult.Concat(new int[] { age[i] }).ToArray();
                }
                else if (age[i] >= 66) // Older Citizen Category
                {
                    catOlderCitizen = catOlderCitizen.Concat(new int[] { age[i] }).ToArray();
                }
            }

            Console.Clear();
            //Console.WriteLine(); //Cleared screen
            //Console.WriteLine($"Category Child: [{{0}}]", string.Join(", ", catChild));
            //Console.Write($"Age average is '{age.Average()}' of total students are '{age.Length}', and the list values are [{{0}}]", string.Join(", ", age));
            PrintStats("", age);
            PrintStats("Child", catChild);
            PrintStats("Teenager", catTeenager);
            PrintStats("Young Adult", catYoungAdult);
            PrintStats("Adult", catAdult);
            PrintStats("Older Citizen", catOlderCitizen);
            //Console.WriteLine($"The mean Age is {age} years and {meanAgeDays} days");
        }
    }
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
            // 2^(32) = 4,284B
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
        int readFirstNumber17 = ReAskNumber("Please enter the first number and press Enter");
        int readSecondNumber17 = ReAskNumber("Please enter the second number and press Enter");
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
    }
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
    }
} // class Program
