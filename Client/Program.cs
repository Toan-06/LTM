// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;
using System.Text;

Console.Title = "Đảo chuỗi Client \n";
IPAddress _ipaddress = IPAddress.Parse("127.0.0.1");
IPEndPoint _ipenpoint = new IPEndPoint(_ipaddress, 9000);
Socket _socketclient = new Socket(SocketType.Stream, ProtocolType.Tcp);
Thread.Sleep(1000);
_socketclient.Connect(_ipenpoint);
byte[] _data = new byte[1024];
int _recv = _socketclient.Receive(_data);
string _s = Encoding.UTF8.GetString(_data, 0, _recv);
Console.WriteLine(_s);
string _input;
while (true)
{
    Console.Write("Nhập: ");
    _input = Console.ReadLine();
    _data = Encoding.UTF8.GetBytes(_input);
    _socketclient.Send(_data, _data.Length, SocketFlags.None);
    if (_input.ToUpper().Equals("QUIT")) break;
    _data = new byte[1024];
    _recv = _socketclient.Receive(_data);
    string DaoChuoi = Encoding.UTF8.GetString(_data, 0, _recv);
    Console.WriteLine($"{DaoChuoi}");
}
_socketclient.Disconnect(true);
_socketclient.Close();