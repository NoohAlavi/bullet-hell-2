using Godot;
using System;

public class VirusEnemy : KinematicBody2D
{
    [Export] private float _speed = 200f;
    private Player _player;
    private AnimationPlayer _anim;
    private Vector2 _direction;

    private RayCast2D _rayCast;

    public override void _Ready()
    {
        _player = GetNode<Player>("/root/World/Player");
        _rayCast = GetNode<RayCast2D>("RayCast2D");
        _anim = GetNode<AnimationPlayer>("AnimationPlayer");
        _anim.Play("Charge");

        _direction = Position.DirectionTo(_player.Position);
        _rayCast.CastTo = Position.DirectionTo(_player.Position) * 25f;
        LookAt(_player.Position);
        RotationDegrees += 270f;
    }

    public override void _PhysicsProcess(float delta)
    {
        Position += _direction * delta * _speed;

        if (Position.x < 0 || Position.x > 512 || Position.y > 600 || Position.y < 0f)
        {
            QueueFree();
        }

        if (_rayCast.IsColliding())
        {
            Node collider = _rayCast.GetCollider() as Node;
            if (collider is Player)
            {
                GetTree().ChangeScene("res://GameOver/GameOver.tscn");
            }
        }
    }
}
