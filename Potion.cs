using PokemonBattle;

// Potion pour soigner un pokemon
public class Ventoline : IItem
{
    public string Name => "Ventoline";
    public int Cost { get; } = 500;

    private int healAmount;

    public Ventoline(int healAmount = 20)
    {
        this.healAmount = healAmount;
    }

    // Utilisation de la potion sur un pokemon
    public void Use(Pokemon pokemon)
    {
        pokemon.Heal(healAmount);
        Console.WriteLine($"Joueur utilise une ventoline et regénère {healAmount} PV à {pokemon.Name}.");
    }
}