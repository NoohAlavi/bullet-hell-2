using Godot;
using System;

public class Bullet : Area2D
{

    [Export] public float Speed = 20f;

    public override void _Ready()
    {
        Connect("body_entered", this, "OnBodyEntered");
        GD.Randomize();
        float r = GD.Randf();
        float g = GD.Randf();
        float b = GD.Randf();
        GetNode<Sprite>("Sprite").Modulate = new Color(r, g, b);
    }

    public override void _PhysicsProcess(float delta)
    {
        Position = new Vector2(Position.x, (Position.y - Speed * delta));
    }

    public void OnBodyEntered(PhysicsBody2D body)
    {
        if (body is Enemy)
        {
            body.QueueFree();
            QueueFree();
        }
    }
}
