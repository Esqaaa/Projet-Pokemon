namespace PokemonBattle;

public class VampireAttack : DamageAttack
{
    public double VampireCoefficient { get; }

    public VampireAttack(string name, int damage, double vampireCoefficient, TypePokemon type) : base(name, damage, type)
    {
        VampireCoefficient = vampireCoefficient;
    }

    public override void Use(Pokemon user, Pokemon target)
    {
        if (user.IsKO())
        {
            Console.WriteLine($"{user.Name} has fainted and cannot use {Name}.");
            return;
        }

        base.Use(user, target);
        int heal = (int)(Damage * VampireCoefficient);
        user.Heal(heal);
        Console.WriteLine($"{user.Name} healed for {heal} HP due to vampire effect!");
    }

    public override void GetDescription()
    {
        base.GetDescription();
        Console.WriteLine("  (Heals part of the damage dealt)");
    }
}