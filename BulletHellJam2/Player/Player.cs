using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public Vector2 Velocity = new Vector2();
    [Export] public float Speed = 200f;

    public override void _PhysicsProcess(float delta)
    {
        Velocity = Velocity.Normalized() * Speed;
        MoveAndSlide(Velocity);
    }

    public override void _Input(InputEvent @event)
    {
        Velocity.x = Input.GetActionStrength("MoveRight") - Input.GetActionStrength("MoveLeft");
        Velocity.y = Input.GetActionStrength("MoveDown") - Input.GetActionStrength("MoveUp");
    }
}