﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Network_Connectur : NetworkBehaviour {



    NetworkClient _client;

    private string ip;
    private string port;
    private int port_int;

    [SerializeField]
    string_event_object ip_event;

    [SerializeField]
    string_event_object port_event;

    [SerializeField]
    event_object connect_event;

    [SerializeField]
    event_object host_event;

    [SerializeField]
    GameObject character;



	public void Start()
	{
		ip_event.e.AddListener(set_ip);
		port_event.e.AddListener(set_port);
		connect_event.e.AddListener(connect);
		host_event.e.AddListener(host);
	}


    public void set_ip(string ip)
    {
    	this.ip = ip;
    }

    public void set_port(string in_port)
    {
    	this.port = in_port;
    	port_int = int.Parse(port);
    }

	public void serve()
    {
        NetworkServer.Listen(port_int);
    }

    public void connect()
    {
    	_client = new NetworkClient();
    	_client.RegisterHandler(MsgType.Connect,connect_callback);
    	_client.Connect(ip,port_int);
    	SceneManager.LoadScene(1);
    	Cmd_spawn_character();
    }

    [Command]
    public void Cmd_spawn_character()
    {
    	GameObject my_char = GameObject.Instantiate(character);
    	NetworkServer.Spawn(my_char);
    }

    public void host()
    {
    	_client = ClientScene.ConnectLocalServer();
    	_client.RegisterHandler(MsgType.Connect,connect_callback);
    	SceneManager.LoadScene(1);
    	GameObject.Instantiate(character);

    }

    public void connect_callback(NetworkMessage msg)
    {
    	print("connected to server");
    }

}