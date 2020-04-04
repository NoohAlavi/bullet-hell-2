using Godot;
using System;

public class Bullet : Area2D
{

    [Export] public float Speed = 1000f;
    [Export] public Vector2 Direction = new Vector2();
    public ColorRect Collider;

    [Export] public bool isEnemyBullet = false;

    public override void _Ready()
    {
        Connect("body_entered", this, "OnBodyEntered");
        GD.Randomize();
        float r = GD.Randf();
        float g = GD.Randf();
        float b = GD.Randf();
        GetNode<Sprite>("Sprite").Modulate = new Color(r, g, b);

        Collider = GetNode<ColorRect>("Collider");
    }

    public override void _PhysicsProcess(float delta)
    {
        Position += Direction * Speed * delta;
    }

    public void OnBodyEntered(PhysicsBody2D body)
    {
        if ((body is Enemy && !isEnemyBullet))
        {
            body.QueueFree();
            QueueFree();
        }

        if ((body is Player && isEnemyBullet))
        {
            Player p = body as Player;
            p.Health--;
            p.HurtParticles.Emitting = true;
            p.ParticlesTimer.Start();
            QueueFree();
        }
    }
}
