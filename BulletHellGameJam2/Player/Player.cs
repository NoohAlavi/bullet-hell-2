using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public Vector2 Velocity = new Vector2();
    [Export] public float MaxSpeed = 200f;
    [Export] public float Health = 3f;
    [Export] public float NumShots = 1f;
    [Export] public float XP = 0f;

    private float Speed;

    [Export] private PackedScene _bulletScene;

    private Vector2 _screenSize;
    private ColorRect _collision;
    private AnimationPlayer _anim;

    public CPUParticles2D HurtParticles;
    public Timer ParticlesTimer;

    private Sprite _background;

    public override void _Ready()
    {
        _bulletScene = ResourceLoader.Load<PackedScene>("res://Bullet/Bullet.tscn");
        _screenSize = GetViewport().Size;

        Speed = MaxSpeed;

        _collision = GetNode<ColorRect>("Collider/ColorRect");

        HurtParticles = GetNode<CPUParticles2D>("CPUParticles2D");
        HurtParticles.Emitting = false;

        ParticlesTimer = GetNode<Timer>("ParticlesTimer");
        ParticlesTimer.Connect("timeout", this, "HideParticles");

        _anim = GetNode<AnimationPlayer>("AnimationPlayer");

        _background = GetNode<Sprite>("/root/World/Background");
    }

    public override void _Process(float delta)
    {
        if (Health <= 0f)
        {
            GetTree().ChangeScene("res://GameOver/GameOver.tscn");
        }
        // GetNode<Label>("/root/World/HUD/LivesLabel").Text = "LIVES: " + Health.ToString();
        GetNode<Sprite>("/root/World/HUD/LivesBar").Frame = Convert.ToInt32(3f - Health);
        GetNode<TextureProgress>("/root/World/HUD/XPBar").Value = XP;
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

        if (Input.IsActionPressed("Focus"))
        {
            Speed = MaxSpeed / 2;
            _collision.Show();
            foreach (Bullet bullet in GetNode("/root/World/BulletHolder").GetChildren())
            {
                bullet.Collider.Show();
            }
            _anim.Play("Focus");
            _background.Texture = ResourceLoader.Load<Texture>("res://World/Background 1.png");
            _background.Modulate = new Color(1, 1, 1);
        }
        else
        {
            Speed = MaxSpeed;
            _collision.Hide();
            foreach (Bullet bullet in GetNode("/root/World/BulletHolder").GetChildren())
            {
                bullet.Collider.Hide();
            }
            _anim.Play("Idle");
            _background.Texture = ResourceLoader.Load<Texture>("res://World/Background 2.png");
            _background.Modulate = new Color(.5f, .5f, .5f);
        }
    }

    private void Shoot()
    {
        Bullet bullet = _bulletScene.Instance() as Bullet;
        GetNode("/root/World/BulletHolder").AddChild(bullet);
        bullet.Position = Position;
        bullet.Direction = Vector2.Up;
        bullet.Speed = 1000f;
    }

    private void HideParticles()
    {
        HurtParticles.Emitting = false;
    }
}
