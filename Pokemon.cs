namespace PokemonBattle
{
    public class Pokemon
    {
        public string Name;
        public TypePokemon Type;
        public int HealthPoint;
        public int MaxHealthPoint;
        public int Attack;
        public int Defense;
        public int Speed;
        public List<Attack> Attacks;

        public Pokemon(string name, TypePokemon type, int healthPoint, int attack, int defense, int speed)
        {
            Name = name;
            Type = type;
            HealthPoint = healthPoint;
            MaxHealthPoint = healthPoint;
            Attack = attack;
            Defense = defense;
            Speed = speed;
            Attacks = new List<Attack>();
        }

        public void AddAttack(Attack attack)
        {
            Attacks.Add(attack);
        }

        public void UseAttack(int indexAttaque, Pokemon cible)
        {
            if (HealthPoint <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{Name} ne peut pas attaquer car il est KO !");
                Console.ResetColor();
                return;
            }

            if (indexAttaque < 0 || indexAttaque >= Attacks.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Attaque invalide !");
                Console.ResetColor();
                return;
            }

            Attacks[indexAttaque].Use(this, cible);
        }

        public void DisplayAttacks()
        {
            Console.WriteLine($"Attaques de {Name} :");
            for (int i = 0; i < Attacks.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                Attacks[i].GetDescription();
            }
        }

        public bool IsKO()
        {
            return HealthPoint <= 0;
        }

        public void Heal(int amount)
        {
            HealthPoint += amount;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Name} récupère {amount} PV !");
            Console.ResetColor();
        }

        public void Attaquer(Pokemon cible, int degatsBase)
        {
            if (HealthPoint <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{Name} ne peut pas attaquer car il est KO !");
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
                2.0 => $"L'attaque de {Name} est très efficace contre {cible.Name} ! Dégâts doublés 💥",
                0.5 => $"L'attaque de {Name} n'est pas très efficace contre {cible.Name}... Dégâts réduits 😐",
                0.0 => $"L'attaque de {Name} n’a aucun effet sur {cible.Name} 😶",
                _ => $"L'attaque de {Name} touche {cible.Name}."
            };

            // --- 💡 Affichage avec couleur selon efficacité ---
            if (multiplicateur == 2.0) Console.ForegroundColor = ConsoleColor.Green;
            else if (multiplicateur == 0.5) Console.ForegroundColor = ConsoleColor.Yellow;
            else if (multiplicateur == 0.0) Console.ForegroundColor = ConsoleColor.Gray;
            else Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(message);
            Console.ResetColor();

            // --- ⚔️ Application des dégâts ---
            cible.RecevoirDegats("Attaque basique", degatsFinaux, multiplicateur);

            // --- 📝 Affichage des dégâts infligés ---
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Name} inflige {degatsFinaux} points de dégâts à {cible.Name} !");
            Console.ResetColor();

            // --- 💬 Vérification de l’état du Pokémon cible ---
            if (cible.HealthPoint > 0)
                Console.WriteLine($"{cible.Name} peut continuer à combattre ! PV restants : {cible.HealthPoint}");
            else
                Console.WriteLine($"{cible.Name} est KO !");
        }



        public void RecevoirDegats(string attaque, int degats, double multiplicateur)
        {
            HealthPoint -= degats;
            if (HealthPoint <= 0) HealthPoint = 0;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{Name} a reçu {degats} points de dégâts de l'attaque {attaque} !");
            Console.ResetColor();   
        }

    }
}   
