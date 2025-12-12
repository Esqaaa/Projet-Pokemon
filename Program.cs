using System;
using PokemonBattle;
using System.Collections.Generic;
using System.Threading;

class Program
{
    // Couleurs en fonction du type de pokemon
    static ConsoleColor GetTypeColor(TypePokemon type)
    {
        return type switch
        {
            TypePokemon.Feu => ConsoleColor.Red,
            TypePokemon.Eau => ConsoleColor.Blue,
            TypePokemon.Plante => ConsoleColor.Green,
            TypePokemon.Electrik => ConsoleColor.Yellow,
            TypePokemon.Glace => ConsoleColor.Cyan,
            TypePokemon.Acier => ConsoleColor.Gray,
            TypePokemon.Combat => ConsoleColor.DarkRed,
            TypePokemon.Dragon => ConsoleColor.DarkMagenta,
            TypePokemon.Fee => ConsoleColor.Magenta,
            TypePokemon.Insecte => ConsoleColor.DarkGreen,
            TypePokemon.Normal => ConsoleColor.White,
            TypePokemon.Poison => ConsoleColor.DarkMagenta,
            TypePokemon.Psy => ConsoleColor.DarkCyan,
            TypePokemon.Roche => ConsoleColor.DarkYellow,
            TypePokemon.Sol => ConsoleColor.DarkYellow,
            TypePokemon.Spectre => ConsoleColor.DarkMagenta,
            TypePokemon.Tenebres => ConsoleColor.DarkGray,
            TypePokemon.Vol => ConsoleColor.Cyan,
            _ => ConsoleColor.White
        };
    }

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

        // Affichage du pokédex si demandé
        Console.WriteLine("\n📜 Accéder au pokédex (y/n) : ");
        string? choice = Console.ReadLine();
        if (choice != null && choice.ToLower() == "y")
        {
            Console.Clear();
            Console.WriteLine("\nListe des Pokémon disponibles :");
            DisplayPokedexColumn(pokemons);
        }

