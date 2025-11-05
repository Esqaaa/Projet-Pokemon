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
                    TypePokemon.Normal, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Roche, 0.5 },
                        { TypePokemon.Acier, 0.5 },
                        { TypePokemon.Spectre, 0.0 }
                    }
                },
                {
                    TypePokemon.Feu, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Feu, 0.5 },
                        { TypePokemon.Eau, 0.5 },
                        { TypePokemon.Plante, 2.0 },
                        { TypePokemon.Glace, 2.0 },
                        { TypePokemon.Insecte, 2.0 },
                        { TypePokemon.Roche, 0.5 },
                        { TypePokemon.Dragon, 0.5 },
                        { TypePokemon.Acier, 2.0 },
                        { TypePokemon.Fee, 1.0 }
                    }
                },
                {
                    TypePokemon.Eau, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Feu, 2.0 },
                        { TypePokemon.Eau, 0.5 },
                        { TypePokemon.Plante, 0.5 },
                        { TypePokemon.Sol, 2.0 },
                        { TypePokemon.Roche, 2.0 },
                        { TypePokemon.Dragon, 0.5 },
                        { TypePokemon.Acier, 1.0 },
                        { TypePokemon.Fee, 1.0 }
                    }
                },
                {
                    TypePokemon.Plante, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Feu, 0.5 },
                        { TypePokemon.Eau, 2.0 },
                        { TypePokemon.Plante, 0.5 },
                        { TypePokemon.Electrik, 0.5 },
                        { TypePokemon.Glace, 1.0 },
                        { TypePokemon.Poison, 0.5 },
                        { TypePokemon.Sol, 2.0 },
                        { TypePokemon.Vol, 0.5 },
                        { TypePokemon.Insecte, 0.5 },
                        { TypePokemon.Roche, 2.0 },
                        { TypePokemon.Dragon, 0.5 },
                        { TypePokemon.Acier, 0.5 },
                        { TypePokemon.Fee, 1.0 }
                    }
                },
                {
                    TypePokemon.Electrik, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Eau, 2.0 },
                        { TypePokemon.Plante, 0.5 },
                        { TypePokemon.Electrik, 0.5 },
                        { TypePokemon.Vol, 2.0 },
                        { TypePokemon.Dragon, 0.5 },
                        { TypePokemon.Acier, 1.0 },
                        { TypePokemon.Fee, 1.0 }
                    }
                },
                {
                    TypePokemon.Glace, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Feu, 0.5 },
                        { TypePokemon.Eau, 0.5 },
                        { TypePokemon.Plante, 2.0 },
                        { TypePokemon.Sol, 2.0 },
                        { TypePokemon.Vol, 2.0 },
                        { TypePokemon.Dragon, 2.0 },
                        { TypePokemon.Acier, 0.5 },
                        { TypePokemon.Fee, 1.0 }
                    }
                },
                {
                    TypePokemon.Combat, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Normal, 2.0 },
                        { TypePokemon.Glace, 2.0 },
                        { TypePokemon.Poison, 0.5 },
                        { TypePokemon.Vol, 0.5 },
                        { TypePokemon.Psy, 0.5 },
                        { TypePokemon.Insecte, 0.5 },
                        { TypePokemon.Roche, 2.0 },
                        { TypePokemon.Spectre, 0.0 },
                        { TypePokemon.Tenebres, 2.0 },
                        { TypePokemon.Acier, 2.0 },
                        { TypePokemon.Fee, 0.5 }
                    }
                },
                {
                    TypePokemon.Poison, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Plante, 2.0 },
                        { TypePokemon.Poison, 0.5 },
                        { TypePokemon.Sol, 0.5 },
                        { TypePokemon.Roche, 0.5 },
                        { TypePokemon.Spectre, 0.5 },
                        { TypePokemon.Acier, 0.0 },
                        { TypePokemon.Fee, 2.0 }
                    }
                },
                {
                    TypePokemon.Sol, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Feu, 2.0 },
                        { TypePokemon.Electrik, 2.0 },
                        { TypePokemon.Poison, 2.0 },
                        { TypePokemon.Roche, 2.0 },
                        { TypePokemon.Vol, 0.0 },
                        { TypePokemon.Psy, 1.0 },
                        { TypePokemon.Insecte, 0.5 },
                        { TypePokemon.Spectre, 1.0 },
                        { TypePokemon.Dragon, 1.0 },
                        { TypePokemon.Tenebres, 1.0 },
                        { TypePokemon.Acier, 2.0 },
                        { TypePokemon.Fee, 1.0 }
                    }
                },
                {
                    TypePokemon.Vol, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Feu, 1.0 },
                        { TypePokemon.Eau, 1.0 },
                        { TypePokemon.Plante, 2.0 },
                        { TypePokemon.Electrik, 0.5 },
                        { TypePokemon.Glace, 1.0 },
                        { TypePokemon.Combat, 2.0 },
                        { TypePokemon.Poison, 1.0 },
                        { TypePokemon.Sol, 1.0 },
                        { TypePokemon.Roche, 0.5 },
                        { TypePokemon.Acier, 0.5 },
                        { TypePokemon.Psy, 1.0 },
                        { TypePokemon.Insecte, 2.0 },
                        { TypePokemon.Dragon, 1.0 },
                        { TypePokemon.Tenebres, 1.0 },
                        { TypePokemon.Fee, 1.0 }
                    }
                },
                {
                    TypePokemon.Psy, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Feu, 1.0 },
                        { TypePokemon.Eau, 1.0 },
                        { TypePokemon.Plante, 1.0 },
                        { TypePokemon.Electrik, 1.0 },
                        { TypePokemon.Glace, 1.0 },
                        { TypePokemon.Combat, 2.0 },
                        { TypePokemon.Poison, 2.0 },
                        { TypePokemon.Sol, 1.0 },
                        { TypePokemon.Vol, 1.0 },
                        { TypePokemon.Psy, 0.5 },
                        { TypePokemon.Insecte, 1.0 },
                        { TypePokemon.Roche, 1.0 },
                        { TypePokemon.Spectre, 1.0 },
                        { TypePokemon.Dragon, 1.0 },
                        { TypePokemon.Tenebres, 0.0 },
                        { TypePokemon.Acier, 0.5 },
                        { TypePokemon.Fee, 1.0 }
                    }
                },
                {
                    TypePokemon.Insecte, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Feu, 0.5 },
                        { TypePokemon.Eau, 1.0 },
                        { TypePokemon.Plante, 2.0 },
                        { TypePokemon.Electrik, 1.0 },
                        { TypePokemon.Glace, 1.0 },
                        { TypePokemon.Combat, 0.5 },
                        { TypePokemon.Poison, 0.5 },
                        { TypePokemon.Sol, 1.0 },
                        { TypePokemon.Vol, 0.5 },
                        { TypePokemon.Psy, 2.0 },
                        { TypePokemon.Spectre, 0.5 },
                        { TypePokemon.Tenebres, 2.0 },
                        { TypePokemon.Acier, 0.5 },
                        { TypePokemon.Fee, 0.5 }
                    }
                },
                {
                    TypePokemon.Roche, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Feu, 2.0 },
                        { TypePokemon.Eau, 1.0 },
                        { TypePokemon.Plante, 1.0 },
                        { TypePokemon.Electrik, 1.0 },
                        { TypePokemon.Glace, 2.0 },
                        { TypePokemon.Combat, 0.5 },
                        { TypePokemon.Poison, 1.0 },
                        { TypePokemon.Sol, 0.5 },
                        { TypePokemon.Vol, 2.0 },
                        { TypePokemon.Insecte, 2.0 },
                        { TypePokemon.Acier, 0.5 },
                        { TypePokemon.Fee, 1.0 }
                    }
                },
                {
                    TypePokemon.Spectre, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Normal, 0.0 },
                        { TypePokemon.Feu, 1.0 },
                        { TypePokemon.Eau, 1.0 },
                        { TypePokemon.Plante, 1.0 },
                        { TypePokemon.Electrik, 1.0 },
                        { TypePokemon.Glace, 1.0 },
                        { TypePokemon.Combat, 1.0 },
                        { TypePokemon.Poison, 1.0 },
                        { TypePokemon.Sol, 1.0 },
                        { TypePokemon.Vol, 1.0 },
                        { TypePokemon.Psy, 2.0 },
                        { TypePokemon.Insecte, 1.0 },
                        { TypePokemon.Roche, 1.0 },
                        { TypePokemon.Spectre, 2.0 },
                        { TypePokemon.Dragon, 1.0 },
                        { TypePokemon.Tenebres, 0.5 },
                        { TypePokemon.Acier, 0.5 },
                        { TypePokemon.Fee, 1.0 }
                    }
                },
                {
                    TypePokemon.Dragon, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Dragon, 2.0 },
                        { TypePokemon.Acier, 0.5 },
                        { TypePokemon.Fee, 0.0 }
                    }
                },
                {
                    TypePokemon.Tenebres, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Feu, 1.0 },
                        { TypePokemon.Eau, 1.0 },
                        { TypePokemon.Plante, 1.0 },
                        { TypePokemon.Electrik, 1.0 },
                        { TypePokemon.Glace, 1.0 },
                        { TypePokemon.Combat, 0.5 },
                        { TypePokemon.Poison, 1.0 },
                        { TypePokemon.Sol, 1.0 },
                        { TypePokemon.Vol, 1.0 },
                        { TypePokemon.Psy, 2.0 },
                        { TypePokemon.Insecte, 1.0 },
                        { TypePokemon.Roche, 1.0 },
                        { TypePokemon.Spectre, 2.0 },
                        { TypePokemon.Dragon, 1.0 },
                        { TypePokemon.Tenebres, 0.5 },
                        { TypePokemon.Acier, 0.5 },
                        { TypePokemon.Fee, 0.5 }
                    }
                },
                {
                    TypePokemon.Acier, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Feu, 0.5 },
                        { TypePokemon.Eau, 0.5 },
                        { TypePokemon.Plante, 1.0 },
                        { TypePokemon.Electrik, 0.5 },
                        { TypePokemon.Glace, 2.0 },
                        { TypePokemon.Combat, 1.0 },
                        { TypePokemon.Poison, 1.0 },
                        { TypePokemon.Sol, 1.0 },
                        { TypePokemon.Vol, 1.0 },
                        { TypePokemon.Psy, 1.0 },
                        { TypePokemon.Insecte, 1.0 },
                        { TypePokemon.Roche, 2.0 },
                        { TypePokemon.Spectre, 1.0 },
                        { TypePokemon.Dragon, 1.0 },
                        { TypePokemon.Tenebres, 1.0 },
                        { TypePokemon.Acier, 0.5 },
                        { TypePokemon.Fee, 2.0 }
                    }
                },
                {
                    TypePokemon.Fee, new Dictionary<TypePokemon, double>
                    {
                        { TypePokemon.Feu, 0.5 },
                        { TypePokemon.Eau, 1.0 },
                        { TypePokemon.Plante, 1.0 },
                        { TypePokemon.Electrik, 1.0 },
                        { TypePokemon.Glace, 1.0 },
                        { TypePokemon.Combat, 2.0 },
                        { TypePokemon.Poison, 0.5 },
                        { TypePokemon.Sol, 1.0 },
                        { TypePokemon.Vol, 1.0 },
                        { TypePokemon.Psy, 1.0 },
                        { TypePokemon.Insecte, 1.0 },
                        { TypePokemon.Roche, 1.0 },
                        { TypePokemon.Spectre, 1.0 },
                        { TypePokemon.Dragon, 2.0 },
                        { TypePokemon.Tenebres, 2.0 },
                        { TypePokemon.Acier, 0.5 },
                        { TypePokemon.Fee, 1.0 }
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

        public static double GetEffectiveness(TypePokemon attackerType, TypePokemon defenderType1, TypePokemon defenderType2)
        {
            double eff1 = GetEffectiveness(attackerType, defenderType1);
            double eff2 = GetEffectiveness(attackerType, defenderType2);
            return eff1 * eff2;
        }

        public static string GetEffectivenessMessage(double multiplier)
        {
            return multiplier switch
            {
                >= 2.0 => "Super efficace !",
                0.5 => "Pas très efficace",
                0.0 => "Aucun effet...",
                _ => ""
            };
        }
    }
}