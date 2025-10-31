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

        

        Pokemon pikachu = new Pokemon("Pikachu", TypePokemon.Electrik, 40, 70, 5);
        Pokemon evoli = new Pokemon("Evoli", TypePokemon.Normal, 60, 15, 6);

        int degatsPikachu = 15;
        int degatsEvoli = 8;
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
            Console.WriteLine($"{pikachu.Nom} attaque {evoli.Nom} et inflige {degatsPikachu} dégâts !");
            pikachu.Attaquer(evoli, degatsPikachu);
            Console.ResetColor();


            if (evoli.HealthPoint > 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{evoli.Nom} riposte et inflige {degatsEvoli} dégâts à {pikachu.Nom} !");
                evoli.Attaquer(pikachu, degatsEvoli);
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
            TypeWriterEffect($" {evoli.Nom} a gagné le combat !");
        }
        else if (evoli.HealthPoint <= 0)
        {
            TypeWriterEffect($" {pikachu.Nom} a gagné le combat !");
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
