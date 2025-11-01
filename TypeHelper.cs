using System;
using System.Collections.Generic;

namespace PokemonBattle
{
    public static class TypeHelper
    {
        private static readonly Dictionary<TypePokemon, Dictionary<TypePokemon, double>> typeChart =
            new()
            {
                {
                    TypePokemon.Feu, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Plante, 2.0 },
                        { TypePokemon.Eau, 0.5 },
                        { TypePokemon.Glace, 2.0 },
                        { TypePokemon.Roche, 0.5 }
                    }
                },
                {
                    TypePokemon.Eau, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Feu, 2.0 },
                        { TypePokemon.Plante, 0.5 },
                        { TypePokemon.Roche, 2.0 }
                    }
                },
                {
                    TypePokemon.Plante, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Eau, 2.0 },
                        { TypePokemon.Feu, 0.5 },
                        { TypePokemon.Roche, 2.0 }
                    }
                },
                {
                    TypePokemon.Electrik, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Eau, 2.0 },
                        { TypePokemon.Plante, 0.5 },
                        { TypePokemon.Sol, 0.0 }
                    }
                },
                {
                    TypePokemon.Normal, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Roche, 0.5 },
                        { TypePokemon.Spectre, 0.0 }
                    }
                }
            };

        public static double GetEffectiveness(TypePokemon attackerType, TypePokemon defenderType)
        {
            if (typeChart.TryGetValue(attackerType, out var relations) &&
                relations.TryGetValue(defenderType, out var multiplier))
            {
                return multiplier;
            }

            return 1.0; // dégâts normaux par défaut
        }

        public static string GetEffectivenessMessage(double multiplier)
        {
            return multiplier switch
            {
                2.0 => "C’est super efficace !",
                0.5 => "Ce n’est pas très efficace...",
                0.0 => "Aucun effet...",
                _ => ""
            };
        }
    }
}