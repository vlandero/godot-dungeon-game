using Godot;
using System;

public partial class EnemyAttackState : EnemyState
{
    protected override void EnterState()
    {
        base.EnterState();
        // characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);
        // characterNode.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
    }
}
