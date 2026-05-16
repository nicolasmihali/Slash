using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class WeaponStateMachine : StateMachine
{
    [field: SerializeField] public PlayerStateMachine PlayerStateMachine { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }
    [field: SerializeField] public Attack LaunchAttack { get; private set; }
    [field: SerializeField] public int BlockAmount { get; private set; }
    [field: SerializeField] public float ParryWindowAmount { get; private set; }


    private void Start()
    {
        SwitchState(new WeaponIdleState(this));
    }
}
