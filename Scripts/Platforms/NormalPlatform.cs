using BulletHellGD.Scripts;
using Godot;
using System;

public partial class NormalPlatform : Node2D
{
	public override void _EnterTree()
	{
		base._EnterTree();

		MeshInstance2D Mesh = GetNode<MeshInstance2D>("StaticBody2D/MeshInstance2D");
		Mesh.Modulate = GlobalFunctions.RandomColor();
	}
}
