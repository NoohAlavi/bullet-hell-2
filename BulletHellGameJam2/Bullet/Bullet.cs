using Godot;
using System;

public class Bullet : Area2D
{

    [Export] public float Speed = 20f;

    public override void _PhysicsProcess(float delta)
    {
        Position = new Vector2(Position.x, (Position.y - Speed * delta));
    }
}
