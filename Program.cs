using System;
using PokemonBattle;

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
        Console.ReadLine(); 
        Console.Clear();
    
        // Importation du pokedex 
        string filePath = "Pokedex.csv";
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
                Console.WriteLine($"{i} - {pokemons[i].Name}");
        }

        // Demande à l'utilisateur quel pokemon il veut utiliser 
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
                pokemon1 = pokemons[index];
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
            pokemon1 = pokemons.Find(p => p.Name.Equals(input, StringComparison.OrdinalIgnoreCase));
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

        Thread.Sleep(1000);

        // Affichage des stats
        Console.ForegroundColor = ConsoleColor.Yellow;
        TypeWriterEffect($" {pokemon1.Name} - Type: {pokemon1.Type}, PV: {pokemon1.HealthPoint}");
        Console.ResetColor();

        Thread.Sleep(500);

        Console.ForegroundColor = ConsoleColor.Gray;
        TypeWriterEffect($" {pokemon2.Name} - Type: {pokemon2.Type}, PV: {pokemon2.HealthPoint}");
        Console.ResetColor();

        Thread.Sleep(1000);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        TypeWriterEffect("\nQue le combat commence !");
        Console.ResetColor();
        Thread.Sleep(2000);

        int money = 1000;

        // Boutique accessible avant le combat
        List<IItem> shopItems = new List<IItem>()
        {
            new Pokeball(50),
            new Potion(25)
        };

        // Inventaire du joueur
        List<IItem> items = new List<IItem>();

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            TypeWriterEffect("\n=== 🛒 Boutique Pokémon ===");
            Console.ResetColor();

            TypeWriterEffect($"Argent disponible : {money} ₽\n");
            for (int i = 0; i < shopItems.Count; i++)
                TypeWriterEffect($"{i + 1}. {shopItems[i].Name} - {shopItems[i].Cost} ₽\n");

            TypeWriterEffect("0. Quitter la boutique\n");
            Console.Write("Votre choix : ");

            string? buyChoice = Console.ReadLine();

            if (buyChoice == "0")
                break;

            if (int.TryParse(buyChoice, out int shopIndex) && shopIndex >= 1 && shopIndex <= shopItems.Count)
            {
                IItem selected = shopItems[shopIndex - 1];

                if (money >= selected.Cost)
                {
                    money -= selected.Cost;
                    items.Add(selected);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✅ Achat réussi ! Vous obtenez : {selected.Name}");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Vous n'avez pas assez d'argent !");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("Choix invalide.");
            }
        }


        int tour = 1;

        // Boucle de combat principale
        while (pokemon1.HealthPoint > 0 && pokemon2.HealthPoint > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\n=== Tour {tour} de combat ===");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{pokemon1.Name}  PV: {pokemon1.HealthPoint}");
            Console.WriteLine($"{pokemon2.Name}  PV: {pokemon2.HealthPoint}");
            Console.ResetColor();

            // Menu de choix
            Console.ForegroundColor = ConsoleColor.Yellow;
            TypeWriterEffect("\nQue voulez-vous faire ?");
            Console.WriteLine("1️⃣  Attaquer");
            Console.WriteLine("2️⃣  Utiliser un objet");
            Console.WriteLine("3️⃣  Afficher l'inventaire");
            Console.WriteLine("4️⃣  Voir les PV de tous les Pokémons\n");
            TypeWriterEffect("Votre choix : ");
            Console.ResetColor();

            string? action = Console.ReadLine();

            if (action == "1")
            {
                // Attaque du joueur
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{pokemon1.Name} attaque !");
                Console.ResetColor();
                pokemon1.Attaquer(pokemon2, pokemon1.Attack);
            }
            else if (action == "2")
            {
                // Inventaire des objets
                TypeWriterEffect("\nObjets disponibles :");
                for (int i = 0; i < items.Count; i++)
                    Console.WriteLine($"{i + 1}. {items[i].Name}");
                Console.WriteLine("0. Retour au menu");

                TypeWriterEffect("\nChoisissez un objet : ");
                string? itemChoice = Console.ReadLine();

                    if (itemChoice == "0")
                {
                    // Retour au menu principal sans rien faire
                    continue; 
                }

                if (int.TryParse(itemChoice, out int itemIndex) && itemIndex >= 1 && itemIndex <= items.Count)
                {
                    var selectedItem = items[itemIndex - 1];

                    if (selectedItem is Potion)
                    {
                        // Potion soigne ton Pokémon
                        selectedItem.Use(pokemon1);
                    }
                    else if (selectedItem is Pokeball)
                    {
                        // Pokeball cible le Pokémon adverse
                        selectedItem.Use(pokemon2);

                        // Vérifier si le Pokémon a été capturé ou KO
                        if (pokemon2.HealthPoint <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            TypeWriterEffect("\n🎉 Le combat se termine !");
                            Console.ResetColor();
                            break;
                        }
                    }

                    // Retirer l'objet de l'inventaire après utilisation
                    items.RemoveAt(itemIndex - 1);
                }

            }
            else if (action == "3")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                TypeWriterEffect("\n=== 📦 Inventaire ===");
                Console.ResetColor();

                if (items.Count == 0)
                    TypeWriterEffect("Votre inventaire est vide !");
                else
                    for (int i = 0; i < items.Count; i++)
                        TypeWriterEffect($"{i + 1}. {items[i].Name} (Coût : {items[i].Cost})");

                TypeWriterEffect("\nAppuyez sur Entrée pour revenir au menu...");
                Console.ReadLine();

                Console.Clear();
                continue;
            }
            else if (action == "4")
            {
                // Affichage des PV de tous les Pokémons
                Console.ForegroundColor = ConsoleColor.Cyan;
                TypeWriterEffect("\n=== PV des Pokémons ===");
                Console.ResetColor();

                Console.WriteLine($"{pokemon1.Name} : {pokemon1.HealthPoint}/{pokemon1.MaxHealthPoint} PV");
                Console.WriteLine($"{pokemon2.Name} : {pokemon2.HealthPoint}/{pokemon2.MaxHealthPoint} PV");
                TypeWriterEffect("\nAppuyez sur Entrée pour revenir au menu...");
                Console.ReadLine();

                continue; // Retour au début du tour sans attaquer
            }
            else
            {
                Console.WriteLine("Choix invalide ! Attaque automatique !");
                pokemon1.Attaquer(pokemon2, pokemon1.Attack);
            }

            // Ennemi qui attaque à son tour s'il est toujours en vie
            if (pokemon2.HealthPoint > 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{pokemon2.Name} riposte !");
                Console.ResetColor();
                pokemon2.Attaquer(pokemon1, pokemon2.Attack);
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            TypeWriterEffect("\nAppuyez sur Entrée pour continuer...");
            Console.ResetColor();
            Console.ReadLine();

            tour++;
        }
        
        Console.ForegroundColor = ConsoleColor.Red;
        TypeWriterEffect("=== Fin du combat ===");
        Console.ResetColor();

        if (pokemon1.HealthPoint <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{pokemon2.Name} a gagné le combat !");
        }
        else if (pokemon2.HealthPoint <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{pokemon1.Name} a gagné le combat !");
        }
        Console.ResetColor();
    }

    // Effet machine à écrire
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