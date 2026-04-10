// See https://aka.ms/new-console-template for more informations
using System.Net;
using System.Net.Sockets;
using System.Text;

Console.Title = "Đảo chuỗi Server \n";
IPAddress _ipaddress = IPAddress.Parse("127.0.0.1");
IPEndPoint _ipenpoint = new IPEndPoint(_ipaddress, 9000);
Socket _socketserver = new Socket(AddressFamily.InterNetwork,
    SocketType.Stream, ProtocolType.Tcp);
_socketserver.Bind(_ipenpoint);
_socketserver.Listen(10);
Console.WriteLine("Chờ kết nối từ client");
Socket _socketclient = _socketserver.Accept();
Console.WriteLine("Chấp nhận kết nối từ {0}",
    _socketclient.RemoteEndPoint.ToString());
string _s = "Chào mừng bạn đến với Server";
byte[] _data = new byte[1024];
_data = Encoding.UTF8.GetBytes(_s);
_socketclient.Send(_data, _data.Length, SocketFlags.None);
while (true)
{
    _data = new byte[1024];
    int _recv = _socketclient.Receive(_data);
    if (_recv == 0) break;
    _s = Encoding.UTF8.GetString(_data, 0, _recv).Trim();
    Console.WriteLine("Client gửi đến: {0}", _s);
    if (_s.ToUpper().Equals("QUIT")) break;
    string DaoChuoi = "";
    try
    {
        char[] arr = _s.ToCharArray();
        Array.Reverse(arr);
        DaoChuoi = new string(arr);
    }
    catch
    {
        DaoChuoi = "Sai định dạng";
    }
    string reply = DaoChuoi;
    _data = Encoding.UTF8.GetBytes(reply);
    _socketclient.Send(_data, _data.Length, SocketFlags.None);
}
_socketclient.Shutdown(SocketShutdown.Both);
_socketclient.Close();
_socketserver.Close();