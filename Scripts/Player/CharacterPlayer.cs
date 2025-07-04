using Godot;
using System;

public partial class CharacterPlayer : CharacterBody2D
{
	[Export]
	public float Gravity = -2f;

	[Export]
	public float Speed = 400f;

	[Export]
	public float MaxMovementSpeed = 300f;

	[Export]
	public float JumpPower = 200f;

	[Export]
	public float Friction = 0.99f;

	[Export]
	public float AirFriction = 0.99f;

	[Export]
	public PackedScene BulletScene = null;

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		if (!IsMultiplayerAuthority()) return;

		if (Position.Y > 2000) Position = -Position;

		if (!IsOnFloor()) 
		{ 
			Velocity -= new Vector2(0, Gravity);

			Vector2 TempVel3 = Velocity;
			TempVel3.X *= AirFriction;
			Velocity = TempVel3;
		}
		else
		{
			Vector2 TempVel = Velocity;
			TempVel.Y = 0;
			Velocity = TempVel;

			Vector2 TempVel2 = Velocity;
			TempVel2.X *= Friction;
			Velocity = TempVel2;
		}

		if (Input.IsActionPressed("Up") && IsOnFloor()) Velocity -= new Vector2(0, JumpPower);

		if (Input.IsActionPressed("Right") && Velocity.X <  MaxMovementSpeed) Velocity += new Vector2( Speed * (float)delta, 0);
		if (Input.IsActionPressed("Left")  && Velocity.X > -MaxMovementSpeed) Velocity += new Vector2(-Speed * (float)delta, 0);

		MoveAndSlide();
    }
}
