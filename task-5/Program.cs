using System.Net;

namespace SubnetCalculator;

internal class Program
{
    public static void IpCalculator(string ipInput, string cidrInput)
    {
        int cidr = int.Parse(cidrInput);
        uint mask = 0xFFFFFFFF << (32 - cidr);

        uint octet1 = (mask >> 24) & 0xFF;
        uint octet2 = (mask >> 16) & 0xFF;
        uint octet3 = (mask >> 8) & 0xFF;
        uint octet4 = (mask >> 0) & 0xFF;
        string maskStr = $"{octet1}.{octet2}.{octet3}.{octet4}";

        var parts = ipInput.Split('.');
        int firstParts = int.Parse(parts[0]);
        
        string networkClass = GetNetworkClass(firstParts);

        int ipOctet1 = int.Parse(parts[0]);
        int ipOctet2 = int.Parse(parts[1]);
        int ipOctet3 = int.Parse(parts[2]);
        int ipOctet4 = int.Parse(parts[3]);
        uint uintIp = ((uint)ipOctet1 << 24) | ((uint)ipOctet2 << 16) | ((uint)ipOctet3 << 8) | (uint)ipOctet4;
        uint network = uintIp & mask;
        string networkStr = $"{(network >> 24) & 0xFF}.{(network >> 16) & 0xFF}.{(network >> 8) & 0xFF}.{network & 0xFF}";
        uint broadcast = network | ~mask;
        string broadcastStr = $"{(broadcast >> 24) & 0xFF}.{(broadcast >> 16) & 0xFF}.{(broadcast >> 8) & 0xFF}.{broadcast & 0xFF}";
        uint firstHost = network + 1;
        uint lastHost = broadcast - 1;
        string firstHostStr = $"{(firstHost >> 24) & 0xFF}.{(firstHost >> 16) & 0xFF}.{(firstHost >> 8) & 0xFF}.{firstHost & 0xFF}";
        string lastHostStr = $"{(lastHost >> 24) & 0xFF}.{(lastHost >> 16) & 0xFF}.{(lastHost >> 8) & 0xFF}.{lastHost & 0xFF}";
        double hostCount = Math.Pow(2, 32 - cidr) - 2;

        Console.WriteLine($"\nРезультати:");
        Console.WriteLine($"IP-адреса: {ipInput}");
        Console.WriteLine($"Маска підмережі: {maskStr}");
        Console.WriteLine($"Мережева адреса: {networkStr}");
        Console.WriteLine($"Broadcast адреса: {broadcastStr}");
        Console.WriteLine($"Перший хост: {firstHostStr}");
        Console.WriteLine($"Останній хост: {lastHostStr}");
        Console.WriteLine($"Кількість хостів: {hostCount}");
        Console.WriteLine($"Клас мережі: {networkClass}");
    }

    public static string GetNetworkClass(int firstParts)
    {
        if (firstParts >= 1 && firstParts <= 126)
            return "A";
        if (firstParts >= 128 && firstParts <= 191)
            return "B";
        if (firstParts >= 192 && firstParts <= 223)
            return "C";
        return "";
    }

    static void Main(string[] args)
    {
        Console.Write("Введіть IP-адресу: ");
        string ip = Console.ReadLine();

        Console.Write("Введіть маску підмережі (CIDR): ");
        string cidr = Console.ReadLine();
        
        IpCalculator(ip, cidr);
    }   
}
