// Classe abstraite 'Attack'

namespace PokemonBattle;

public abstract class Attack
{
    public string Name { get; }
    public TypePokemon Type { get; }

    protected Attack(string name, TypePokemon type)
    {
        Name = name;
        Type = type;
    }

    public abstract void Use(Pokemon attacker, Pokemon defender);
    public abstract void GetDescription(); 
}