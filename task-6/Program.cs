using System.Net.NetworkInformation;

namespace NetworkScanner;

internal class Program
{
    public static bool PingHost(string ipInput)
    {
        var ping = new Ping();
        PingReply reply = ping.Send(ipInput);
        
        if (reply.Status == IPStatus.Success)
            return true;
        
        return false;
    }

    public static PingReply GetPingTime(string ipInput)
    {
        var ping = new Ping();
        PingReply reply = ping.Send(ipInput);

        return reply;
    }

    static void Main(string[] args)
    {
        Parallel.For(1, 255, i =>
        {
            string ip = $"192.168.0.{i}";
            PingReply reply = GetPingTime(ip);
            if (PingHost(ip))
            {
                Console.WriteLine($"{ip} - активний | {reply.RoundtripTime}ms");
            }
        });
    }
}
