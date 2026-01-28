using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class WeaponStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }

    private void Start()
    {
        SwitchState(new WeaponIdleState(this));
    }
}
