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
                    AddDefaultAttacksByType(pokemon); 
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
                    AddDefaultAttacksByType(pokemon); 
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

    // Ajoute des attaques par défaut en fonction du type du Pokémon
    private static void AddDefaultAttacksByType(Pokemon p)
    {
        switch (p.Type)
        {
            // Ajout pour le type Feu
            case TypePokemon.Feu:
                p.AddAttack(new DamageAttack("Flammèche", 20, TypePokemon.Feu));
                p.AddAttack(new DamageAttack("Lance-Flammes", 35, TypePokemon.Feu));
                p.AddAttack(new HealingAttack("Cendres chaudes", 15, TypePokemon.Feu));
                break;

            // Ajout pour le type Eau
            case TypePokemon.Eau:
                p.AddAttack(new DamageAttack("Pistolet à O", 20, TypePokemon.Eau));
                p.AddAttack(new DamageAttack("Cascade", 35, TypePokemon.Eau));
                p.AddAttack(new HealingAttack("Pluie régénérante", 15, TypePokemon.Eau));
                break;

            // Ajout pour le type Plante
            case TypePokemon.Plante:
                p.AddAttack(new DamageAttack("Fouet Lianes", 20, TypePokemon.Plante));
                p.AddAttack(new DamageAttack("Tempête Verte", 35, TypePokemon.Plante));
                p.AddAttack(new HealingAttack("Synthèse", 15, TypePokemon.Plante));
                break;

            // Ajout pour le type Electrik
            case TypePokemon.Electrik:
                p.AddAttack(new DamageAttack("Éclair", 20, TypePokemon.Electrik));
                p.AddAttack(new DamageAttack("Fatal-Foudre", 35, TypePokemon.Electrik));
                p.AddAttack(new HealingAttack("Recharge électrique", 15, TypePokemon.Electrik));
                break;

            // Ajout pour le type Glace
            case TypePokemon.Acier:
                p.AddAttack(new DamageAttack("Griffe Acier", 20, TypePokemon.Acier));
                p.AddAttack(new DamageAttack("Tir Métallique", 35, TypePokemon.Acier));
                p.AddAttack(new HealingAttack("Renfort Acier", 15, TypePokemon.Acier));
                break;

            // Ajout pour le type Combat
            case TypePokemon.Combat:
                p.AddAttack(new DamageAttack("Poing Éclair", 20, TypePokemon.Combat));
                p.AddAttack(new DamageAttack("Coup de Boule", 35, TypePokemon.Combat));
                p.AddAttack(new HealingAttack("Focus Vital", 15, TypePokemon.Combat));
                break;

            // Ajout pour le type Dragon
            case TypePokemon.Dragon:
                p.AddAttack(new DamageAttack("Draco-Rage", 20, TypePokemon.Dragon));
                p.AddAttack(new DamageAttack("Dracochoc", 35, TypePokemon.Dragon));
                p.AddAttack(new HealingAttack("Souffle Draconique", 15, TypePokemon.Dragon));
                break;

            // Ajout pour le type Eau
            case TypePokemon.Fee:
                p.AddAttack(new DamageAttack("Éclat Magique", 20, TypePokemon.Fee));
                p.AddAttack(new DamageAttack("Voile Féérique", 35, TypePokemon.Fee));
                p.AddAttack(new HealingAttack("Soin Enchanté", 15, TypePokemon.Fee));
                break;

            // Ajout pour le type Glace
            case TypePokemon.Glace:
                p.AddAttack(new DamageAttack("Poudreuse", 20, TypePokemon.Glace));
                p.AddAttack(new DamageAttack("Blizzard", 35, TypePokemon.Glace));
                p.AddAttack(new HealingAttack("Frissons Revigorants", 15, TypePokemon.Glace));
                break;
            
            // Ajout pour le type Insecte
            case TypePokemon.Insecte:
                p.AddAttack(new DamageAttack("Dard-Venin", 20, TypePokemon.Insecte));
                p.AddAttack(new DamageAttack("Fouet Insecte", 35, TypePokemon.Insecte));
                p.AddAttack(new HealingAttack("Régénération Naturelle", 15, TypePokemon.Insecte));
                break;

            // Ajout pour le type Normal
            case TypePokemon.Normal:
                p.AddAttack(new DamageAttack("Charge", 20, TypePokemon.Normal));
                p.AddAttack(new DamageAttack("Coup Puissant", 35, TypePokemon.Normal));
                p.AddAttack(new HealingAttack("Repos", 15, TypePokemon.Normal));
                break;

            // Ajout pour le type Poison
            case TypePokemon.Poison:
                p.AddAttack(new DamageAttack("Piqûre Toxique", 20, TypePokemon.Poison));
                p.AddAttack(new DamageAttack("Nuage Vénéneux", 35, TypePokemon.Poison));
                p.AddAttack(new HealingAttack("Soin Acide", 15, TypePokemon.Poison));
                break;

            // Ajout pour le type Psy
            case TypePokemon.Psy:
                p.AddAttack(new DamageAttack("Choc Mental", 20, TypePokemon.Psy));
                p.AddAttack(new DamageAttack("Psyko", 35, TypePokemon.Psy));
                p.AddAttack(new HealingAttack("Calme Mental", 15, TypePokemon.Psy));
                break;

            // Ajout pour le type Roche
            case TypePokemon.Roche:
                p.AddAttack(new DamageAttack("Éboulement", 20, TypePokemon.Roche));
                p.AddAttack(new DamageAttack("Pierre-Volante", 35, TypePokemon.Roche));
                p.AddAttack(new HealingAttack("Soin Sablonneux", 15, TypePokemon.Roche));
                break;

            // Ajout pour le type Sol
            case TypePokemon.Sol:
                p.AddAttack(new DamageAttack("Pelle", 20, TypePokemon.Sol));
                p.AddAttack(new DamageAttack("Séisme", 35, TypePokemon.Sol));
                p.AddAttack(new HealingAttack("Régénération Terrestre", 15, TypePokemon.Sol));
                break;

            // Ajout pour le type Spectre
            case TypePokemon.Spectre:
                p.AddAttack(new DamageAttack("Griffe Spectrale", 20, TypePokemon.Spectre));
                p.AddAttack(new DamageAttack("Ombre Nocturne", 35, TypePokemon.Spectre));
                p.AddAttack(new HealingAttack("Soin Fantomatique", 15, TypePokemon.Spectre));
                break;

            // Ajout pour le type Ténèbres
            case TypePokemon.Tenebres:
                p.AddAttack(new DamageAttack("Morsure", 20, TypePokemon.Tenebres));
                p.AddAttack(new DamageAttack("Griffe Ombre", 35, TypePokemon.Tenebres));
                p.AddAttack(new HealingAttack("Soin Obscur", 15, TypePokemon.Tenebres));
                break;

            // Ajout pour le type Vol
            case TypePokemon.Vol:
                p.AddAttack(new DamageAttack("Aile d'Acier", 20, TypePokemon.Vol));
                p.AddAttack(new DamageAttack("Rapace", 35, TypePokemon.Vol));
                p.AddAttack(new HealingAttack("Rafale Revigorante", 15, TypePokemon.Vol));
                break;

            // Attaques par défaut pour les autres
            default:
                p.AddAttack(new DamageAttack("Coup Normal", 20, TypePokemon.Normal));
                p.AddAttack(new DamageAttack("Coup Puissant", 35, TypePokemon.Normal));
                p.AddAttack(new HealingAttack("Repos", 15, TypePokemon.Normal));
                break;
        }
    }
}