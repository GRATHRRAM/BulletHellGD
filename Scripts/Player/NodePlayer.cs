using BulletHellGD.Scripts;
using Godot;
using System;

public partial class NodePlayer : Node2D
{
	public UInt16 id = 0;

	public override void _EnterTree()
	{
		base._EnterTree();

		MeshInstance2D Mesh = GetNode<MeshInstance2D>("CharacterBody2D/MeshInstance2D");
		Mesh.Modulate = GlobalFunctions.RandomColor();

		id = GlobalFunctions.RandomId();
	}
}
