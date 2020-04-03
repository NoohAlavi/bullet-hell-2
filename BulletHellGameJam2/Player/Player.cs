using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public Vector2 Velocity = new Vector2();
    [Export] public float Speed = 200f;

    [Export] private PackedScene _bulletScene;

    private Vector2 _screenSize;

    public override void _Ready()
    {
        _bulletScene = ResourceLoader.Load<PackedScene>("res://Bullet/Bullet.tscn");
        _screenSize = GetViewport().Size;
    }

    public override void _PhysicsProcess(float delta)
    {
        Velocity = Velocity.Normalized() * Speed;
        MoveAndSlide(Velocity);

        Position = new Vector2(Mathf.Clamp(Position.x, 0, _screenSize.x), Mathf.Clamp(Position.y, 0, _screenSize.y));
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
        GetNode("/root/World/BulletHolder").AddChild(bullet);
        bullet.Position = Position;
    }
}
