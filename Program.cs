using System;
using PokemonBattle;

class Program
{
    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        TypeWriterEffect("⚔️ Bienvenue dans la Console de Combat Pokémon !");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        TypeWriterEffect("\nAppuyez sur Entrée pour commencer le combat ...");
        Thread.Sleep(1000);
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


        Console.ForegroundColor = ConsoleColor.Gray;
        TypeWriterEffect($" {pokemon2.Name} - Type: {pokemon2.Type}, PV: {pokemon2.HealthPoint}");
        Console.ResetColor();

        Thread.Sleep(500);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        TypeWriterEffect("\nQue le combat commence !");
        Console.ResetColor();

        // Définir argent et boutique, inventaire
        int money = 1000;

        List<IItem> shopItems = new List<IItem>()
        {
            new Pokeboule(50),
            new Ventoline(25)
        };

        List<IItem> items = new List<IItem>();

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
            Console.WriteLine("4️⃣  Voir les PV de tous les Pokémons");
            Console.WriteLine("5️⃣  Boutique\n");
            TypeWriterEffect("Votre choix : ");
            Console.ResetColor();

            string? action = Console.ReadLine();

            // Attaquer
            if (action == "1")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                TypeWriterEffect("\nAttaques disponibles :");
                Console.ResetColor();

                pokemon1.DisplayAttacks();

                Console.WriteLine("0. Retour au menu");
                TypeWriterEffect("\nChoisissez une attaque : ");
                string? attackChoice = Console.ReadLine();

                if (attackChoice == "0")
                    continue;

                if (int.TryParse(attackChoice, out int attackIndex) && attackIndex >= 1 && attackIndex <= pokemon1.Attacks.Count)
                {
                    pokemon1.UseAttack(attackIndex - 1, pokemon2);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Choix d'attaque invalide !");
                    Console.ResetColor();
                    continue;
                }
            }
            // Utiliser un objet
            else if (action == "2")
            {
                if (items.Count == 0)
                {
                    TypeWriterEffect("\nVotre inventaire est vide !");
                    Console.Clear();
                    continue;
                }

                TypeWriterEffect("\nObjets disponibles :");
                for (int i = 0; i < items.Count; i++)
                    Console.WriteLine($"{i + 1}. {items[i].Name}");
                Console.WriteLine("0. Retour au menu");

                TypeWriterEffect("\nChoisissez un objet : ");
                string? itemChoice = Console.ReadLine();

                if (itemChoice == "0")
                    continue;

                if (int.TryParse(itemChoice, out int itemIndex) && itemIndex >= 1 && itemIndex <= items.Count)
                {
                    var selectedItem = items[itemIndex - 1];

                    if (selectedItem is Ventoline)
                        selectedItem.Use(pokemon1);
                    else if (selectedItem is Pokeboule)
                    {
                        selectedItem.Use(pokemon2);
                        if (pokemon2.HealthPoint <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            TypeWriterEffect("\n🎉 Le combat se termine !");
                            Console.ResetColor();
                            break;
                        }
                    }

                    items.RemoveAt(itemIndex - 1);
                }
                else
                {
                    Console.WriteLine("Choix invalide !");
                }
            }
            // Inventaire
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
                continue;
            }
            // Voir les PV
            else if (action == "4")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                TypeWriterEffect("\nPV des Pokémons");
                Console.ResetColor();

                Console.WriteLine($"{pokemon1.Name} : {pokemon1.HealthPoint}/{pokemon1.MaxHealthPoint} PV");
                Console.WriteLine($"{pokemon2.Name} : {pokemon2.HealthPoint}/{pokemon2.MaxHealthPoint} PV");

                TypeWriterEffect("\nAppuyez sur Entrée pour revenir au menu...");
                Console.ReadLine();
                continue;
            }
            // La boutique
            else if (action == "5")
            {
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
                    Console.Clear();

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

                continue;
            }
            // Si le choix est invalide
            else
            {
                Console.WriteLine("Choix invalide ! Fais attention !");
                continue;
            }

            // Attaque de l'ennemi 
            if (pokemon2.HealthPoint > 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{pokemon2.Name} riposte !");
                Console.ResetColor();

                // Vérifier que le Pokémon ennemi a des attaques
                if (pokemon2.Attacks.Count > 0)
                {
                    Random rndAttack = new Random();
                    int randomIndex = rndAttack.Next(pokemon2.Attacks.Count); // Choisit un index aléatoire
                    pokemon2.UseAttack(randomIndex, pokemon1);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{pokemon2.Name} n'a pas d'attaques disponibles !");
                    Console.ResetColor();
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            TypeWriterEffect("\nAppuyez sur Entrée pour continuer...");
            Console.ResetColor();
            Console.ReadLine();

            tour++;
        }

        // Fin du combat
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
