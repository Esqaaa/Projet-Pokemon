using PokemonBattle;

public class Potion : IItem
{
    public string Name => "Potion";
    public int Cost { get; } = 200;

    private int healAmount;

    public Potion(int healAmount = 20)
    {
        this.healAmount = healAmount;
    }

    public void Use(Pokemon pokemon)
    {
        pokemon.Heal(healAmount);
        Console.WriteLine($"Joueur utilise une potion et regénère {healAmount} PV à {pokemon.Name}.");
    }
}