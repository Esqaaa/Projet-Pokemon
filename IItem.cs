using PokemonBattle;

public interface IItem
{
    string Name { get; }
    int Cost { get; }

    void Use(Pokemon target);
}
