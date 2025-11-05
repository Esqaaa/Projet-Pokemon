using System;
using PokemonBattle;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        TypeWriterEffect("⚔️ Welcome to the Pokemon Battle Console !");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        TypeWriterEffect("\nPress Enter to start the fight...");
        Console.ResetColor();
        Console.ReadLine(); //Attend que l'utilisateur appuie sur entrée

    
        // Importation du pokedex 
        string filePath = "pokedex.csv";
        List<Pokemon> pokemons = PokemonLoader.LoadFromCSV(filePath);

        if (pokemons.Count < 2)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Le fichier doit contenir au moins deux Pokémon pour lancer un combat.");
            Console.ResetColor();
            return;
        }

        // Accès au pokedex ou poursuite du code 
        Console.WriteLine("📜 Accéder au pokédex (y/n) : ");
        string? choice = Console.ReadLine();
        if (choice != null && choice.ToLower() == "y")
        {
            Console.WriteLine("\nListe des Pokémon disponibles :");
            for (int i = 0; i < pokemons.Count; i++)
            {
                Console.WriteLine($"{i} - {pokemons[i].Nom}");
            }
        }
        else
        {
            // Poursuite du code 
        }

        // Demande à l'utilisateur quel pokemon veut-il utiliser 
        Console.WriteLine("\nQuel Pokémon voulez-vous dans votre équipe ? (N° ou nom) : ");
        string? input = Console.ReadLine();
        Console.Clear();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Entrée vide.");
            Console.ResetColor();
            return;
        }

        Pokemon? pokemon1 = null;

        if (int.TryParse(input, out int index))
        {
            if (index >= 0 && index < pokemons.Count)
            {
                pokemon1 = pokemons[index];
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Numéro invalide.");
                Console.ResetColor();
                return;
            }
        }
        else
        {
            pokemon1 = pokemons.Find(p => p.Nom.Equals(input, StringComparison.OrdinalIgnoreCase));
            if (pokemon1 is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Aucun Pokémon nommé '{input}' trouvé.");
                Console.ResetColor();
                return;
            }
        }

        Pokemon pokemon1Selected = pokemon1;

        // Pokemon ennemi défini aléatoirement 
        Random rnd = new Random();
        Pokemon pokemon2 = pokemons[rnd.Next(pokemons.Count)];



        Console.ForegroundColor = ConsoleColor.White;
        TypeWriterEffect("Les combattants entrent dans l'arène...");
        Console.ResetColor();

        Thread.Sleep(1000); // Petite pause pour le suspense

        // Affichage des stats des Pokémons
        Console.ForegroundColor = ConsoleColor.Yellow;
        TypeWriterEffect($"⚡ {pokemon1.Nom} - Type: {pokemon1.Type}, PV: {pokemon1.HealthPoint}, Attaque: {pokemon1.Attack}, Défense: {pokemon1.Defense}");
        Console.ResetColor();

        Thread.Sleep(500);

        Console.ForegroundColor = ConsoleColor.Gray;
        TypeWriterEffect($"🌟 {pokemon2.Nom} - Type: {pokemon2.Type}, PV: {pokemon2.HealthPoint}, Attaque: {pokemon2.Attack}, Défense: {pokemon2.Defense}");
        Console.ResetColor();

        Thread.Sleep(1000);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        TypeWriterEffect("\nQue le combat commence !");
        Console.ResetColor();


        int tour = 1;

        while (pokemon1.HealthPoint > 0 && pokemon2.HealthPoint > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\n=== Tour {tour} de combat ===");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{pokemon1.Nom}  PV: {pokemon1.HealthPoint}");
            Console.WriteLine($"{pokemon2.Nom}  PV: {pokemon2.HealthPoint}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{pokemon1.Nom} attaque !");
            pokemon1.Attaquer(pokemon2, pokemon1.Attack);
            Console.ResetColor();


            if (pokemon2.HealthPoint > 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{pokemon2.Nom} riposte!");
                pokemon2.Attaquer(pokemon1, pokemon2.Attack);
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress Enter to move on to the next round !");
            Console.ResetColor();
            Console.ReadLine(); 
            tour++;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n=== Fin du combat ===");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Green;
        if (pokemon1.HealthPoint <= 0)
        {
            TypeWriterEffect($" {pokemon2.Nom} a gagné le combat et il s'est terminé en {tour} tours !");
        }
        else if (pokemon2.HealthPoint <= 0)
        {
            TypeWriterEffect($" {pokemon1.Nom} a gagné le combat et il s'est terminé en {tour} tours !");
        }
        Console.ResetColor();
    }

    static void TypeWriterEffect(string text, int delay = 40)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
        Console.WriteLine();
    }
}
