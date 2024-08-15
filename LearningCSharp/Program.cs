namespace LearningCSharp
{
    internal class Program
    {
        private static char LetterPlayer;
        private static bool[] LettersFound = null!;
        private static string MysteryWord = null!;
        private static readonly string FileDirectory = "Dico/";
        private static List<char> LettersTyped = new List<char>();
        private static ushort Lifes;

        static void Main(string[] args)
        {
            Console.Title = "Jeu du Pendu par ErronBlack501";
            Console.WriteLine("-----------------BIENVENUE DANS LE JEU DU PENDU-----------------\n");
         
            do
            {
                GenerateMysteryWord("Easy");  
                do
                {
                    if (Lifes > 1)
                        Console.WriteLine($"---Il vous reste {Lifes} essais---");
                    else
                        Console.WriteLine($"---Il vous reste {Lifes} essai---");

                    PrintMysteryWord();
                    Console.Write("Saisissez une lettre : ");
                    LetterPlayer = InputLetter();

                    if (!LettersTyped.Contains(LetterPlayer))
                    {
                        if (MysteryWord.Contains(LetterPlayer))
                            Update();
                        else
                        {
                            Console.Beep();
                            Lifes--;
                        }
                        LettersTyped.Add(LetterPlayer);
                    }
                    else
                        Console.WriteLine($"Vous avez déjà entré la lettre \"{LetterPlayer}\", essayer une nouvelle lettre.");

                } while (!IsWinning() && Lifes > 0);
                
                Console.Write('\n');
                
                if (Lifes > 0)
                    Console.WriteLine($"Bravo!\nVous avez gagné, le mot mystère était bien \"{MysteryWord}\".");
                else
                    Console.WriteLine($"Dommage!\nVous avez perdu car vous avez atteint le nombre limite d'essai.\nLe mot mystère était \"{MysteryWord}\".");
            
            } while (IsRestarting());
            Console.WriteLine("-----------AU REVOIR------------");
        }

        private static bool IsWinning()
        {
            foreach (bool b in LettersFound) 
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
            for (int i = 0; i < MysteryWord.Length; i++)
            {
                if (LetterPlayer.Equals(MysteryWord[i]))
                {
                    LettersFound[i] = true;
                }
            }
        }

        private static char InputLetter()
        {
            string? LetterPlayer = Console.ReadLine();

            if (LetterPlayer == String.Empty || LetterPlayer == null)
            {
                do
                {
                    Console.WriteLine("Entrez une lettre SVP!!! : ");
                    LetterPlayer = Console.ReadLine();
                } while (LetterPlayer == String.Empty || LetterPlayer == null);
            }
            return LetterPlayer.Trim().ToUpper().ElementAt(0);
        }

        private static void PrintMysteryWord()
        {
            Console.Write("\n");
            Console.Write("Mot mystère: ");
            for (int i = 0; i < LettersFound.Length; i++)
            {
                if (LettersFound[i])
                {
                    Console.Write(MysteryWord[i]);
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
                Console.Write("Voulez-vous une nouvelle partie ? : ");
                string? input = Console.ReadLine();
            
                if (input != null && input != String.Empty)
                    answer = input.Trim().ToUpper().ElementAt(0);
                Console.Write('\n');
            
            } while (answer != 'O' && answer != 'N');
            
            if (answer == 'O')
            {
                Console.Clear();    
                return true;
            }
            else
                return false;
        }

        private static void GenerateMysteryWord(string level)
        {
            Random random = new Random();
            string[] fileTextLines = File.ReadAllLines($"{FileDirectory}/{level}.txt");
            MysteryWord = fileTextLines[random.Next(fileTextLines.Length)];
            LettersFound = new bool[MysteryWord.Length];
            Array.Fill(LettersFound, false);
            Lifes = 3;
            LetterPlayer = default;
            LettersTyped.Clear();
        }
    }
}
