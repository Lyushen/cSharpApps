internal class Day03
{
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
    } // DayThree()
}