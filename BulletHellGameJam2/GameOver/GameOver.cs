using Godot;
using System;

public class GameOver : Control
{

    public override void _Ready()
    {
        GetNode<Button>("Button").Connect("pressed", this, "OnButtonPressed");
    }

    private void OnButtonPressed()
    {
        GetTree().ChangeScene("res://World/World.tscn");
    }
}
