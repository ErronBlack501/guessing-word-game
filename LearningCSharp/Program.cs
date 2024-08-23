namespace LearningCSharp
{
    public enum LevelEnum
    {
        Easy,
        Medium,
        Hard,
        UltraHard
    }
    internal class Program
    {
        private static char s_letterPlayer;
        private static bool[] s_lettersFound = null!;
        private static string s_mysteryWord = null!;
        private static readonly string s_fileDirectory = "Dico/";
        private static List<char> s_lettersTyped = new List<char>();
        private static ushort s_lifes;
        private static string s_level = String.Empty;

        static void Main(string[] args)
        {
            Console.Title = "Jeu du Pendu par ErronBlack501";
            Console.WriteLine("-----------------BIENVENUE DANS LE JEU DU PENDU-----------------\n");
            PickLevel();
            
            do
            {
                GenerateMysteryWord();  
                do
                {
                    if (s_lifes > 1)
                        Console.WriteLine($"---Il vous reste {s_lifes} essais---");
                    else
                        Console.WriteLine($"---Il vous reste {s_lifes} essai---");

                    PrintMysteryWord();
                    Console.Write("Saisissez une lettre : ");
                    s_letterPlayer = InputLetter();

                    if (!s_lettersTyped.Contains(s_letterPlayer))
                    {
                        if (s_mysteryWord.Contains(s_letterPlayer))
                            Update();
                        else
                        {
                            Console.Beep();
                            s_lifes--;
                        }
                        s_lettersTyped.Add(s_letterPlayer);
                    }
                    else
                        Console.WriteLine($"Vous avez déjà entré la lettre \"{s_letterPlayer}\", essayer une nouvelle lettre.");

                } while (!IsWinning() && s_lifes > 0);
                
                Console.Write('\n');
                
                if (s_lifes > 0)
                    Console.WriteLine($"Bravo!\nVous avez gagné, le mot mystère était bien \"{s_mysteryWord}\".");
                else
                    Console.WriteLine($"Dommage!\nVous avez perdu car vous avez atteint le nombre limite d'essai.\nLe mot mystère était \"{s_mysteryWord}\".");
            
            } while (IsRestarting());
            Console.WriteLine("-----------AU REVOIR------------");
        }

        private static void PickLevel()
        {
            string[] choices = { "1", "2", "3", "4" };
            string? choice;
            do
            {
                Console.Write('\n');
                Console.WriteLine("Choisissez une difficultée: \n1 => Facile\n2 => Moyenne\n3 => Difficile\n4 => Très difficile");
                choice = Console.ReadLine();
            } while (choice == null || !choices.Contains(choice));
            
            switch (choice) 
            {
                case "1":
                    s_level = "Easy"; 
                    break;
                case "2":
                    s_level = "Medium";
                    break;
                case "3": 
                    s_level = "Hard";
                    break;
                case "4": 
                    s_level = "UltraHard";
                    break;
                default:
                    Console.WriteLine("-----Erreur fatale-----");
                    break;
            }
        }

        private static bool IsWinning()
        {
            foreach (bool b in s_lettersFound) 
            {
                if (!b)
                {
                    return false;
                }
            }
            return true;
        }

        private static void Update()
        {
            for (int i = 0; i < s_mysteryWord.Length; i++)
            {
                if (s_letterPlayer.Equals(s_mysteryWord[i]))
                {
                    s_lettersFound[i] = true;
                }
            }
        }

        private static char InputLetter()
        {
            string? s_letterPlayer = Console.ReadLine();

            if (s_letterPlayer == String.Empty || s_letterPlayer == null)
            {
                do
                {
                    Console.WriteLine("Entrez une lettre SVP!!! : ");
                    s_letterPlayer = Console.ReadLine();
                } while (s_letterPlayer == String.Empty || s_letterPlayer == null);
            }
            return s_letterPlayer.Trim().ToUpper().ElementAt(0);
        }

        private static void PrintMysteryWord()
        {
            Console.Write("\n");
            Console.Write("Mot mystère: ");
            for (int i = 0; i < s_lettersFound.Length; i++)
            {
                if (s_lettersFound[i])
                {
                    Console.Write(s_mysteryWord[i]);
                }
                else
                {
                    Console.Write("_");
                }
            }
            Console.Write("\n");
        }

        private static bool IsRestarting()
        {
            char answer = default;
            do
            {
                Console.Write("Voulez-vous une nouvelle partie (O: oui, N: non)? : ");
                string? input = Console.ReadLine();
            
                if (input != null && input != String.Empty)
                    answer = input.Trim().ToUpper().ElementAt(0);
                Console.Write('\n');
            
            } while (answer != 'O' && answer != 'N');
            
            if (answer == 'O')
            {
                return true;
            }
            else
                return false;
        }

        private static void GenerateMysteryWord()
        {
            Random random = new Random();
            string[] fileTextLines = File.ReadAllLines($"{s_fileDirectory}/{s_level}.txt");
            s_mysteryWord = fileTextLines[random.Next(fileTextLines.Length)];
            s_lettersFound = new bool[s_mysteryWord.Length];
            Array.Fill(s_lettersFound, false);
            s_lifes = 5;
            s_letterPlayer = default;
            s_lettersTyped.Clear();
            Console.Clear();
        }
    }
}
