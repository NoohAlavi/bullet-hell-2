using Godot;
using System;

public class Boss : KinematicBody2D
{
    private Player _player;

    private PackedScene _bulletScene;

    public override void _Ready()
    {
        _player = GetNode<Player>("../Player");

        _bulletScene = ResourceLoader.Load<PackedScene>("res://Bullet/Bullet.tscn");

        Shoot();
    }

    async private void Shoot()
    {
        Bullet b = _bulletScene.Instance() as Bullet;
        GetNode("../BulletHolder").AddChild(b);
        b.Position = Position;
        b.Direction = Position.DirectionTo(_player.Position);
        b.Speed = 250f;
        b.isEnemyBullet = true;
        await ToSignal(GetTree().CreateTimer(.5f), "timeout");
        Shoot();
    }
}
