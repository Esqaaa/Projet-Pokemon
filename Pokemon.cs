namespace PokemonBattle
{
    public class Pokemon
    {
        public string Nom;
        public string Type;
        public int HealthPoint;
        public int Attack;
        public int Defense;

        public Pokemon(string nom, string type, int healthPoint, int attack, int defense)
        {
            Nom = nom;
            Type = type;
            HealthPoint = healthPoint;
            Attack = attack;
            Defense = defense;
        }

        public void Attaquer(Pokemon cible)
        {
            int degats = Attack - cible.Defense;
            if (degats < 0) degats = 0;

            Console.WriteLine($"{Nom} attaque {cible.Nom} et inflige {degats} dégâts !");
            cible.RecevoirDegats(degats);
        }

        public void RecevoirDegats(int degats)
        {
            HealthPoint -= degats;
            if (HealthPoint < 0) HealthPoint = 0;

            Console.WriteLine($"{Nom} a {HealthPoint} PV restants !");
        }
    }
}
