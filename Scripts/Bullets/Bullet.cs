using Godot;
using System;

public partial class Bullet : Node2D
{
	public Vector2 Velocity = Vector2.Zero;

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		Position += Velocity * (float) delta;
	}

	public void _on_timer_timeout()
	{
		QueueFree();
	}
}
