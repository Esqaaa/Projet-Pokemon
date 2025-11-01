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

        public void Attaquer(Pokemon cible, int degatsBase)
        {
            if (HealthPoint <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{Nom} ne peut pas attaquer car il est KO !");
                Console.ResetColor();
                return;
            }

            if (degatsBase < 0)
                degatsBase = 0;

            // --- 🔥 Calcul du multiplicateur selon le type ---
            double multiplicateur = TypeHelper.GetEffectiveness(this.Type, cible.Type);

            // --- ⚔️ Calcul des dégâts finaux avec défense ---
            int degatsFinaux = (int)((degatsBase * multiplicateur) - cible.Defense);
            if (degatsFinaux < 0) degatsFinaux = 0;

            // --- 💬 Message sur l’efficacité ---
            string message = multiplicateur switch
            {
                2.0 => $"L'attaque de {Nom} est très efficace contre {cible.Nom} ! Dégâts doublés 💥",
                0.5 => $"L'attaque de {Nom} n'est pas très efficace contre {cible.Nom}... Dégâts réduits 😐",
                0.0 => $"L'attaque de {Nom} n’a aucun effet sur {cible.Nom} 😶",
                _ => $"L'attaque de {Nom} est normalement efficace contre {cible.Nom}."
            };

            // --- 💡 Affichage avec couleur selon efficacité ---
            if (multiplicateur == 2.0) Console.ForegroundColor = ConsoleColor.Green;
            else if (multiplicateur == 0.5) Console.ForegroundColor = ConsoleColor.Yellow;
            else if (multiplicateur == 0.0) Console.ForegroundColor = ConsoleColor.Gray;
            else Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(message);
            Console.ResetColor();

            // --- ⚔️ Application des dégâts ---
            cible.RecevoirDegats(degatsFinaux);

            // --- 📝 Affichage des dégâts infligés ---
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Nom} inflige {degatsFinaux} points de dégâts à {cible.Nom} !");
            Console.ResetColor();

            // --- 💬 Vérification de l’état du Pokémon cible ---
            if (cible.HealthPoint > 0)
                Console.WriteLine($"{cible.Nom} peut continuer à combattre ! PV restants : {cible.HealthPoint}");
            else
                Console.WriteLine($"{cible.Nom} est KO !");
        }



        public void RecevoirDegats(int degats)
        {
            HealthPoint -= degats;
            if (HealthPoint <= 0)
            {
                HealthPoint = 0;
            }
        }
    }
}   
