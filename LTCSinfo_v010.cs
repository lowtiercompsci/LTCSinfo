using System;
using System.Management;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.IO;
using System.Text;

public static class SystemInfo
{
    public static void CPU()
    {
        Console.WriteLine("Processor (CPU) information:");
        var searcher = new ManagementObjectSearcher("select * from Win32_Processor");

        foreach (ManagementObject obj in searcher.Get())
        {
            Console.WriteLine($"Processor ID: {obj["ProcessorId"]}");
            Console.WriteLine($"Processor manufacturer: {obj["Manufacturer"]}");
            Console.WriteLine($"Processor name: {obj["Name"]}");
            Console.WriteLine($"Number of cores: {obj["NumberOfCores"]}");
            Console.WriteLine($"Number of threads: {obj["NumberOfLogicalProcessors"]}");
            Console.WriteLine($"Processor architecture: {obj["Architecture"]}");
            Console.WriteLine($"Clock Speed: {obj["MaxClockSpeed"]} MHz");
            Console.WriteLine($"L1 Cache Size: {obj["L1CacheSize"]} KB");
            Console.WriteLine($"L2 Cache Size: {obj["L2CacheSize"]} KB");
        }
    }

    public static void RAM()
    {
        Console.WriteLine("\nRAM information:");
        var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");

        foreach (ManagementObject obj in searcher.Get())
        {
            Console.WriteLine($"Capacity: {obj["Capacity"]} bytes");
            Console.WriteLine($"Memory Type: {obj["MemoryType"]}");
            Console.WriteLine($"Speed: {obj["Speed"]} MHz");
            Console.WriteLine($"Manufacturer: {obj["Manufacturer"]}");
        }

        searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");

        foreach (ManagementObject obj in searcher.Get())
        {
            Console.WriteLine($"Total Visible Memory: {obj["TotalVisibleMemorySize"]} KB");
            Console.WriteLine($"Free Physical Memory: {obj["FreePhysicalMemory"]} KB");
            Console.WriteLine($"Total Virtual Memory: {obj["TotalVirtualMemorySize"]} KB");
            Console.WriteLine($"Free Virtual Memory: {obj["FreeVirtualMemory"]} KB");
        }
    }

    public static void NET()
    {
        Console.WriteLine("\nNetwork information:");
        foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (ni.OperationalStatus == OperationalStatus.Up)
            {
                IPInterfaceProperties properties = ni.GetIPProperties();
                foreach (UnicastIPAddressInformation address in properties.UnicastAddresses)
                {
                    if (address.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        Console.WriteLine($"IP Address: {address.Address}");
                        Console.WriteLine($"Subnet Mask: {address.IPv4Mask}");
                    }
                }
            }
        }
    }

    public static void GetGPUInfo()
    {
        Console.WriteLine("GPU information:");
        var searcher = new ManagementObjectSearcher("select * from Win32_VideoController");

        foreach (ManagementObject obj in searcher.Get())
        {
            Console.WriteLine($"Name: {obj["Name"]}");
            Console.WriteLine($"VRAM: {Convert.ToInt64(obj["AdapterRAM"]) / 1024 / 1024} MB");
            Console.WriteLine($"Video Processor: {obj["VideoProcessor"]}");
            Console.WriteLine($"Resolution: {obj["VideoModeDescription"]}");
            Console.WriteLine($"Driver Version: {obj["DriverVersion"]}");
            Console.WriteLine($"Driver Date: {obj["DriverDate"]}");
        }
    }

    public static void GetStorageInfo()
    {
        Console.WriteLine("\nStorage information:");
        var searcher = new ManagementObjectSearcher("select * from Win32_DiskDrive");

        foreach (ManagementObject obj in searcher.Get())
        {
            Console.WriteLine($"Drive Model: {obj["Model"]}");
            Console.WriteLine($"Drive Interface: {obj["InterfaceType"]}");
            Console.WriteLine($"Drive Size: {Convert.ToInt64(obj["Size"]) / 1024 / 1024 / 1024} GB");
            Console.WriteLine($"Drive Serial Number: {obj["SerialNumber"]}");
            Console.WriteLine($"Drive Partitions: {obj["Partitions"]}");
            Console.WriteLine($"Drive Signature: {obj["Signature"]}");
            Console.WriteLine($"Drive Firmware: {obj["FirmwareRevision"]}");
            Console.WriteLine($"Drive Type: {obj["MediaType"]}");
        }
    }

    public static void GetMotherboardInfo()
    {
        Console.WriteLine("\nMotherboard information:");
        var searcher = new ManagementObjectSearcher("select * from Win32_BaseBoard");

        foreach (ManagementObject obj in searcher.Get())
        {
            Console.WriteLine($"Motherboard Manufacturer: {obj["Manufacturer"]}");
            Console.WriteLine($"Motherboard Product: {obj["Product"]}");
            Console.WriteLine($"Motherboard Serial Number: {obj["SerialNumber"]}");
            Console.WriteLine($"Motherboard Version: {obj["Version"]}");
            Console.WriteLine($"Motherboard Name: {obj["Name"]}");
            Console.WriteLine($"Chipset: {obj["Chipset"]}");
        }
    }
}