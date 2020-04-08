using Godot;
using System;

public class Enemy : KinematicBody2D
{

    private Timer _shootTimer;
    private PackedScene _bulletScene;

    private Vector2 _screenSize;
    private Player _player;
    private PackedScene _orbScene;

    public bool IsDead = false;

    public AnimationPlayer Anim;
    public Timer KillTimer;

    public override void _Ready()
    {

        GD.Randomize();

        _shootTimer = GetNode<Timer>("ShootTimer");
        _shootTimer.Connect("timeout", this, "Shoot");

        _shootTimer.WaitTime = GD.Randf() + 0.25f;

        _bulletScene = ResourceLoader.Load<PackedScene>("res://Bullet/Bullet.tscn");

        _screenSize = GetViewport().Size;
        _player = GetNode<Player>("/root/World/Player");

        Anim = GetNode<AnimationPlayer>("AnimationPlayer");
        KillTimer = GetNode<Timer>("KillTimer");
        KillTimer.Connect("timeout", this, "Remove");

        _orbScene = ResourceLoader.Load<PackedScene>("res://Orb/Orb.tscn");

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
        if (!IsDead && Position.DistanceTo(_player.Position) > 32)
        {
            Bullet bullet = _bulletScene.Instance() as Bullet;
            GetNode("/root/World/BulletHolder").AddChild(bullet);
            bullet.Position = this.Position;
            bullet.isEnemyBullet = true;
            // bullet.LookAt(_player.Position);
            bullet.Speed = 250f;

            bullet.Direction = Position.DirectionTo(_player.Position);
        }
    }

    private void Remove()
    {
        Orb orb = _orbScene.Instance() as Orb;
        GetNode("/root/World/OrbHolder").AddChild(orb);
        orb.Position = Position;
        QueueFree();
    }
}
