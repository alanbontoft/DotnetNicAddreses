using System.Net.NetworkInformation;

namespace firstconsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Dictionary<string, string> nics = new();

                foreach(NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    Console.WriteLine(ni.Name);
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            Console.WriteLine(ip.Address.ToString());
                            nics.Add(ni.Name, ip.Address.ToString());
                        }
                    }
                } 
                
                var name = "eth0";
                Console.WriteLine($"{name} is at {nics[name]}");
                name = "wlan0";
                Console.WriteLine($"{name} is at {nics[name]}");
                name = "wlan";
                Console.WriteLine($"{name} is at {nics[name]}");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
