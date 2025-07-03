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
	public float Bounce = 0.20f;

	public UInt16 PlayerInfo = 1;

	/*
		PlayerInfo
		0 = Net // to add
		1 = Player1 (keyboard)
		2 = Player2 (keyboard)
		3 = Player3 (GamePad) // to add
		4 = Player4 (GamePad) // to add
	*/


	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

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

		if (PlayerInfo == 1)
		{
			if (Input.IsActionPressed("Up1") && IsOnFloor()) Velocity -= new Vector2(0, JumpPower);

			if (Input.IsActionPressed("Right1") && Velocity.X <  MaxMovementSpeed) Velocity += new Vector2( Speed * (float)delta, 0);
			if (Input.IsActionPressed("Left1")  && Velocity.X > -MaxMovementSpeed) Velocity += new Vector2(-Speed * (float)delta, 0);
		}

		if (PlayerInfo == 2)
		{
			if (Input.IsActionPressed("Up2") && IsOnFloor()) Velocity -= new Vector2(0, JumpPower);

			if (Input.IsActionPressed("Right2") && Math.Abs(Velocity.X) < MaxMovementSpeed) Velocity += new Vector2( Speed * (float)delta, 0);
			if (Input.IsActionPressed("Left2")  && Math.Abs(Velocity.X) < MaxMovementSpeed) Velocity += new Vector2(-Speed * (float)delta, 0);
		}

		MoveAndSlide();
	}
}
