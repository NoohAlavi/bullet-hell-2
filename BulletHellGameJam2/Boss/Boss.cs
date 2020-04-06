using Godot;
using System;

public class Boss : KinematicBody2D
{

    public float Health = 300f;

    private Vector2 _velocity = new Vector2();
    private float _maxSpeed = 100f;
    private float _speed;
    private Player _player;

    private PackedScene _bulletScene;

    public override void _Ready()
    {
        _player = GetNode<Player>("../Player");

        _bulletScene = ResourceLoader.Load<PackedScene>("res://Bullet/Bullet.tscn");
        _speed = _maxSpeed;


        Shoot();
    }

    public override void _Process(float delta)
    {
        GetNode<TextureProgress>("../HUD/BossHealthBar").Value = Health;

        if (Health <= 0f)
        {
            QueueFree();
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        _velocity.x = _speed;
        MoveAndSlide(_velocity);

        if (Position.x >= (512 - 32))
        {
            _speed = -_maxSpeed;
        }
        if (Position.x <= 32f)
        {
            _speed = _maxSpeed;
        }
    }

    async private void Shoot()
    {
        Bullet b = _bulletScene.Instance() as Bullet;
        GetNode("../BulletHolder").AddChild(b);
        b.Position = Position;
        b.Direction = Position.DirectionTo(_player.Position);
        b.Speed = 250f;
        b.isEnemyBullet = true;
        await ToSignal(GetTree().CreateTimer(.25f), "timeout");
        Shoot();
    }
}
