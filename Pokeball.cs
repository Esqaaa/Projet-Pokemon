using PokemonBattle;

public class Pokeball : IItem
{
    public string Name => "Pokeball";
    public int Cost { get; } = 200;

    private Random rng = new Random();
    private int captureThreshold; // Plus la valeur est basse, plus la capture du pokemon est facile

    public Pokeball(int captureThreshold = 50)
    {
        this.captureThreshold = captureThreshold;
    }

    public void Use(Pokemon target)
    {
        Console.WriteLine($"Joueur utilise Pokeball sur {target.Name}.");
        
        int roll = rng.Next(0, 101);
        if (roll > captureThreshold)
        {
            target.Catch();
            Console.WriteLine("Le pokémon est capturé !");
        }
        else
        {
            Console.WriteLine($"{target.Name} s'est libéré !");
        }
    }
}