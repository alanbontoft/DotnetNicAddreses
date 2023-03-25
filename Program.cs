using System.Net.NetworkInformation;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;

namespace firstconsole
{
    class Program
    {

        
        static void Main(string[] args)
        {
            SerialPort? sp = null;

            try
            {

                var port = int .Parse(args[0]);    

                // sp = new SerialPort("/dev/ttyS0", 115200, Parity.None, 8, StopBits.One);

                // sp.Open();

                


                Dictionary<string, IPAddress> nics = new();

                foreach(NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    Console.WriteLine(ni.Name);
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            Console.WriteLine(ip.Address);
                            nics.Add(ni.Name, ip.Address);
                        }
                    }
                } 

                var name = "eth0";
                Console.WriteLine($"{name} is at {nics[name]}");
                name = "wlan0";
                Console.WriteLine($"{name} is at {nics[name]}");

                name = "eth0";
                Console.WriteLine($"Listening on {nics[name]}, port {port}");
                var listener = new TcpListener(nics[name], port);
                listener.Start();

                var client = listener.AcceptTcpClient();
                Console.WriteLine("Connected!");

                while(true)
                {
                    Console.WriteLine("Sleeping for 1");
                    Thread.Sleep(1000);
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            // sp?.Close();

        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Console.WriteLine("Data Received:");
            Console.Write(indata);
        }

    }
}
