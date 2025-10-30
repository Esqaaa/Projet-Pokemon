using PokemonBattle;

class Program
{
    static void Main()
    {
        Pokemon pikachu = new Pokemon("Pikachu", "Électrik", 40, 10, 5);
        Pokemon evoli = new Pokemon("Evoli", "Normal", 60, 15, 6);

        pikachu.Attaquer(evoli);
        evoli.Attaquer(pikachu);
    }
}