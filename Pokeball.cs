using PokemonBattle;

public class Pokeboule : IItem
{
    // Propriétés de l'objet
    public string Name => "Pokeboule";
    public int Cost { get; } = 220;

    private Random rng = new Random();
    private int captureThreshold; // Plus la valeur est basse, plus la capture du pokemon est facile

    public Pokeboule(int captureThreshold = 50)
    {
        this.captureThreshold = captureThreshold;
    }

    // Utilisation de la Pokeball sur un pokemon cible
    public void Use(Pokemon target)
    {
        Console.WriteLine($"Joueur utilise Pokeboule sur {target.Name}.");
        
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