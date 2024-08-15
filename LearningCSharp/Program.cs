namespace LearningCSharp
{
    internal class Program
    {
        private static char _letterPlayer;
        private static bool[] _lettersFound = null!;
        private static string _mysteryWord = null!;
        private static readonly string _fileDirectory = "Dico/";
        private static List<char> _lettersTyped = new List<char>();
        private static ushort _lifes;

        static void Main(string[] args)
        {
            Console.Title = "Jeu du Pendu par ErronBlack501";
            Console.WriteLine("-----------------BIENVENUE DANS LE JEU DU PENDU-----------------\n");
         
            do
            {
                GenerateMysteryWord("Easy");  
                do
                {
                    if (_lifes > 1)
                        Console.WriteLine($"---Il vous reste {_lifes} essais---");
                    else
                        Console.WriteLine($"---Il vous reste {_lifes} essai---");

                    PrintMysteryWord();
                    Console.Write("Saisissez une lettre : ");
                    _letterPlayer = InputLetter();

                    if (!_lettersTyped.Contains(_letterPlayer))
                    {
                        if (_mysteryWord.Contains(_letterPlayer))
                            Update();
                        else
                        {
                            Console.Beep();
                            _lifes--;
                        }
                        _lettersTyped.Add(_letterPlayer);
                    }
                    else
                        Console.WriteLine($"Vous avez déjà entré la lettre \"{_letterPlayer}\", essayer une nouvelle lettre.");

                } while (!IsWinning() && _lifes > 0);
                
                Console.Write('\n');
                
                if (_lifes > 0)
                    Console.WriteLine($"Bravo!\nVous avez gagné, le mot mystère était bien \"{_mysteryWord}\".");
                else
                    Console.WriteLine($"Dommage!\nVous avez perdu car vous avez atteint le nombre limite d'essai.\nLe mot mystère était \"{_mysteryWord}\".");
            
            } while (IsRestarting());
            Console.WriteLine("-----------AU REVOIR------------");
        }

        private static bool IsWinning()
        {
            foreach (bool b in _lettersFound) 
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
            for (int i = 0; i < _mysteryWord.Length; i++)
            {
                if (_letterPlayer.Equals(_mysteryWord[i]))
                {
                    _lettersFound[i] = true;
                }
            }
        }

        private static char InputLetter()
        {
            string? _letterPlayer = Console.ReadLine();

            if (_letterPlayer == String.Empty || _letterPlayer == null)
            {
                do
                {
                    Console.WriteLine("Entrez une lettre SVP!!! : ");
                    _letterPlayer = Console.ReadLine();
                } while (_letterPlayer == String.Empty || _letterPlayer == null);
            }
            return _letterPlayer.Trim().ToUpper().ElementAt(0);
        }

        private static void PrintMysteryWord()
        {
            Console.Write("\n");
            Console.Write("Mot mystère: ");
            for (int i = 0; i < _lettersFound.Length; i++)
            {
                if (_lettersFound[i])
                {
                    Console.Write(_mysteryWord[i]);
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
            string[] fileTextLines = File.ReadAllLines($"{_fileDirectory}/{level}.txt");
            _mysteryWord = fileTextLines[random.Next(fileTextLines.Length)];
            _lettersFound = new bool[_mysteryWord.Length];
            Array.Fill(_lettersFound, false);
            _lifes = 5;
            _letterPlayer = default;
            _lettersTyped.Clear();
        }
    }
}
