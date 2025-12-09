namespace PokemonBattle;

public class VampireAttack : DamageAttack
{
    public double VampireCoefficient { get; }

    public VampireAttack(string name, int damage, double vampireCoefficient, TypePokemon type) 
        : base(name, damage, type)
    {
        VampireCoefficient = vampireCoefficient;
    }

    public override void Use(Pokemon user, Pokemon target)
    {
        if (user.IsKO())
        {
            Console.WriteLine($"{user.Name} a été mis K.O. et ne peut pas utiliser {Name}.");
            return;
        }

        Console.WriteLine($"{user.Name} utilise {Name}!");
        
        var effectiveness = TypeHelper.GetEffectiveness(Type, target.Type);
        
        var degatsFinaux = (int)(Damage * effectiveness);
        
        target.RecevoirDegats(Name, degatsFinaux, effectiveness);
        
        int soin = (int)(degatsFinaux * VampireCoefficient);
        user.Heal(soin);
        
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine($"💉 {user.Name} a absorbé {soin} PV grâce au vampirisme !");
        Console.ResetColor();
    }

    public override void GetDescription()
    {
        Console.WriteLine($"- {Name} (Dégâts: {Damage}, Type: {Type})");
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine($"  💉 Vampirisme : Restaure {VampireCoefficient * 100}% des dégâts infligés");
        Console.ResetColor();
    }
}