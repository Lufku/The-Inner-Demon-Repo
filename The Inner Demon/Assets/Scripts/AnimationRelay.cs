using UnityEngine;

public class AnimationRelay : MonoBehaviour
{
    public PlayerAttack attack;

    public void TriggerAttack()
    {
        if (attack != null)
            attack.TriggerAttack();
    }
}
