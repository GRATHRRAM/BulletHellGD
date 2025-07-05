using Godot;
using System;

public partial class Bullet : Node2D
{
	public Vector2 Velocity = Vector2.Zero;

	public int CreatorID = 0;

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		if (!IsMultiplayerAuthority()) return;
		Position += Velocity * (float) delta;
	}

	public void _on_timer_timeout()
	{
		QueueFree();
	}
}
