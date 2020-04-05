using Godot;
using System;

public class Orb : Area2D
{
    public override void _Ready()
    {
        Connect("body_entered", this, "OnBodyEntered");
    }

    public void OnBodyEntered(PhysicsBody2D body)
    {
        if (body is Player)
        {
            Player p = body as Player;
            p.XP += 10f;
            QueueFree();
        }
    }
}
