using CharacterNamespace;

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
List<Character> charlist = [];
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

            string charname = "";
            string buildname = "";
            int buildint = 0;
            string classname = "";
            int classint = 0;

            string textanswer = "n";
            while (textanswer == "n")
            {
                Console.Write("\nState your build/character name: ");
                charname = Console.ReadLine()!;
                Console.Write($"{charname} is that right? (y/n): ");
                textanswer = Convert.ToString(Console.ReadLine())!;

                while (textanswer != "n" && textanswer != "y")
                {
                    Console.Write($"Invalid answer, {charname} is that right? (y/n):");
                    textanswer = Convert.ToString(Console.ReadLine())!;
                }
            }

            textanswer = "n";
            while (textanswer == "n")
            {
                Console.WriteLine($"\nWhat kind of build will {charname} be? (chose one):");
                Console.WriteLine("1 - PP (Pure potter) (100% Prepare potion + mixed cooking* )");
                Console.WriteLine("2 - PP (100% Special pharmacy)");
                Console.WriteLine("3 - Bionic Pharmacy (luk + dex + crit)");

                while (!Int32.TryParse(Console.ReadLine(), out buildint) || buildint < 1 || buildint > 4)
                {
                    Console.WriteLine("invalid choice");
                }

                builddic.TryGetValue(buildint, out buildname!);

                Console.Write($"{buildname} is that right? (y/n):");
                textanswer = Convert.ToString(Console.ReadLine())!;
                while (textanswer != "n" && textanswer != "y")
                {
                    Console.Write($"Invalid answer, {buildname} is that right? (y/n):");
                    textanswer = Convert.ToString(Console.ReadLine())!;
                }
            }

            if (buildint == 3)
            {
                classint = 4;
                classdic.TryGetValue(classint, out classname!);
            }
            else
            {
                textanswer = "n";
                while (textanswer == "n")
                {
                    Console.WriteLine($"What class is {charname}? (chose one)");
                    int index = 0;
                    if (buildint == 1)
                    {
                        index += 1;
                        Console.WriteLine($"{index} - Alchemist");
                        index += 1;
                        Console.WriteLine($"{index} - Biochemist");
                    }
                    if (buildint <= 2)
                    {
                        index += 1;
                        Console.WriteLine($"{index} - Geneticist");
                        index += 1;
                        Console.WriteLine($"{index} - Biolo");
                    }

                    while (!Int32.TryParse(Console.ReadLine(), out classint) || classint < 1 || classint > index)
                    {
                        classdic.TryGetValue(classint + (4 - index), out classname!);
                        if ((classint < 1 && classint > index) || classname == null)
                        {
                            Console.WriteLine("invalid choice, try again.");
                        }
                    }

                    classdic.TryGetValue(classint + (4 - index), out classname!);

                    Console.WriteLine($"{classname} is that right? (y/n):");
                    textanswer = Convert.ToString(Console.ReadLine())!;
                    while (textanswer != "n" && textanswer != "y")
                    {
                        Console.Write($"Invalid answer, {classname} is that right? (y/n):");
                        textanswer = Convert.ToString(Console.ReadLine())!;
                    }
                    classint += 4-index;
                }

            }

            Character character = new Character(charname,buildname,buildint,classname,classint);
            charlist.Add(character);
            character.DisplayCharacter();
            
            break;
        case 2:
            Console.WriteLine("you chose to review builds, good!");
            if (charlist.Count() == 0) {Console.WriteLine("you don't have any build now, create one!");}
            else{Console.WriteLine($"You got {charlist.Count()} characters choose one:");}
            
            int charindex = 1;
            foreach (Character cha in charlist)
            {   
                Console.WriteLine($"{charindex} - {cha.charname}, {cha.classname}, {cha.buildname}");
            }
            Console.WriteLine(charlist);
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

            

            

            

            

            
    }
}
