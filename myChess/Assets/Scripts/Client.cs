using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net.Sockets;
using System;


public class Client : MonoBehaviour {

    public string clientName;

    private bool socketReady;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;

    private List<GameClient> players = new List<GameClient>();

    public bool isHost;

    public bool ConnectToServer(string host, int port)
    {
        if (socketReady)
            return false;
        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log(host);
            Debug.Log("Socket error " + e.Message);
        }

        return socketReady;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (socketReady)
        {
            if (stream.DataAvailable)
            {
                string data = reader.ReadLine();
                if (data != null)
                    OnIncomingData(data);
            }
        }
    }

    //send message to server

    public void Send(string data)
    {
        if (!socketReady)
            return;
        writer.WriteLine(data);
        writer.Flush();
    }


    //read message from server
    private void OnIncomingData(string data)
    {
        Debug.Log("client: "+ data);
        string[] aData = data.Split('|');

        switch (aData[0])
        {
            case "SWHO":
                for(int i = 1; i < aData.Length - 1; i++)
                {
                    UserConnected(aData[i], false);
                }
                Send("CWHO|" + clientName + "|" + ((isHost)?1:0).ToString());
                break;
            case "SCNN":
                UserConnected(aData[1], false);
                break;
            case "SSEL":
                BoardManager.Instance.SelectPiece(int.Parse(aData[2]), int.Parse(aData[3]));

                //BoardManager.Instance.MovePiece(int.Parse(aData[4]), int.Parse(aData[5]));

                break;
            case "CMOV":
                BoardManager.Instance.MovePiece(int.Parse(aData[1]), int.Parse(aData[2]));
                break;

            
        }
    }

    private void UserConnected(string name, bool host)
    {
        GameClient c = new GameClient();
        c.name = name;
        players.Add(c);

        if (players.Count == 2)
            GameManager.Instance.StartGame();
    }

    private void CloseSocket()
    {
        if (!socketReady)
            return;
        writer.Close();
        reader.Close();
        socket.Close();
        socketReady = false;
    }
    private void OnDisable()
    {
        CloseSocket();
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }
    public string formatMove(string[] aData)
    {
        string msg = "";
        switch (aData[1].GetType().ToString())
        {
            case "Pawn":
                msg += "p";
                break;
            case "Knight":
                msg += "k";
                break;
            case "Queen":
                msg += "q";
                break;
            case "Bishop":
                msg += "b";
                break;
            case "Rook":
                msg += "r";
                break;
            case "King":
                msg += "K";
                break;
        }
        return msg;
    }
}

public class GameClient
{
    public string name;
    public bool isHost;
}





