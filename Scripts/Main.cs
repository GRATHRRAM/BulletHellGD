using Godot;
using System;
using static Godot.TextServer;

public partial class Main : Node2D
{
	public override void _Ready()
	{
		Multiplayer.PeerConnected += OnPeerConnected;
		Multiplayer.PeerDisconnected += OnPeerDisconnected;
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		if (Input.IsActionJustPressed("HideGui")) 
			GetNode<CanvasLayer>("CanvasLayer").Visible = !GetNode<CanvasLayer>("CanvasLayer").Visible;
	}

	public void _on_host_pressed()
	{
		string Port = GetNode<TextEdit>("CanvasLayer/Control/Port").Text;

		if (!Port.IsValidInt())
		{
			GD.Print("Main.cs Invalid Port!!!");
			return;
		}

		ENetMultiplayerPeer peer = new ENetMultiplayerPeer();
		peer.CreateServer(Port.ToInt());
		Multiplayer.MultiplayerPeer = peer;
		GetNode<Label>("CanvasLayer/Control/hosting").Visible = true;

		GetNode<Node2D>("PlayerHolder").Call("SpawnPlayer", 1, Multiplayer.GetUniqueId());
	}

	public void _on_join_pressed()
	{
		string Port = GetNode<TextEdit>("CanvasLayer/Control/Port").Text;
		string ip = GetNode<TextEdit>("CanvasLayer/Control/Ip").Text;

		if (!Port.IsValidInt())
		{
			GD.Print("Main.cs Invalid Port!!!");
			return;
		}

		if (!ip.IsValidIPAddress())
		{
			GD.Print("Main.cs Invalid ip Address!!!");
			return;
		}

		ENetMultiplayerPeer peer = new ENetMultiplayerPeer();
		peer.CreateClient(ip, Port.ToInt());
		Multiplayer.MultiplayerPeer = peer;
	}


	private void OnPeerConnected(long id)
	{
		GD.Print($"Peer connected: {id}");

		if (Multiplayer.IsServer())
		{
			GetNode<Node2D>("PlayerHolder").Call("SpawnPlayer", 1, id);
		}
	}

	private void OnPeerDisconnected(long id)
	{
		GD.Print($"Peer disconnected: {id}");
		GetNode<Node2D>("PlayerHolder").Call("FreePlayer", id);
	}
}
