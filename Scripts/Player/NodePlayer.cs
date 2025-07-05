using BulletHellGD.Scripts;
using Godot;
using System;

public partial class NodePlayer : Node2D
{
	[Export]
	public float BulletSpeed = 250f;

	[Export]
	private PackedScene BulletScene = null;

	private Node2D BulletHolder = null;

	public override void _EnterTree()
	{
		base._EnterTree();

		MeshInstance2D Mesh = GetNode<MeshInstance2D>("CharacterBody2D/MeshInstance2D");
		Mesh.Modulate = GlobalFunctions.RandomColor();

		SetMultiplayerAuthority(Int32.Parse(Name));

		BulletHolder = GetParent().GetNode<Node2D>("BulletHolder");

		var bulletHolder = GetParent().GetNode("BulletHolder");
		bulletHolder.SetMultiplayerAuthority(GetMultiplayerAuthority());
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		if (!IsMultiplayerAuthority()) return;

		if (Input.IsActionJustPressed("Shoot"))
		{
			Bullet _Bullet = (Bullet) BulletScene.Instantiate();
			
			_Bullet.Position = GetNode<CharacterBody2D>("CharacterBody2D").Position;
			_Bullet.Velocity = GetNode<CharacterBody2D>("CharacterBody2D").GlobalPosition.DirectionTo(GetGlobalMousePosition()) * BulletSpeed;

			_Bullet.Name = Multiplayer.GetUniqueId().ToString() + new Random().Next(0,10000).ToString();
			_Bullet.SetMultiplayerAuthority(Int32.Parse(Name));

			BulletHolder.AddChild(_Bullet, true);
		}
	}
}
