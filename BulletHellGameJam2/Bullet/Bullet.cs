using Godot;
using System;

public class Bullet : Area2D
{

    public Vector2 Direction = new Vector2();
    [Export] public float Speed = 3f;

    public override void _PhysicsProcess(float delta)
    {
        Position = new Vector2(Position.x, Position.y - Speed);
    }
}
