using TMPro;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] TMP_Text debugText;
    [SerializeField] PlayerStateMachine playerSM;
    [SerializeField] WeaponStateMachine weaponSM;

    void Update()
    {
        debugText.text =
            $"Movement: {playerSM.CurrentStateName}\n" +
            $"Weapon: {weaponSM.CurrentStateName}\n" +
            $"Health; {playerSM.health.GetHealth()}";
    }
}
