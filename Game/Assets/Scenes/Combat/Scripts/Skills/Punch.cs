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
        cooldown: 0,
        attack: true,
        description: "Perform a basic attack on one enemy."
        
        ){

    }

    public override bool Effect(GameCharacter target) {

        int damageDealt = Mathf.FloorToInt(gc.Strength * power);

        target.TakeDamage(damageDealt);

        return true;

    }

    public override void SkillAnimation(Vector3 targetPos, GameCharacter sender, SpriteManager sm) {
        SpriteRenderer AbilityRenderer = sm.spriteLayers["Ability"];
        Transform AbilityContainer = AbilityRenderer.gameObject.transform;

        sm.SetSprite(this.sprites[0], AbilityRenderer);
        sm.HideSprite(AbilityRenderer);

        sm.ChangeOpacity(AbilityRenderer, 1f);
        sm.SetScale(AbilityRenderer.transform, 6f);

        Vector3 toTarget = targetPos - sender.transform.position; 

        sm.AttackAnimation(sender);
        sm.LungeTo(sender, toTarget * 0.65f, 0.45f);
        sm.RollScales(AbilityContainer, toTarget * 0.95f, 10, 0.18f, 0.8f, false, false, 10);
    }

}
