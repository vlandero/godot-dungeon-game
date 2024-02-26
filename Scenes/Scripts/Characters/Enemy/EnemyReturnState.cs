using Godot;
using System;

public partial class EnemyReturnState : EnemyState
{
    [Export(PropertyHint.Range, "0,20,0.1")] public float speed = 5;
    private Vector3 destination;
    public override void _Ready()
    {
        base._Ready();
        Vector3 localPos = characterNode.PathNode.Curve.GetPointPosition(0);
        Vector3 globalPos = characterNode.PathNode.GlobalPosition;
        destination = globalPos + localPos;
    }
    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);

        characterNode.AgentNode.TargetPosition = destination;
    }

    public override void _PhysicsProcess(double delta)
    {
        if(characterNode.AgentNode.IsNavigationFinished())
        {
            characterNode.StateMachineNode.SwitchState<EnemyIdleState>();
            return;
        }
        characterNode.Velocity = characterNode.GlobalPosition.DirectionTo(destination) * speed;
        characterNode.MoveAndSlide();
    }
}