using Godot;
using System;

public class PlayerInfo : Node
{
    private Player _player;

    public float Kills = 0f;
    public float XP = 0f;
    public float Health = 0f;

    public override void _Ready()
    {
        _player = GetNodeOrNull<Player>("../Player");
        if (_player != null)
        {
            _player.Health = Health;
            _player.XP = XP;
            _player.Kills = Kills;
        }
    }

    public override void _Process(float delta)
    {
        if (_player != null)
        {
            Kills = _player.Kills;
            XP = _player.XP;
            Health = _player.Health;
        }
    }
}
