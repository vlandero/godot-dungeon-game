using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : EnemyState
{
    [Export] private Timer updateChaseTimer;
    private CharacterBody3D target;
    protected override void EnterState()
    {
        base.EnterState();
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        target = characterNode.ChaseAreaNode.GetOverlappingBodies().First() as CharacterBody3D;
        updateChaseTimer.Timeout += HandleUpdateChaseTimerTimeout;
    }
    protected override void ExitState()
    {
        base.ExitState();
        updateChaseTimer.Timeout -= HandleUpdateChaseTimerTimeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        Move();
    }

    private void HandleUpdateChaseTimerTimeout()
    {
        destination = target.GlobalPosition;
        characterNode.AgentNode.TargetPosition = destination;
    }
}
