using System;
using System.Collections.Generic;
using System.IO;
using PokemonBattle;

public static class PokemonLoader
{
    // Charge les pokémons depuis un fichier CSV
    public static List<Pokemon> LoadFromCSV(string filePath)
    {
        var pokemons = new List<Pokemon>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"❌ Fichier introuvable : {filePath}");
            return pokemons;
        }

        // Lecture du fichier CSV
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

                    // Ignore le deuxième type pour rester compatible avec la classe actuelle
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

    /// <summary>
    /// Assigne les attaques par défaut à un Pokémon en fonction de son type.
    /// </summary>
    private static void AddDefaultAttacksByType(Pokemon p)
    {
        switch (p.Type1)
        {
            case TypePokemon.Feu:
                p.Attacks.Add(new DamageAttack("Flammèche", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Lance-Flammes", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Cendres chaudes", p.Type1, 15));
                break;

            case TypePokemon.Eau:
                p.Attacks.Add(new DamageAttack("Pistolet à O", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Cascade", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Pluie régénérante", p.Type1, 15));
                break;

            case TypePokemon.Plante:
                p.Attacks.Add(new DamageAttack("Fouet Lianes", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Tempête Verte", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Synthèse", p.Type1, 15));
                break;

            case TypePokemon.Electrik:
                p.Attacks.Add(new DamageAttack("Éclair", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Fatal-Foudre", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Recharge électrique", p.Type1, 15));
                break;

            case TypePokemon.Acier:
                p.Attacks.Add(new DamageAttack("Griffe Acier", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Tir Métallique", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Renfort Acier", p.Type1, 15));
                break;

            case TypePokemon.Combat:
                p.Attacks.Add(new DamageAttack("Poing Éclair", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Coup de Boule", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Focus Vital", p.Type1, 15));
                break;

            case TypePokemon.Dragon:
                p.Attacks.Add(new DamageAttack("Draco-Rage", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Dracochoc", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Souffle Draconique", p.Type1, 15));
                break;

            case TypePokemon.Fee:
                p.Attacks.Add(new DamageAttack("Éclat Magique", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Voile Féérique", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Soin Enchanté", p.Type1, 15));
                break;

            case TypePokemon.Glace:
                p.Attacks.Add(new DamageAttack("Poudreuse", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Blizzard", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Frissons Revigorants", p.Type1, 15));
                break;

            case TypePokemon.Insecte:
                p.Attacks.Add(new DamageAttack("Dard-Venin", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Fouet Insecte", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Régénération Naturelle", p.Type1, 15));
                break;

            case TypePokemon.Normal:
                p.Attacks.Add(new DamageAttack("Charge", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Coup Puissant", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Repos", p.Type1, 15));
                break;

            case TypePokemon.Poison:
                p.Attacks.Add(new DamageAttack("Piqûre Toxique", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Nuage Vénéneux", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Soin Acide", p.Type1, 15));
                break;

            case TypePokemon.Psy:
                p.Attacks.Add(new DamageAttack("Choc Mental", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Psyko", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Calme Mental", p.Type1, 15));
                break;

            case TypePokemon.Roche:
                p.Attacks.Add(new DamageAttack("Éboulement", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Pierre-Volante", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Soin Sablonneux", p.Type1, 15));
                break;

            case TypePokemon.Sol:
                p.Attacks.Add(new DamageAttack("Pelle", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Séisme", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Régénération Terrestre", p.Type1, 15));
                break;

            case TypePokemon.Spectre:
                p.Attacks.Add(new DamageAttack("Griffe Spectrale", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Ombre Nocturne", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Soin Fantomatique", p.Type1, 15));
                break;

            case TypePokemon.Tenebres:
                p.Attacks.Add(new DamageAttack("Morsure", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Griffe Ombre", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Soin Obscur", p.Type1, 15));
                break;

            case TypePokemon.Vol:
                p.Attacks.Add(new DamageAttack("Aile d’Acier", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Rapace", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Rafale Revigorante", p.Type1, 15));
                break;

            default:
                p.Attacks.Add(new DamageAttack("Coup Normal", p.Type1, 20));
                p.Attacks.Add(new DamageAttack("Coup Puissant", p.Type1, 35));
                p.Attacks.Add(new HealingAttack("Repos", p.Type1, 15));
                break;
        }
    }
}
