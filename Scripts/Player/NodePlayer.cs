using BulletHellGD.Scripts;
using Godot;
using System;

public partial class NodePlayer : Node2D
{
	[Export]
	public float BulletSpeed = 250f;

	[Export]
	private PackedScene BulletScene = null;

	private Node BulletHolder = null;

	public override void _EnterTree()
	{
		base._EnterTree();

		MeshInstance2D Mesh = GetNode<MeshInstance2D>("CharacterBody2D/MeshInstance2D");
		Mesh.Modulate = GlobalFunctions.RandomColor();

		SetMultiplayerAuthority(Int32.Parse(Name));

		BulletHolder = GetParent().GetNode("BulletHolder");
	}

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

		if(Input.IsActionJustPressed("Shoot"))
		{
			Vector2 PlayerPos = GetNode<CharacterBody2D>("CharacterBody2D").GlobalPosition;

			Node2D Bullet = (Node2D) BulletScene.Instantiate();

            Bullet.Set("Position", PlayerPos);
            Bullet.Set("Velocity", PlayerPos.DirectionTo(GetGlobalMousePosition()) * BulletSpeed);

            BulletHolder.AddChild(Bullet, true);

			GD.Print($"{Bullet.GlobalPosition}");
		}
    }
}
