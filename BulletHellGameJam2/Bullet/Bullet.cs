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
        Connect("area_entered", this, "OnAreaEntered");
        GD.Randomize();
        GetNode<AudioStreamPlayer2D>("Sound").PitchScale = GD.Randf() + 1f;
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

    public override void _Process(float delta)
    {
        if (Position.y < 0f || Position.y > 600)
        {
            QueueFree();
        }
    }

    public void OnBodyEntered(PhysicsBody2D body)
    {
        if ((body is Enemy && !isEnemyBullet))
        {
            Enemy e = body as Enemy;
            if (!e.IsDead)
            {
                e.IsDead = true;
                e.Anim.Play("Uncorrupt");
                e.KillTimer.Start();
                QueueFree();
                GetNode<Player>("/root/World/Player").Kills++;
            }
        }

        if ((body is Player && isEnemyBullet))
        {
            Player p = body as Player;
            p.Health--;
            p.HurtParticles.Emitting = true;
            p.Hurt();
            p.ParticlesTimer.Start();
            QueueFree();
        }

        if (body is Boss && !isEnemyBullet)
        {
            Boss b = body as Boss;
            b.Damage();
            QueueFree();
        }
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area is VirusEnemy && !isEnemyBullet)
        {
            area.QueueFree();
            SetDeferred("QueueFree", this);
        }
    }
}
