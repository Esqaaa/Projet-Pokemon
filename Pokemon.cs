namespace PokemonBattle
{
    public class Pokemon
    {
        public string Nom;
        public TypePokemon Type;
        public int HealthPoint;
        public int Attack;
        public int Defense;

        public Pokemon(string nom, TypePokemon type, int healthPoint, int attack, int defense)
        {
            Nom = nom;
            Type = type;
            HealthPoint = healthPoint;
            Attack = attack;
            Defense = defense;
        }

        public void Attaquer(Pokemon cible, int degats)
        {
            if (HealthPoint <= 0)
            {
                Console.WriteLine($"{Nom} ne peut pas attaquer car il est KO !");
                return;
            }
            
            if (degats < 0)
            {
                degats = 0;
            }
            cible.RecevoirDegats(degats);
        }

        public void RecevoirDegats(int degats)
        {
            HealthPoint -= degats;
            if (HealthPoint <= 0)
            {
                HealthPoint = 0; 
                Console.WriteLine($"{Nom} est KO !");
            }
            else
            {
                Console.WriteLine($"Le Pokémon {Nom} peut continuer à combattre car il a assez de PV !");
            }

            Console.WriteLine($"{Nom} a effectivement {HealthPoint} PV restants !");
        }
    } 
}
