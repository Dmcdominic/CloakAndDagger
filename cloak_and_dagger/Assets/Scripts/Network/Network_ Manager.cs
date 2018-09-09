using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Network_Manager : MonoBehaviour {

    NetworkClient _client;
    private string ip;
    private int port;

    public void set_ip(string ip)
    {
    	this.ip = ip;
    }

    public void set_port(int port)
    {
    	this.port = port;
    }

	public void serve()
    {
        NetworkServer.Listen(port);
    }

    public void connect()
    {
    	_client = new NetworkClient();
    	_client.RegisterHandler(MsgType.Connect,connect_callback);
    	_client.Connect(ip,port);
    }

    public void host()
    {
    	_client = ClientScene.ConnectLocalServer();
    	_client.RegisterHandler(MsgType.Connect,connect_callback);

    }

    public void connect_callback(NetworkMessage msg)
    {
    	print("connected to server");
    }

}
