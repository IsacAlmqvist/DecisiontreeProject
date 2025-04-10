using System.Collections.Generic;
using UnityEngine;

public class Punch : Skill
{

    public Punch() : base(
        icon: Resources.Load<Sprite>("Sprites/Abilities/Punch_Icon"),
        sprites: new List<Sprite>{Resources.Load<Sprite>("Sprites/Abilities/punchAnimation")},
        gc: null,
        name: "Punch", 
        power: 0, 
        manaCost: 0,
        skillCost: 1,
        description: "Perform a basic attack on one enemy."
        
        ){

    }

    public override bool Effect(GameCharacter target)
    {

        if (target == gc)
            return false;
        if (gc.Mana < manaCost)
            return false;
            
        gc.Mana -= manaCost;

        int damageDealt = Mathf.FloorToInt(gc.Strength * power);

        target.TakeDamage(damageDealt);

        return true;

    }

}
