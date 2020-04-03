using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public Vector2 Velocity = new Vector2();
    [Export] public float Speed = 200f;

    [Export] private PackedScene _bulletScene;

    public override void _Ready()
    {
        _bulletScene = ResourceLoader.Load<PackedScene>("res://Bullet/Bullet.tscn");
    }

    public override void _PhysicsProcess(float delta)
    {
        Velocity = Velocity.Normalized() * Speed;
        MoveAndSlide(Velocity);
    }

    public override void _Input(InputEvent @event)
    {
        Velocity.x = Input.GetActionStrength("MoveRight") - Input.GetActionStrength("MoveLeft");
        Velocity.y = Input.GetActionStrength("MoveDown") - Input.GetActionStrength("MoveUp");

        if (Input.IsActionJustPressed("Shoot"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Bullet bullet = _bulletScene.Instance() as Bullet;
        bullet.Position = Position;
        GD.Print("POW POW");
    }
}
