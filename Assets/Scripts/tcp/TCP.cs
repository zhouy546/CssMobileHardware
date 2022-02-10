using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TCP : MonoBehaviour
{
    public static TCP INSTANCE;

    public  Socket tcpClient;

    private string Revmessage;

    private bool isConnected;

    public bool m_lock;
    // Start is called before the first frame update
    void Start()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }

    }

    // Update is called once per frame
    async void  Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
          await  sendMsg("127.0.0.1", 28010, "123");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            await sendHEXMsg("127.0.0.1", 28010, "00000000000601050801FF00");
        }

        if (ValueSheet.callBackList.Count > 0)
        {
            if (!m_lock)
            {
                m_lock = true;

                await ValueSheet.callBackList.Dequeue().Invoke();

                m_lock = false;
            }
            //Debug.Log(ValueSheet.callBackList.Count);

        }

    }


    public async Task Connect(string ip,int port)
    {
        tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress ipaddress = IPAddress.Parse(ip);
        EndPoint point = new IPEndPoint(ipaddress, port);
        tcpClient.ReceiveTimeout = 500;
        tcpClient.SendTimeout = 500;

        try
        {
            isConnected = true;       
            tcpClient.Connect(point);//通过IP和端口号来定位一个所要连接的服务器端
        }
        catch (Exception e)
        {
            isConnected = false;
            Debug.Log(e);
        }

        await Task.Yield();
    }

    public async Task sendMsg(string ip, int port,string msg)
    {
        await Connect(ip, port);

        if (isConnected)
        {
            tcpClient.Send(Encoding.Default.GetBytes(msg));
            //receive();
            await Task.Delay(1000);


          //  tcpClient.Shutdown(SocketShutdown.Both);
            tcpClient.Close();
        }

        tcpClient = null;

 
    }

    public async Task sendHEXMsg(string ip, int port, string msg)
    {
        await Connect(ip, port);

        if (isConnected)
        {

            byte[] b = Utility.strToToHexByte(msg);
            tcpClient.Send(b);
            //receive();
            await Task.Delay(1000);

          //  tcpClient.Shutdown(SocketShutdown.Both);
            tcpClient.Close();
        }

        tcpClient = null;

    }

    private void receive()
    {
        byte[] data = new byte[1024];
        //传递一个byte数组，用于接收数据。length表示接收了多少字节的数据
    
        try
        {
            int length = tcpClient.Receive(data);
            Revmessage = Encoding.Default.GetString(data, 0, length);//只将接收到的数据进行转化
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        //Debug.Log(Revmessage);
    }

}
