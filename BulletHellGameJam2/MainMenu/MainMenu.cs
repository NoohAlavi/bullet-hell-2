using Godot;
using System;

public class MainMenu : Control
{
    public override void _Ready()
    {
        GetNode<Button>("Button").Connect("pressed", this, "OnButtonClicked");
    }

    private void OnButtonClicked()
    {
        GetTree().ChangeScene("res://World/World.tscn");
    }
}
