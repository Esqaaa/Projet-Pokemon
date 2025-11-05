using System;
using System.Collections.Generic;
using System.IO;
using PokemonBattle;

public static class PokemonLoader
{
    public static List<Pokemon> LoadFromCSV(string filePath)
    {
        var pokemons = new List<Pokemon>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"❌ Fichier introuvable : {filePath}");
            return pokemons;
        }

        using (var reader = new StreamReader(filePath))
        {
            bool headerSkipped = false;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (!headerSkipped)
                {
                    headerSkipped = true;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var values = line.Split(',');

                if (values.Length == 6)
                {
                    string name = values[0];

                    if (!Enum.TryParse<TypePokemon>(values[1], ignoreCase: true, out var type1))
                    {
                        Console.WriteLine($"❌ Type inconnu pour le Pokémon '{name}' : {values[1]}");
                        continue;
                    }

                    int pv = int.Parse(values[2]);
                    int attack = int.Parse(values[3]);
                    int defense = int.Parse(values[4]);
                    int speed = int.Parse(values[5]);

                    var pokemon = new Pokemon(name, type1, pv, attack, defense, speed);
                    pokemons.Add(pokemon);
                }
                else if (values.Length == 7)
                {
                    string name = values[0];

                    if (!Enum.TryParse<TypePokemon>(values[1], ignoreCase: true, out var type1))
                    {
                        Console.WriteLine($"❌ Type inconnu pour le Pokémon '{name}' : {values[1]}");
                        continue;
                    }

                    // Ignore le deuxième type pour rester compatible avec ta classe actuelle
                    int pv = int.Parse(values[3]);
                    int attack = int.Parse(values[4]);
                    int defense = int.Parse(values[5]);
                    int speed = int.Parse(values[6]);

                    var pokemon = new Pokemon(name, type1, pv, attack, defense, speed);
                    pokemons.Add(pokemon);
                }
                else
                {
                    Console.WriteLine($"❌ Ligne ignorée (format invalide) : {line}");
                }
            }
        }

        return pokemons;
    }
}