using BulletHellGD.Scripts;
using Godot;
using System;

public partial class NodePlayer : Node2D
{
	public override void _EnterTree()
	{
		base._EnterTree();

		MeshInstance2D Mesh = GetNode<MeshInstance2D>("CharacterBody2D/MeshInstance2D");
		Mesh.Modulate = GlobalFunctions.RandomColor();

		SetMultiplayerAuthority(Int32.Parse(Name));
	}
}
