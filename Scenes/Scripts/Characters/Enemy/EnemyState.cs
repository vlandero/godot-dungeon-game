using Godot;

public abstract partial class EnemyState: CharacterState
{
    [Export(PropertyHint.Range, "0,20,0.1")] public float speed = 5;
    protected Vector3 destination;

    protected Vector3 GetPointsGlobalPosition(int index)
    {
        Vector3 localPos = characterNode.PathNode.Curve.GetPointPosition(index);
        Vector3 globalPos = characterNode.PathNode.GlobalPosition;
        return globalPos + localPos;
    }

    protected void Move()
    {
        characterNode.AgentNode.GetNextPathPosition();
        characterNode.Velocity = characterNode.GlobalPosition.DirectionTo(destination) * speed;
        characterNode.MoveAndSlide();
    }
    
}