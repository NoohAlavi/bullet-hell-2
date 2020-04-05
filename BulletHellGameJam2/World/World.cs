using Godot;
using System;

public class World : Node2D
{
    private PackedScene _enemyScene;
    private PackedScene _virusScene;
    private Timer _spawnTimer;

    private Vector2 _screenSize;
    private Player _player;
    private Camera2D _camera;

    public override void _Ready()
    {

        GD.Randomize();

        _enemyScene = ResourceLoader.Load<PackedScene>("res://Enemy/Enemy.tscn");
        _virusScene = ResourceLoader.Load<PackedScene>("res://VirusEnemy/VirusEnemy.tscn");

        _spawnTimer = GetNode<Timer>("SpawnTimer");
        _spawnTimer.Connect("timeout", this, "SpawnEnemies");

        _screenSize = GetViewport().Size;

        _player = GetNode<Player>("Player");
        _camera = GetNode<Camera2D>("Camera2D");

        SpawnEnemies();
    }

    public override void _PhysicsProcess(float delta)
    {
        foreach (Node2D s in GetTree().GetNodesInGroup("ScrollableItems"))
        {
            s.Position = new Vector2(s.Position.x, s.Position.y + 3f);
        }

        if (GetNode("EnemyHolder").GetChildren().Count == 0)
        {
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        for (var i = 0; i < 5; i++)
        {
            Enemy enemy = _enemyScene.Instance() as Enemy;
            GetNode("EnemyHolder").AddChild(enemy);
            float randX = GD.Randi() % _screenSize.x + 32;
            enemy.Position = new Vector2(randX, 0);
        }
        if (_player.Kills >= 0f && GD.Randf() < 0.33f)
        {
            VirusEnemy v = _virusScene.Instance() as VirusEnemy;
            GetNode("EnemyHolder").AddChild(v);
            float randX = GD.Randi() % _screenSize.x + 32;
            v.Position = new Vector2(randX, 0);
        }
    }
}
