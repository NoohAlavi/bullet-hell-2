using Godot;
using System;

public class PlayerInfo : Node
{
    private Player _player;

    public float Kills = 0f;
    public float XP = 0f;
    public float Health = 3f;
    public float Speed = 200f;
    public float Level = 1f;

    public override void _Ready()
    {
        _player = GetNodeOrNull<Player>("../World/Player");
        // if (_player != null)
        // {
        //     _player.Health = Health;
        //     _player.XP = XP;
        //     _player.Kills = Kills;
        //     _player.MaxSpeed = Speed;
        // }
    }

    public override void _Process(float delta)
    {
        if (_player != null && _player.Health > 0f)
        {
            Kills = _player.Kills;
            XP = _player.XP;
            Health = _player.Health;
            Speed = _player.MaxSpeed;
            Level = _player.Level;
        }
    }
}
