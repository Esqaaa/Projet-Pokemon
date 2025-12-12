namespace PokemonBattle;

public class DamageAttack : Attack
{
    public int Damage { get; }

    public DamageAttack(string name, int damage, TypePokemon type) : base(name, type)
    {
        Damage = damage;
    }

    public override void Use(Pokemon attacker, Pokemon target)
    {
        Console.WriteLine($"{attacker.Name} utilise {Name}!");
        var effectiveness = TypeHelper.GetEffectiveness(Type, target.Type);
        var degatsFinaux = (int)(Damage * effectiveness);
        target.RecevoirDegats(Name, degatsFinaux, effectiveness);
    }

    public override void GetDescription()
    {
        Console.WriteLine($"- {Name} (Dégats: {Damage}, Type: {Type})");
    }
}