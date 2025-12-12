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

    // Constructeur de copie
    public Attack(Attack other)
    {   
        Name = other.Name;
        Type = other.Type;
    }

    public abstract void Use(Pokemon attacker, Pokemon defender);
    public abstract void GetDescription(); 
}