using PokemonBattle;

// Interface pour les objets utilisables
public interface IItem
{
    string Name { get; }
    int Cost { get; }

    void Use(Pokemon target);
}
