using System.Runtime.InteropServices;

namespace CharacterNamespace
{
    public class Character
    {   
        public string classname;
        public string buildname;
        public string charname;
        public int buildint;
        public int classint;
        public List<int> stats = [1,1,1,0];
        public int joblevel;
        public int baselevel;
        public int statPoints = 0;
        public int talentPoints = 0;
        public List<decimal> buildmulti = [0m,0m,0m];

        public Character(string charname, string buildname, int buildint, string classname, int classint)
        {
            this.charname = charname;
            this.buildname = buildname;
            this.buildint = buildint;
            this.classname = classname;
            this.classint = classint;
            this.ClassDefaults();
            this.BuildDefaults();
        }

        public void DisplayCharacter()
        {
            Console.WriteLine($"Character name : {charname}");
            Console.WriteLine($"Class : {classname}");
            Console.WriteLine($"Build Type : {buildname}");
            StatsDisplay();
            CreationChances();
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

        decimal StatEfficiency(int statcost, decimal multiplier)
        {
            return multiplier / statcost;
        }

        void ClassDefaults()
        {
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
        }

        void BuildDefaults()
        {
            switch (buildint)
            {
                case 1:
                    buildmulti[0] = 0.1m;
                    buildmulti[1] = 0.05m;
                    buildmulti[2] = 0.1m;
                    break;
                case 2:
                    buildmulti[0] = 0.5m;
                    buildmulti[1] = 1m;
                    buildmulti[2] = 1m;
                    break;
                case 3:
                    for (int i = 0; i < 129; i++)
                    {
                        statPoints -= StatCost(stats[2]);
                        stats[2] += 1;
                        statPoints -= StatCost(stats[0]);
                        stats[0] += 1;
                    }
                    break;
            }
        }
        public void DistributePoints()
        {
            decimal inteff = StatEfficiency(StatCost(stats[0]), buildmulti[1]);
            decimal dexeff = StatEfficiency(StatCost(stats[1]), buildmulti[0]);
            decimal lukeff = StatEfficiency(StatCost(stats[2]), buildmulti[2]);
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
                inteff = StatEfficiency(StatCost(stats[0]), buildmulti[1]);
                dexeff = StatEfficiency(StatCost(stats[1]), buildmulti[0]);
                lukeff = StatEfficiency(StatCost(stats[2]), buildmulti[2]);

                if (StatCost(stats[0]) > statPoints) { inteff = -90m; }
                if (StatCost(stats[1]) > statPoints) { dexeff = -90m; }
                if (StatCost(stats[2]) > statPoints) { lukeff = -90m; }
            }
        }

        void StatsDisplay()
        {
            Console.WriteLine($"\nStats:");
            if (stats[0] != 0) { Console.WriteLine($"Int = {stats[0]}"); }
            if (stats[1] != 0) { Console.WriteLine($"Dex = {stats[1]}"); }
            if (stats[2] != 0) { Console.WriteLine($"Luk = {stats[2]}"); }
            Console.WriteLine($"StatPoints left = {statPoints}");

            if (stats[3] != 0)
            {
                Console.WriteLine("\nTalents:");
                Console.WriteLine($"CRT = {stats[3]}");
                Console.WriteLine($"TalentPoints left = {talentPoints}");
            }
        }

        void CreationChances()
        {
            Console.WriteLine("\n--Creation chances--");

            if (classint >= 1)
            {
                decimal ppchance = 30 + 10 + 5 + joblevel * 0.2m + stats[0] * 0.05m + stats[1] * 0.1m + stats[2] * 0.1m;
                decimal ppchancemin = ppchance - 15;
                decimal ppchancemax = ppchance - 5;
                Console.WriteLine($"Prepare potion chance: {ppchancemin} ~ {ppchancemax} %");
            }

            if (classint >= 3)
            {
                decimal spchance = stats[0] + (stats[1] / 2) + stats[2] + joblevel + 50 + (baselevel - 100);
                decimal spchancemin = spchance + 30 + 40;
                decimal spchancemax = spchance + 150 + 100;
                Console.WriteLine($"Special pharmacy creation: {spchancemin} ~ {spchancemax}");

                decimal mcchance = (joblevel / 4) + (stats[1] / 3) + (stats[2] / 2);
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
            Console.WriteLine();
        }


    }
}