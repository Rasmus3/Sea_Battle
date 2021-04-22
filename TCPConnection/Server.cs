using System;
using System.Net.Sockets;

namespace TCPConnection
{
    public class Server : Connection
    {
        public Server () { }
        
        public Server(string ipAddress, int port) : base(ipAddress, port) { }
        
        public void Start()
        {
            try
            {
                _socket.Bind(_ipEndPoint);
                _socket.Listen(10);
            }
            catch (ObjectDisposedException)
            {
                throw new Exception("Socket был закрыт");
            }
            catch (SocketException)
            {
                throw new Exception("Произошлп ошибка при попытке досткпа к сокету");
            }
        }

        public Connection NewClient()
        {
            Client cli;
            try
            {
                var socket = _socket.Accept();
                cli = new Client(socket);
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при установлении соединения с клиентом");
            }
            return cli;
        }
    }
}