        // Sélection de l'équipe du joueur 
        List<Pokemon> playerTeam = new List<Pokemon>();
        while (playerTeam.Count < 6)
        {
            Console.WriteLine($"\nChoisissez votre Pokémon {playerTeam.Count + 1} (nom ou numéro) : ");
            string? inputTeam = Console.ReadLine();
            Pokemon? selected = null;

            if (int.TryParse(inputTeam, out int indexTeam) && indexTeam >= 0 && indexTeam < pokemons.Count)
            {
                selected = new Pokemon(pokemons[indexTeam]); // copie
            }
            else
            {
                var p = pokemons.Find(pkm => pkm.Name.Equals(inputTeam, StringComparison.OrdinalIgnoreCase));
                if (p != null) selected = new Pokemon(p); // copie
            }

            
            if (selected != null)
            {
                // Vérifier si le Pokémon est déjà dans l'équipe
                if (playerTeam.Any(p => p.Name.Equals(selected.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Vous avez déjà choisi ce Pokémon, réessayez !");
                    Console.ResetColor();
                    continue;
                }

                playerTeam.Add(selected);
                Console.Clear();
                Console.ForegroundColor = GetTypeColor(selected.Type);
                Console.WriteLine($"✅ {selected.Name} ajouté à votre équipe !");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Pokémon invalide, réessayez !");
                Console.ResetColor();
            }
        }

        // Sélection aléatoire de l'équipe ennemie 
        List<Pokemon> enemyTeam = new List<Pokemon>();
        Random rnd = new Random();
        while (enemyTeam.Count < 6)
        {
            Pokemon randomPokemon = new Pokemon(pokemons[rnd.Next(pokemons.Count)]);
            enemyTeam.Add(randomPokemon);
        }

        // Définir les Pokémon actifs pour le combat
        Pokemon playerActive = playerTeam[0];
        Pokemon enemyActive = enemyTeam[0];

        Console.ForegroundColor = ConsoleColor.White;
        TypeWriterEffect("\nLes combattants entrent dans l'arène...");
        Console.ResetColor();
        Thread.Sleep(1000);

        // Affichage initial
        DisplayActivePokemon(playerActive, "Votre Pokémon");
        DisplayActivePokemon(enemyActive, "\nAdversaire");

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
        while (playerTeam.Exists(p => p.HealthPoint > 0) && enemyTeam.Exists(p => p.HealthPoint > 0))
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\n=== Tour {tour} de combat ===");
            Console.ResetColor();

            DisplayActivePokemon(playerActive, "Votre Pokémon");
            DisplayActivePokemon(enemyActive, "Adversaire");

            // Menu de choix
            Console.ForegroundColor = ConsoleColor.Yellow;
            TypeWriterEffect("\nQue voulez-vous faire ?");
            Console.WriteLine("1️⃣  Attaquer");
            Console.WriteLine("2️⃣  Utiliser un objet");
            Console.WriteLine("3️⃣  Afficher l'inventaire");
            Console.WriteLine("4️⃣  Changer de Pokémon");
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
                playerActive.DisplayAttacks();

                Console.WriteLine("0. Retour au menu");
                TypeWriterEffect("\nChoisissez une attaque : ");
                string? attackChoice = Console.ReadLine();

                if (attackChoice == "0")
                    continue;

                if (int.TryParse(attackChoice, out int attackIndex) && attackIndex >= 1 && attackIndex <= playerActive.Attacks.Count)
                {
                    AnimateAttack(playerActive, enemyActive);
                    playerActive.UseAttack(attackIndex - 1, enemyActive);
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
                    continue;
                }

                TypeWriterEffect("\nObjets disponibles :");
                for (int i = 0; i < items.Count; i++)
                    Console.WriteLine($"{i + 1}. {items[i].Name}");
                Console.WriteLine("0. Retour au menu");

                TypeWriterEffect("\nChoisissez un objet : ");
                string? itemChoice = Console.ReadLine();

                if (itemChoice == "0") continue;

                if (int.TryParse(itemChoice, out int itemIndex) && itemIndex >= 1 && itemIndex <= items.Count)
                {
                    var selectedItem = items[itemIndex - 1];

                    if (selectedItem is Ventoline)
                        selectedItem.Use(playerActive);
                    else if (selectedItem is Pokeboule)
                    {
                        selectedItem.Use(enemyActive);
                        if (enemyActive.HealthPoint <= 0)
                        {
                            TypeWriterEffect("\n🎉 Le combat se termine !");
                            break;
                        }
                    }
                    items.RemoveAt(itemIndex - 1);
                }
            }
            // Afficher l'inventaire 
            else if (action == "3")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                TypeWriterEffect("\n=== 📦 Inventaire ===");
                Console.ResetColor();

                if (items.Count == 0) TypeWriterEffect("Votre inventaire est vide !");
                else
                    for (int i = 0; i < items.Count; i++)
                        TypeWriterEffect($"{i + 1}. {items[i].Name} (Coût : {items[i].Cost})");

                TypeWriterEffect("\nAppuyez sur Entrée pour revenir au menu...");
                Console.ReadLine();
                continue;
            }
            // Changer de Pokémon 
            else if (action == "4")
            {
                Console.WriteLine("\nChoisissez un Pokémon de votre équipe :");
                for (int i = 0; i < playerTeam.Count; i++)
                    Console.WriteLine($"{i + 1}. {playerTeam[i].Name} ({playerTeam[i].HealthPoint}/{playerTeam[i].MaxHealthPoint} PV)");

                string? swapChoice = Console.ReadLine();
                if (int.TryParse(swapChoice, out int swapIndex) && swapIndex >= 1 && swapIndex <= playerTeam.Count && playerTeam[swapIndex - 1].HealthPoint > 0)
                    playerActive = playerTeam[swapIndex - 1];
                else
                    TypeWriterEffect("Choix invalide !");
                continue;
            }
            // Boutique 
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

