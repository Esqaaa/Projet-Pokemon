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
            Console.WriteLine($"{user.Name} est K.O. et ne peut pas utiliser {Name}.");
            return;
        }

        user.Heal(HealingAmount);
        Console.WriteLine($"{user.Name} utilise {Name} et récupère {HealingAmount} PV !");
    }

    public override void GetDescription()
    {
        Console.WriteLine($"- {Name} (Soin : {HealingAmount}, Type : {Type})");
    }
}