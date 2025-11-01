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


        // Initialisation des Pokémons
        Pokemon pikachu = new Pokemon("Pikachu", TypePokemon.Electrik, 40, 20, 5);
        Pokemon evoli = new Pokemon("Evoli", TypePokemon.Normal, 60, 15, 6);

        Console.ForegroundColor = ConsoleColor.White;
        TypeWriterEffect("Les combattants entrent dans l'arène...");
        Console.ResetColor();

        Thread.Sleep(1000); // Petite pause pour le suspense

        // Affichage des stats des Pokémons
        Console.ForegroundColor = ConsoleColor.Yellow;
        TypeWriterEffect($"⚡ {pikachu.Nom} - Type: {pikachu.Type}, PV: {pikachu.HealthPoint}, Attaque: {pikachu.Attack}, Défense: {pikachu.Defense}");
        Console.ResetColor();

        Thread.Sleep(500);

        Console.ForegroundColor = ConsoleColor.Gray;
        TypeWriterEffect($"🌟 {evoli.Nom} - Type: {evoli.Type}, PV: {evoli.HealthPoint}, Attaque: {evoli.Attack}, Défense: {evoli.Defense}");
        Console.ResetColor();

        Thread.Sleep(1000);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        TypeWriterEffect("\nQue le combat commence !");
        Console.ResetColor();


        int tour = 1;

        while (pikachu.HealthPoint > 0 && evoli.HealthPoint > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\n=== Tour {tour} de combat ===");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{pikachu.Nom}  PV: {pikachu.HealthPoint}");
            Console.WriteLine($"{evoli.Nom}  PV: {evoli.HealthPoint}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{pikachu.Nom} attaque !");
            pikachu.Attaquer(evoli, pikachu.Attack);
            Console.ResetColor();


            if (evoli.HealthPoint > 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{evoli.Nom} riposte!");
                evoli.Attaquer(pikachu, evoli.Attack);
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
        if (pikachu.HealthPoint <= 0)
        {
            TypeWriterEffect($" {evoli.Nom} a gagné le combat et il s'est terminé en {tour} tours !");
        }
        else if (evoli.HealthPoint <= 0)
        {
            TypeWriterEffect($" {pikachu.Nom} a gagné le combat et il s'est terminé en {tour} tours !");
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
