using Godot;
using System;

public class Enemy : KinematicBody2D
{

    public float Speed = 1f;

    public override void _Ready()
    {

    }

    public override void _PhysicsProcess(float delta)
    {
        Position = new Vector2(Position.x, Position.y + Speed);
    }
}
