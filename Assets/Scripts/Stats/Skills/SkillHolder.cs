using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    public Skill skill;
    float cooldownTime;


    enum SkillState {
        Ready,
        Cooldown
    }

    SkillState state = SkillState.Ready;

    // This is where keycode would go
    // But instead we want to activate it from menu option

    // Update is called once per frame
    void Update()
    {
        if (true) { // Activated from menu
            switch (state) {
                case SkillState.Ready:
                    skill.Activate(gameObject);
                    state = SkillState.Cooldown;
                    // Do the skill
                    break;
                case SkillState.Cooldown:
                    if (cooldownTime > 0) {
                        // Do not do the skill
                        // Maybe grey out the menu option
                        // Handle turn stuff here?
                    } else {
                        state = SkillState.Ready;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
