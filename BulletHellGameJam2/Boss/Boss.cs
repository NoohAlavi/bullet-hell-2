using Godot;
using System;

public class Boss : KinematicBody2D
{

    public float Health = 200f;

    private Vector2 _velocity = new Vector2();
    private float _maxSpeed = 100f;
    private float _speed;
    private Player _player;

    private PackedScene _bulletScene;
    private PackedScene _virusScene;

    private bool _isRed = false;

    public override void _Ready()
    {
        _player = GetNode<Player>("../Player");
        _player.Health = 3f;

        _bulletScene = ResourceLoader.Load<PackedScene>("res://Bullet/Bullet.tscn");
        _virusScene = ResourceLoader.Load<PackedScene>("res://VirusEnemy/VirusEnemy.tscn");

        _speed = _maxSpeed;


        GetNode<Timer>("ShootTimer").Connect("timeout", this, "Shoot");
        GetNode<Timer>("SpawnTimer").Connect("timeout", this, "Spawn");
        GetNode<Timer>("ColorTimer").Connect("timeout", this, "RestoreColor");

        GetNode<AudioStreamPlayer>("../MusicPlayer").QueueFree();
    }

    public override void _Process(float delta)
    {
        GetNode<TextureProgress>("../HUD/BossHealthBar").Value = Health;

        if (Health <= 0f)
        {
            GetTree().ChangeScene("res://Win/Win.tscn");
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

    private void Shoot()
    {
        Bullet b = _bulletScene.Instance() as Bullet;
        GetNode("../BulletHolder").AddChild(b);
        b.Position = Position;
        b.Direction = Position.DirectionTo(_player.Position);
        b.Speed = 250f;
        b.isEnemyBullet = true;
    }

    private void Spawn()
    {
        VirusEnemy v = _virusScene.Instance() as VirusEnemy;
        GetNode("../EnemyHolder").AddChild(v);
        float randX = GD.Randi() % 512;
        float randY = GD.Randi() % 32;
        v.Position = new Vector2(randX, randY);
    }

    public void Damage()
    {
        if (!_isRed)
        {
            Health -= 2.5f;
            _isRed = true;
            Modulate = new Color(1f, 0f, 0f);
            GetNode<Timer>("ColorTimer").Start(0.25f);
        }
    }

    private void RestoreColor()
    {
        Modulate = new Color(1f, 1f, 1f);
        _isRed = false;
    }
}
