internal class Day04
{
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
        static void DayThreeTeacherApproach2()
        {
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
}