## 4em partie : L'héritage

Attention : gros morceaux ! C’est sans doute une des parties les plus charnière.

En programmation orientée objet, il est fréquent que plusieurs classes partagent des caractéristiques communes.
👉 Plutôt que de réécrire le même code plusieurs fois, on peut factoriser ce qui est commun dans une classe de base, puis créer des classes dérivées qui héritent de ces propriétés et comportements.

Exemple concret :

- Tous les Pokémon ont un nom, un type, une santé.
- Mais une attaque de type Dégâts ou une attaque de type Soin n’ont pas exactement le même comportement.
- On place donc ce qui est commun dans une classe Attack, et on laisse des sous-classes comme DamageAttack, HealingAttack ou VampireAttack définir leur logique propre.

Ici, on utilise déjà un peu de **polymorphisme** :
Si une méthode reçoit un paramètre de type Attack, elle pourra accepter indifféremment une DamageAttack ou une HealingAttack, et le bon comportement sera exécuté automatiquement.

✅ Message à retenir :

- Héritage = réutiliser du code commun.
- Polymorphisme = utiliser le même nom (Use) mais obtenir des comportements différents selon le type concret de l’objet.

### 📌 classes abstraites et polymorphisme

#### Pourquoi une classe abstraite ?
Une **classe abstraite** est une classe qui ne peut pas être instanciée directement.  
Elle sert de **modèle commun** à plusieurs classes filles, et définit :
- des **attributs/méthodes partagés**,
- des **méthodes abstraites** qui doivent être réécrites dans les classes filles.

👉 Cela évite la duplication de code, tout en forçant un contrat minimal.

##### Classe abstraite `Attack`

```csharp
namespace PokemonBattle;

public abstract class Attack
{
    public string Name { get; }
    public PokemonType Type { get; }

    protected Attack(string name, PokemonType type)
    {
        Name = name;
        Type = type;
    }

    public abstract void Use(Pokemon user, Pokemon target);

    public abstract void GetDescription();
}
```

- `abstract` devant la classe → impossible de faire `new Attack()`.  
- `abstract` devant une méthode → **oblige les enfants à implémenter leur version**.  
- Constructeur `protected` → bonne pratique : empêche de créer directement une `Attack` en dehors d’une classe fille.

##### Classe concrète `DamageAttack`

```csharp
namespace PokemonBattle;

public class DamageAttack : Attack
{
    public int Damage { get; }

    public DamageAttack(string name, int damage, PokemonType type)
        : base(name, type)
    {
        Damage = damage;
    }

    public override void Use(Pokemon user, Pokemon target)
    {
        Console.WriteLine($"{user.Name} uses {Name}!");
        var effectiveness = TypeHelper.GetEffectiveness(Type, target.Type);
        var totalDamage = (int)(Damage * effectiveness);
        target.TakeDamage(Name, totalDamage, effectiveness);
    }

    public override void GetDescription()
    {
        Console.WriteLine($"- {Name} (Damage: {Damage}, Type: {Type})");
    }
}
```

- Code commun hérité de `Attack`.  
- Implémente `Use()` avec des dégâts simples.  
- Vous remarquerez l'utilisation du mot clef base : c'est un appel à l'implémentation parente.


##### Classe concrète `HealingAttack`

```csharp
namespace PokemonBattle;

public class HealingAttack : Attack
{
    public int HealingAmount { get; }

    public HealingAttack(string name, int healingAmount, PokemonType type)
        : base(name, type)
    {
        HealingAmount = healingAmount;
    }

    public override void Use(Pokemon user, Pokemon target)
    {
        if (user.IsFainted())
        {
            Console.WriteLine($"{user.Name} has fainted and cannot use {Name}.");
            return;
        }

        user.Heal(HealingAmount);
        Console.WriteLine($"{user.Name} used {Name} and healed for {HealingAmount} HP!");
    }

    public override void GetDescription()
    {
        Console.WriteLine($"- {Name} (Healing: {HealingAmount}, Type: {Type})");
    }
}
```

- Même contrat (`Use`, `GetDescription`), mais comportement différent.  
- Ne touche pas à l’adversaire, soigne l’utilisateur.


##### Classe concrète `VampireAttack` (héritage + polymorphisme)

