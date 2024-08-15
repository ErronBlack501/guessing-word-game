namespace LearningCSharp
{
    internal class Program
    {
        private static char LetterPlayer = default;
        private static bool[] LettersFound = null!;
        private static string MysteryWord = "EXEMPLE";
        private static List<char> LettersTyped = new List<char>();
        private static ushort Lifes = 3;

        static void Main(string[] args)
        {
            Console.WriteLine("-----------------BIENVENUE DANS LE JEU DU PENDU-----------------\n");
            LettersFound = new bool[MysteryWord.Length];
            Array.Fill(LettersFound, false);

            do
            {
                do
                {
                    Console.WriteLine($"---Il vous reste {Lifes} essais---");
                    PrintMysteryWord();
                    Console.Write("Saisissez une lettre : ");
                    LetterPlayer = InputLetter();

                    if (!LettersTyped.Contains(LetterPlayer))
                    {
                        if (MysteryWord.Contains(LetterPlayer))
                            Update();
                        else
                            Lifes--;
                        LettersTyped.Add(LetterPlayer);
                    }
                    else
                        Console.WriteLine($"Vous avez déjà entré la lettre \"{LetterPlayer}\", essayer une nouvelle lettre.");

                } while (!IsWinning() && Lifes > 0);
                
                if (Lifes > 0)
                    Console.WriteLine($"Bravo!\nVous avez gagné, le mot mystère était bien \"{MysteryWord}\".");
                else
                    Console.WriteLine("Dommage!\nVous avez perdu car vous avez atteint le nombre limite d'essai.");
            
            } while (IsRestarting());          
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
                return true;
            else
                return false;
        }
    }
}
