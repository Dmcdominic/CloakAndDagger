using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Network_Connectur : NetworkManager {



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
		DontDestroyOnLoad(gameObject);
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
    	_client = StartClient();
    	_client.RegisterHandler(MsgType.Connect,connect_callback);
        _client.RegisterHandler(MsgType.Error,error_callback);
    	if(ip == null) ip = "localhost";
        print(ip + " - " + port_int.ToString());
    	_client.Connect(ip,port_int);
        print(_client.isConnected);
    	SceneManager.LoadScene(1);
    }



    public void host()
    {
    	_client = StartHost();
    	_client.RegisterHandler(MsgType.Connect,connect_callback);
    	SceneManager.LoadScene(1);

    }

    public void error_callback(NetworkMessage msg)
    {
        string error_msg = "";
        string temp = "";
        temp = msg.reader.ReadString();
        while(temp != "")
        {
            error_msg += temp;
            temp = msg.reader.ReadString();
            Debug.Log("oops");
        }
        Debug.Log("Error: " + error_msg);
    }

    public void connect_callback(NetworkMessage msg)
    {
    	Debug.Log("connected");
    }

}
