using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class NetClient : MonoBehaviour {

	

    public void Send()
    {
        const int BUFFER_SISE = 1024;
        byte[] readBuff = new byte[BUFFER_SISE];
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect("192.168.0.13", 10001);
        string str = "hello world!!";
        byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
        socket.Send(bytes);
        int count = socket.Receive(readBuff);
        str = System.Text.Encoding.UTF8.GetString(readBuff, 0, count);
        Debug.Log(str);
        socket.Close();
        
    }

    

   

}