```csharp
namespace PokemonBattle;

public class VampireAttack : DamageAttack
{
    public double VampireCoefficient { get; }

    public VampireAttack(string name, int damage, double vampireCoefficient, PokemonType type)
        : base(name, damage, type)
    {
        VampireCoefficient = vampireCoefficient;
    }

    public override void Use(Pokemon user, Pokemon target)
    {
        if (user.IsFainted())
        {
            Console.WriteLine($"{user.Name} has fainted and cannot use {Name}.");
            return;
        }

        // Console.WriteLine($"{user.Name} uses {Name}!");
        // var effectiveness = TypeHelper.GetEffectiveness(Type, target.Type);
        // var totalDamage = (int)(Damage * effectiveness);
        // target.TakeDamage(Name, totalDamage, effectiveness);
        // Duplicata du parent, peut être remplacé par base.Use()
        base.Use(user, target)
        int heal = (int)(totalDamage * VampireCoefficient);
        user.Heal(heal);
        Console.WriteLine($"{user.Name} healed for {heal} HP due to vampire effect!");
    }

    public override void GetDescription()
    {
        base.GetDescription();
        Console.WriteLine("  (Heals part of the damage dealt)");
    }
}
```
- Vous pouvez voir qu'on a copié collé le comportement de DamageAttack : On peut utiliser base.Use() pour éviter de dupliquer le code !
- Hérite de `DamageAttack`.  
- **Réutilise du code commun** (calcul des dégâts, effet de type).  
- Ajoute un comportement spécialisé (soin proportionnel aux dégâts).  

##### Petit exemple d’utilisation

```csharp
var pikachu = new Pokemon("Pikachu", PokemonType.Electric, 100);
var bulbasaur = new Pokemon("Bulbizarre", PokemonType.Grass, 100);

Attack healPulse     = new HealingAttack("Soin", 30, PokemonType.Normal);
Attack drainLife     = new VampireAttack("Vampirisme", 20, 0.5, PokemonType.Grass);

thunderShock.Use(pikachu, bulbasaur);
healPulse.Use(pikachu, bulbasaur);
drainLife.Use(bulbasaur, pikachu);
```

👉 **Polymorphisme** :  
- On déclare tout en type `Attack`, mais chaque objet appelle **sa propre version** de `Use()`.  
- `pikachu` peut lancer une attaque de dégâts, de soin ou vampire sans que le code ait besoin de savoir le type exact d’attaque.  


#### Résumé pédagogique
- **Classe abstraite** = modèle commun, pas instanciable.  
- **Classes concrètes** héritent et implémentent les comportements.  
- **Polymorphisme** : on peut manipuler des attaques en tant que `Attack`, et laisser chaque classe concrète exécuter son comportement.  
- Avantage : **évite la duplication de code** (ex. nom, type, logique commune) tout en permettant la **spécialisation** (damage, heal, vampire).  

#### Pour aller plus loin :

L’idée d’« appeler l’implémentation parente » n’est pas propre à C# — on la retrouve dans beaucoup de langages, avec une syntaxe différente.
Voici l’équivalent de base.Method() ailleurs :

- Java → super.method();
Et pour le constructeur parent : super(args);
- C++ → Base::method();
(Si la méthode est virtuelle et overridée, tu forces l’appel de la version de Base.)
- JavaScript (classes ES6) → super.method();
(Dans un constructeur enfant, super() appelle le constructeur parent.)
- Python (3.x) → super().method()
(Pas de self à repasser. super() gère aussi le MRO en héritage multiple.)
- Ruby → super ou super(args)
(Sans arguments → transmet automatiquement ceux reçus.)
- PHP → parent::method();
(Et parent::__construct() pour le constructeur.)
- Kotlin → super.method()
(En cas d’héritage de plusieurs implémentations par défaut via interfaces, on peut préciser : super<InterfaceA>.method().)
- Swift → super.method()
(Et super.init(...) pour l’initialiseur parent.)
- Objective-C → [super method];
(Et [super init] pour l’initialiseur parent.)
- Go → pas d’héritage de classes (composition). Donc pas d’appel « super/base ».
- Rust → pas d’héritage de classes ; on peut appeler l’impl par défaut d’un trait via la syntaxe qualifiée, p. ex. TraitName::method(self).










#### Exercice :

- Vous allez implémenter ce système d’attaques à base d’héritage et de polymorphisme
- Pour ceux en avance : vampirisme doit soigner la moitié des dégâts infligés ! En prenant donc en compte les faiblesses et résistances.
-Comprendre l’utilité de l’encapsulation




Class parent : Attack
Class enfant : DamageAttack / HealthAttack / VampireAttack
Methode abstraite Use sur Attack
Utilisation de Use sur les pokémons

Pokemon doit maintenant avoir List<Attack>