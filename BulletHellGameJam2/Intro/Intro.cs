using Godot;
using System;

public class Intro : Control
{
    public override void _Ready()
    {
        GetNode<Button>("Button").Connect("pressed", this, "OnButtonPressed");
    }

    public void OnButtonPressed()
    {
        GetTree().ChangeScene("res://World/World.tscn");
    }
}
