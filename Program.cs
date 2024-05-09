// See https://aka.ms/new-console-template for more information

Dictionary<int, string> builddic = new Dictionary<int, string>();
builddic.Add(1, "PP + Mixed Coooking");
builddic.Add(2, "PP Special Pharmacy");
builddic.Add(3, "Bionic Pharmacy");

Dictionary<int, string> classdic = new Dictionary<int, string>();
classdic.Add(1, "Alchemist");
classdic.Add(2, "Biochemist");
classdic.Add(3, "Geneticist");
classdic.Add(4, "Biolo");


int choice = 0;
while (choice != 4)
{
    Console.WriteLine("--welcome to an ragnarok pure potter build calculator--");
    Console.WriteLine("what do you want to do ?");
    Console.WriteLine("1 - Create a new character.");
    Console.WriteLine("2 - Review builds.");
    Console.WriteLine("3 - Delete builds.");
    Console.WriteLine("4 - Exit.");
    Console.Write("Your choice : ");

    while (!Int32.TryParse(Console.ReadLine(), out choice) || (choice < 1 && choice > 4))
    {
        Console.WriteLine("invalid choice");
    }

    switch (choice)
    {

        case 1:
            Console.WriteLine("you chose to create a new character, good!");
            string sure = "n";
            string name = "";
            string buildname = "";
            int buildint = 0;
            string classname = "";
            int classint = 0;
            int joblevel = 1;
            int baselevel = 1;
            List<int> stats = [1, 1, 1, 0]; // INT, DEX, LUK ,CRT

            while (sure == "n")
            {
                Console.Write("State your build/character name: ");
                name = Console.ReadLine() ?? "-";
                Console.Write($"{name} is that right? (y/n): ");
                sure = Convert.ToString(Console.ReadLine())!;
                while (sure != "n" && sure != "y")
                {
                    Console.Write($"Invalid answer, {buildname} is that right? (y/n):");
                    sure = Convert.ToString(Console.ReadLine())!;
                }
            }

            sure = "n";
            while (sure == "n")
            {
                Console.WriteLine($"What kind of build will {name} be? (chose one):");
                Console.WriteLine("1 - PP (Pure potter) (100% Prepare potion + mixed cooking* )");
                Console.WriteLine("2 - PP (100% Special pharmacy)");
                Console.WriteLine("3 - Bionic Pharmacy (luk + dex + crit)");

                while (!Int32.TryParse(Console.ReadLine(), out buildint) || (buildint < 1 && buildint > 4))
                {
                    Console.WriteLine("invalid choice");
                }

                builddic.TryGetValue(buildint, out buildname!);

                Console.Write($"{buildname} is that right? (y/n):");
                sure = Convert.ToString(Console.ReadLine())!;
                while (sure != "n" && sure != "y")
                {
                    Console.Write($"Invalid answer, {buildname} is that right? (y/n):");
                    sure = Convert.ToString(Console.ReadLine())!;
                }
            }

            sure = "n";
            while (sure == "n")
            {
                if (buildint == 3)
                {
                    classint = 4;
                    classdic.TryGetValue(classint, out classname!);
                    sure = "y";
                }
                else
                {
                    Console.WriteLine($"What class is {name}? (chose one)");
                    Console.WriteLine("1 - Alchemist");
                    Console.WriteLine("2 - Biochemist");
                    Console.WriteLine("3 - Geneticist");
                    Console.WriteLine("4 - Biolo\n");
                    
                    while (!Int32.TryParse(Console.ReadLine(), out classint) 
                    || (classint < 1 && classint > 4) 
                    || ((classint == 1 || classint == 2) && (buildint == 2 || buildint == 3)) 
                    || (classint == 1 || classint == 2 || classint == 3) 
                    && (buildint == 3))
                    {
                        classdic.TryGetValue(classint, out classname!);
                        if ((classint < 1 && classint > 4)|| classname == null)
                        {
                            Console.WriteLine("invalid choice, try again.");
                        }
                        if ((classint == 1 || classint == 2) && (buildint == 2 || buildint == 3))
                        {
                            Console.WriteLine($"{classname} don't have the correct skill for this buildtype");
                        }

                    }

                    classdic.TryGetValue(classint, out classname!);

                    Console.WriteLine($"{classname} is that right? (y/n):");
                    sure = Convert.ToString(Console.ReadLine())!;
                    while (sure != "n" && sure != "y")
                    {
                        Console.Write($"Invalid answer, {classname} is that right? (y/n):");
                        sure = Convert.ToString(Console.ReadLine())!;
                    }
                }
            }

            OptimizeBuild(buildint, classint, stats);

            CreationChances(stats, classint, joblevel);

            break;
        case 2:
            Console.WriteLine("you chose to review builds, good!");
            break;
        case 3:
            Console.WriteLine("you chose to delete builds, good!");
            break;
        case 4:
            Console.WriteLine("good bye!");
            break;
        default:
            break;
        //end

            List<int> DistributePoints(List<int> stats, decimal dexmulti, decimal intmulti, decimal lukmulti, int statPoints, int talentPoints)
            {
                decimal inteff = StatEfficiency(StatCost(stats[0]), intmulti);
                decimal dexeff = StatEfficiency(StatCost(stats[1]), dexmulti);
                decimal lukeff = StatEfficiency(StatCost(stats[2]), lukmulti);
                while (StatCost(stats[0]) < statPoints || StatCost(stats[1]) < statPoints || StatCost(stats[2]) < statPoints)
                {
                    if (inteff > dexeff && inteff > lukeff)
                    {
                        statPoints -= StatCost(stats[0]);
                        stats[0] += 1;
                    }
                    else
                    {
                        if (dexeff > lukeff)
                        {
                            statPoints -= StatCost(stats[1]);
                            stats[1] += 1;
                        }
                        else
                        {
                            statPoints -= StatCost(stats[2]);
                            stats[2] += 1;
                        }
                    }
                    inteff = StatEfficiency(StatCost(stats[0]), intmulti);
                    dexeff = StatEfficiency(StatCost(stats[1]), dexmulti);
                    lukeff = StatEfficiency(StatCost(stats[2]), lukmulti);

                    if (StatCost(stats[0]) > statPoints) { inteff = -90m; }
                    if (StatCost(stats[1]) > statPoints) { dexeff = -90m; }
                    if (StatCost(stats[2]) > statPoints) { lukeff = -90m; }
                }

                StatsDisplay(stats, statPoints, talentPoints);
                
                return stats;
            }

            void StatsDisplay(List<int> stats, int statPoints, int talentPoints)
            {
                Console.WriteLine($"Stats:");
                if (stats[0] != 0) { Console.WriteLine($"Int = {stats[0]}"); }
                if (stats[1] != 0) { Console.WriteLine($"Dex = {stats[1]}"); }
                if (stats[2] != 0) { Console.WriteLine($"Luk = {stats[2]}"); }
                Console.WriteLine($"StatPoints left = {statPoints}");

                if (stats[3] != 0)
                {
                    Console.WriteLine("Talents:");
                    Console.WriteLine($"CRT = {stats[3]}");
                    Console.WriteLine($"TalentPoints left = {talentPoints}");
                }
            }

            void CreationChances(List<int> stats, int classint, int job)
            {
                Console.WriteLine("--Creation chances--");

                if (classint >= 1)
                {
                    decimal ppchance = 30 + 10 + 5 + job * 0.2m + stats[0] * 0.05m + stats[1] * 0.1m + stats[2] * 0.1m;
                    decimal ppchancemin = ppchance - 15;
                    decimal ppchancemax = ppchance - 5;
                    Console.WriteLine($"Prepare potion chance: {ppchancemin} ~ {ppchancemax} %");
                }

                if (classint >= 3)
                {
                    decimal spchance = stats[0] + (stats[1] / 2) + stats[2] + job + 50 + (baselevel - 100);
                    decimal spchancemin = spchance + 30 + 40;
                    decimal spchancemax = spchance + 150 + 100;
                    Console.WriteLine($"Special pharmacy creation: {spchancemin} ~ {spchancemax}");

                    decimal mcchance = (job / 4) + (stats[1] / 3) + (stats[2] / 2);
                    decimal mcchancemin = mcchance - 150 - 15;
                    decimal mcchancemax = mcchance - 30 - 15;
                    Console.WriteLine($"Mixed Cooking creation: {mcchancemin} ~ {mcchancemax}");
                }

                if (classint == 4)
                {
                    decimal bpchance = ((baselevel - 200) * 4) + stats[0] + stats[2] + (stats[3] * 2) + 100;
                    decimal bpchancemin = bpchance + 300;
                    decimal bpchancemax = bpchance + 400;
                    Console.WriteLine($"Bionic pharmacy creation: {bpchancemin} ~ {bpchancemax}");
                }
            }

            decimal StatEfficiency(int statcost, decimal multiplier)
            {
                return multiplier / statcost;
            }

            int StatCost(int attribute)
            {
                int cost = 999;
                if (attribute < 130)
                {
                    if (attribute <= 99)
                    {
                        cost = (int)Math.Floor((decimal)(((attribute - 1) / 10) + 2));
                    }
                    else
                    {
                        cost = 4 * (int)Math.Floor((decimal)((attribute - 100) / 5)) + 16;
                    }
                }
                return cost;
            }

            void OptimizeBuild(int buildint, int classint, List<int> stats)
            {
                decimal dexmulti;
                decimal intmulti;
                decimal lukmulti;
                int statPoints = 0;
                int talentPoints = 0;
                switch (classint)
                {
                    case 1:
                        statPoints = 1273;
                        joblevel = 50;
                        baselevel = 99;
                        break;

                    case 2:
                        statPoints = 1325;
                        joblevel = 70;
                        baselevel = 99;
                        break;

                    case 3:
                        statPoints = 2597;
                        joblevel = 70;
                        baselevel = 200;
                        break;

                    case 4:
                        statPoints = 4151;
                        talentPoints = 97;
                        stats[3] = 100;
                        joblevel = 50;
                        baselevel = 250;
                        break;
                }

                switch (buildint)
                {
                    case 1:
                        dexmulti = 0.1m;
                        intmulti = 0.05m;
                        lukmulti = 0.1m;
                        stats = DistributePoints(stats, dexmulti, intmulti, lukmulti, statPoints, talentPoints);
                        break;
                    case 2:
                        dexmulti = 0.5m;
                        intmulti = 1m;
                        lukmulti = 1m;
                        stats = DistributePoints(stats, dexmulti, intmulti, lukmulti, statPoints, talentPoints);
                        break;
                    case 3:
                        for (int i = 0; i < 129; i++)
                        {
                            statPoints -= StatCost(stats[2]);
                            stats[2] += 1;
                            statPoints -= StatCost(stats[0]);
                            stats[0] += 1;
                        }
                        StatsDisplay(stats, statPoints, talentPoints);
                        break;
                }
            }
    }
}


