using Godot;
using System;

public class Enemy : KinematicBody2D
{

    private Timer _shootTimer;
    private PackedScene _bulletScene;

    private Vector2 _screenSize;
    private Player _player;

    public override void _Ready()
    {
        _shootTimer = GetNode<Timer>("ShootTimer");
        _shootTimer.Connect("timeout", this, "Shoot");

        _shootTimer.WaitTime = GD.Randi() % 1 + 1;

        _bulletScene = ResourceLoader.Load<PackedScene>("res://Bullet/Bullet.tscn");

        _screenSize = GetViewport().Size;
        _player = GetNode<Player>("/root/World/Player");

        Shoot();
    }

    public override void _Process(float delta)
    {
        if (Position.y >= _screenSize.y)
        {
            QueueFree();
        }
    }

    private void Shoot()
    {
        Bullet bullet = _bulletScene.Instance() as Bullet;
        GetNode("/root/World/BulletHolder").AddChild(bullet);
        bullet.Position = this.Position;
        bullet.isEnemyBullet = true;
        // bullet.LookAt(_player.Position);
        bullet.Speed = 250f;

        if (GD.Randf() > 0.5f)
        {
            bullet.Direction = Position.DirectionTo(_player.Position);
        }
        else
        {
            bullet.Direction = Vector2.Down;
        }
    }
}