                    if (buyChoice == "0") break;

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
                }
                continue;
            }
            else
            {
                Console.WriteLine("Choix invalide !");
                continue;
            }

            // Attaque de l'ennemi 
            if (enemyActive.HealthPoint > 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{enemyActive.Name} riposte !");
                Console.ResetColor();

                if (enemyActive.Attacks.Count > 0)
                {
                    int randomIndex = rnd.Next(enemyActive.Attacks.Count);
                    AnimateAttack(enemyActive, playerActive);
                    enemyActive.UseAttack(randomIndex, playerActive);
                }
            }

            // Changer Pokémon K.O si nécessaire 
            if (playerActive.HealthPoint <= 0 && playerTeam.Exists(p => p.HealthPoint > 0))
            {
                TypeWriterEffect($"\n💀 {playerActive.Name} est K.O ! Choisissez un autre Pokémon :");
                for (int i = 0; i < playerTeam.Count; i++)
                    if (playerTeam[i].HealthPoint > 0)
                        Console.WriteLine($"{i + 1}. {playerTeam[i].Name} ({playerTeam[i].HealthPoint}/{playerTeam[i].MaxHealthPoint} PV)");

                while (true)
                {
                    string? choiceSwap = Console.ReadLine();
                    if (int.TryParse(choiceSwap, out int idx) && idx >= 1 && idx <= playerTeam.Count && playerTeam[idx - 1].HealthPoint > 0)
                    {
                        playerActive = playerTeam[idx - 1];
                        break;
                    }
                    else
                        TypeWriterEffect("Choix invalide !");
                }
            }

            if (enemyActive.HealthPoint <= 0 && enemyTeam.Exists(p => p.HealthPoint > 0))
            {
                enemyActive = enemyTeam.Find(p => p.HealthPoint > 0)!;
                TypeWriterEffect($"\n💀 L'adversaire envoie {enemyActive.Name} !");
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            TypeWriterEffect("\nAppuyez sur Entrée pour continuer...");
            Console.ResetColor();
            Console.ReadLine();

            tour++;
        }

        // Fin du combat 
        Console.ForegroundColor = ConsoleColor.Red;
        TypeWriterEffect("\n=== Fin du combat ===");
        Console.ResetColor();

        if (playerTeam.All(p => p.HealthPoint <= 0))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("L'adversaire a gagné le combat !");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Vous avez gagné le combat !");
        }
        Console.ResetColor();
    }

    // Effet machine à écrire
    static void TypeWriterEffect(string text, int delay = 5)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
        Console.WriteLine();
    }

    // Affiche la barre de vie
    static string HealthBar(int current, int max)
    {
        int size = 20;
        int filled = (current * size) / max;
        return "|" + new string('█', filled) + new string('░', size - filled) + "|";
    }

    // Affiche un Pokémon actif avec mini-sprite
    static void DisplayActivePokemon(Pokemon p, string title)
    {
        Console.ForegroundColor = GetTypeColor(p.Type);
        TypeWriterEffect($"{title}: {p.Name.PadRight(15)} {HealthBar(p.HealthPoint, p.MaxHealthPoint)} {p.HealthPoint}/{p.MaxHealthPoint}");
        Console.WriteLine(GetMiniSprite(p.Type));
        Console.ResetColor();
    }

    // Mini sprites
    static string GetMiniSprite(TypePokemon type)
    {
        return type switch
        {
            TypePokemon.Feu =>
    @"    (\_/)
    ( •_•)🔥
    / >🔥",

            TypePokemon.Eau =>
    @"    (\_/)
    ( •_•)💧
    / >💦",

            TypePokemon.Plante =>
    @"    (\_/)
    ( •_•)🌿
    / >🌱",

            TypePokemon.Electrik =>
    @"    (\_/)
    ( •_•)⚡
    / >⚡",

            TypePokemon.Glace =>
    @" (\_/)
    ( •_•)❄️
    / >☃️",

            TypePokemon.Acier =>
    @"   (\_/)
    ( •_•)⚙️
    / >🔧",

            TypePokemon.Combat =>
    @"   (\_/)
    ( •_•)👊
    / >🥊",

            TypePokemon.Dragon =>
    @"     /^ ^\
    ( •.• )
    / >🐉",

            TypePokemon.Spectre =>
    @"    .-.
    ( •_•)
    /)   )",

            TypePokemon.Tenebres =>
    @"    (\_/)
    ( •_•)🌑
    / >🌘",

            TypePokemon.Poison =>
    @"    (\_/)
    ( x_x)☠️
    /  >☣️",

            TypePokemon.Psy =>
    @"    (\_/)
    ( •_•)🔮
    / >✨",

            TypePokemon.Insecte =>
    @"     .--.
    ( •.•)
    /  🐛",

            TypePokemon.Vol =>
    @"    /\_/\
    ( •_• )🕊️
    \   \",

            TypePokemon.Roche =>
    @"     _____
    ( •_• )
    /  🪨 \",

            TypePokemon.Sol =>
    @"    (\_/)
    ( •_•)⛰️
    /  >🏔️",

            TypePokemon.Fee =>
    @" (\_/)
    ( ^_^)✨
    / >✨",

            _ =>
    @"   (\_/)
    ( •_•)
    /  >"
        };
    }

    // Affichage du pokédex en colonnes
    static void DisplayPokedexColumn(List<Pokemon> pokemons)
    {
        int columns = 3;
        int rows = (int)Math.Ceiling((double)pokemons.Count / columns);

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                int index = r + c * rows;
                if (index < pokemons.Count)
                {
                    Console.Write($"{index} - {pokemons[index].Name}".PadRight(30));
                    Console.ResetColor();
                }
            }
            Console.WriteLine();
        }
    }

    // Effet simple animation d'attaque
    static void AnimateAttack(Pokemon attacker, Pokemon defender)
    {
        Console.ForegroundColor = GetTypeColor(attacker.Type);
        Console.WriteLine($"\n{attacker.Name} attaque {defender.Name} !");
        Thread.Sleep(400);
        Console.WriteLine("⚡💥✨");
        Thread.Sleep(400);
        Console.ResetColor();
    }
}