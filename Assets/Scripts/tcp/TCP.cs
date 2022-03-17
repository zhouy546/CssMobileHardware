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

//    private bool isConnected;

    private string perviousIP;

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

    public void shutDownSocket()
    {
        if (tcpClient != null)
        {
            try
            {
                tcpClient.Shutdown(SocketShutdown.Both);

                tcpClient.Close();

                tcpClient = null;
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.ToString());
            }


        }
    }

    public async Task Connect(string ip,int port)
    {
        tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress ipaddress = IPAddress.Parse(ip);
        perviousIP = ip;
        EndPoint point = new IPEndPoint(ipaddress, port);
        tcpClient.ReceiveTimeout = 500;
        tcpClient.SendTimeout = 500;

        try
        {
            //isConnected = true;       
            tcpClient.Connect(point);//ͨ��IP�Ͷ˿ں�����λһ����Ҫ���ӵķ�������
        }
        catch (Exception e)
        {
           // isConnected = false;

            tcpClient = null;
            Debug.Log(e);
        }

        await Task.Yield();
    }



    public async Task sendMsg(string ip, int port,string msg)
    {
        try
        {
            if (perviousIP == ip && tcpClient != null)
            {
                if (tcpClient.IsBound)
                {
                    tcpClient.Send(Encoding.Default.GetBytes(msg));
                    Debug.Log("���Ӵ�IP�ظ���ֱ�ӷ���");

                    await Task.Delay(1000);
                }
                else
                {
                    await Connect(ip, port);

                    if (tcpClient.IsBound)
                    {
                        tcpClient.Send(Encoding.Default.GetBytes(msg));

                        await Task.Delay(1000);
                    }
                }
            }
            else
            {
                if (tcpClient != null)
                {
                    shutDownSocket();

                }

                await Connect(ip, port);
                if (tcpClient.IsBound)
                {
                    Debug.Log("��һ�����Ӵ�IP");

                    tcpClient.Send(Encoding.Default.GetBytes(msg));

                    await Task.Delay(1000);
                }
            }
        }
        catch (Exception e)
        {

            Debug.LogWarning(e.ToString());
        }
   

    }

    public async Task sendHEXMsg(string ip, int port, string msg)
    {
        try
        {
            if (perviousIP == ip && tcpClient != null)
            {
                Debug.Log("isbound?" + tcpClient.IsBound);

                if (tcpClient.IsBound)
                {
                    byte[] b = Utility.strToToHexByte(msg);
                    tcpClient.Send(b);
                    Debug.Log("���Ӵ�IP�ظ���ֱ�ӷ���");

                    await Task.Delay(1000);
                }
                else
                {
                    await Connect(ip, port);

                    if (tcpClient.IsBound)
                    {
                        byte[] b = Utility.strToToHexByte(msg);
                        tcpClient.Send(b);

                        await Task.Delay(1000);
                    }
                }
            }
            else
            {
                if (tcpClient != null)
                {
                    shutDownSocket();

                }

                await Connect(ip, port);
                if (tcpClient.IsBound)
                {
                    Debug.Log("isbound?" + tcpClient.IsBound);
                    Debug.Log("��һ�����Ӵ�IP");

                    byte[] b = Utility.strToToHexByte(msg);


                    tcpClient.Send(b);

                    await Task.Delay(1000);
                }
            }
        }
        catch (Exception E)
        {
            Debug.LogWarning(E.ToString());

        }



        //await Connect(ip, port);

        //if (isConnected)
        //{

        //    byte[] b = Utility.strToToHexByte(msg);
        //    tcpClient.Send(b);
        //    //receive();
        //    await Task.Delay(1000);

        //    //  tcpClient.Shutdown(SocketShutdown.Both);
        //    tcpClient.Close();
        //}

        //tcpClient = null;

    }

    public async Task sendudpHEXMsg(string ip, int port, string msg)
    {
        UDP.udp_Send(msg, ip, port);

        await Task.Delay(1000);
    }

    private void receive()
    {
        byte[] data = new byte[1024];
        //����һ��byte���飬���ڽ������ݡ�length��ʾ�����˶����ֽڵ�����
    
        try
        {
            int length = tcpClient.Receive(data);
            Revmessage = Encoding.Default.GetString(data, 0, length);//ֻ�����յ������ݽ���ת��
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        //Debug.Log(Revmessage);
    }

}
