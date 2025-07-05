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

		GetNode<Node2D>("PlayerHolder").Call("SpawnPlayer", Multiplayer.GetUniqueId());

		GetNode<CanvasLayer>("CanvasLayer").Visible = false;
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
		var err = peer.CreateClient(ip, Port.ToInt());
		if(err != Error.Ok) { GD.Print(err); return; }

		Multiplayer.MultiplayerPeer = peer;

		GetNode<CanvasLayer>("CanvasLayer").Visible = false;
	}


	private void OnPeerConnected(long id)
	{
		GD.Print($"Peer connected: {id}");

		if (Multiplayer.IsServer())
		{
			GetNode<Node2D>("PlayerHolder").Call("SpawnPlayer", id);
		}
	}

	private void OnPeerDisconnected(long id)
	{
		GD.Print($"Peer disconnected: {id}");
		GetNode<Node2D>("PlayerHolder").Call("FreePlayer", id);
	}
}
