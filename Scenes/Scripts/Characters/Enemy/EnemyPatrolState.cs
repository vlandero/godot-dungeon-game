using Godot;
using System;

public partial class EnemyPatrolState : EnemyState
{
    [Export] private Timer idleTimerNode;
    [Export(PropertyHint.Range, "0,20,0.1")] private float maxIdleTime = 4f;
    private int pointIndex = 0;
    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        
        pointIndex = 1;

        destination = GetPointsGlobalPosition(pointIndex);
        characterNode.AgentNode.TargetPosition = destination;

        characterNode.AgentNode.NavigationFinished += HandleNavigationFinished;
        idleTimerNode.Timeout += HandleTimeout;
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        base.ExitState();
        characterNode.AgentNode.NavigationFinished -= HandleNavigationFinished;
        idleTimerNode.Timeout -= HandleTimeout;
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }

    private void HandleTimeout()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        pointIndex = Mathf.Wrap(pointIndex + 1, 0, characterNode.PathNode.Curve.PointCount);

        destination = GetPointsGlobalPosition(pointIndex);
        characterNode.AgentNode.TargetPosition = destination;
    }


    public override void _PhysicsProcess(double delta)
    {
        if(!idleTimerNode.IsStopped())
        {
            return;
        }
        Move();
    }

    private void HandleNavigationFinished()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);

        RandomNumberGenerator rng = new RandomNumberGenerator();
        idleTimerNode.WaitTime = rng.RandfRange(0, maxIdleTime);

        idleTimerNode.Start();
    }
}
