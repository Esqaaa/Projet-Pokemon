namespace PokemonBattle;

public class HealingAttack : Attack
{
    public int HealingAmount { get; }

    public HealingAttack(string name, int healingAmount, TypePokemon type)
        : base(name, type)
    {
        HealingAmount = healingAmount;
    }

    public override void Use(Pokemon user, Pokemon target)
    {
        if (user.IsKO())
        {
            Console.WriteLine($"{user.Name} has fainted and cannot use {Name}.");
            return;
        }

        user.Heal(HealingAmount);
        Console.WriteLine($"{user.Name} used {Name} and healed for {HealingAmount} HP!");
    }

    public override void GetDescription()
    {
        Console.WriteLine($"- {Name} (Healing: {HealingAmount}, Type: {Type})");
    }
}