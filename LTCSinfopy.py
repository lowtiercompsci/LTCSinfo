import psutil
import wmi
import socket
import subprocess

def cpu_info():
    print("Processor (CPU) Information:")
    w = wmi.WMI()
    for processor in w.query("SELECT * FROM Win32_Processor"):
        print(f"Processor ID: {processor.ProcessorId}")
        print(f"Processor Manufacturer: {processor.Manufacturer}")
        print(f"Processor Name: {processor.Name}")
        print(f"Number of Cores: {processor.NumberOfCores}")
        print(f"Number of Threads: {processor.NumberOfLogicalProcessors}")
        print(f"Processor Architecture: {processor.Architecture}")
        print(f"Clock Speed: {processor.MaxClockSpeed} MHz")
        print(f"L1 Cache Size: {processor.L1CacheSize} KB")
        print(f"L2 Cache Size: {processor.L2CacheSize} KB")

def ram_info():
    print("\nRAM Information:")
    for mem in psutil.virtual_memory():
        if isinstance(mem, psutil._common.svmem):
            print(f"Total Visible Memory: {mem.total / (1024 * 1024)} MB")
            print(f"Free Physical Memory: {mem.available / (1024 * 1024)} MB")
            print(f"Total Virtual Memory: {mem.total / (1024 * 1024)} MB")
            print(f"Free Virtual Memory: {mem.available / (1024 * 1024)} MB")

def network_info():
    print("\nNetwork Information:")
    for interface, addrs in psutil.net_if_addrs().items():
        for addr in addrs:
            if addr.family == socket.AF_INET:
                print(f"IP Address: {addr.address}")
                print(f"Subnet Mask: {addr.netmask}")

def gpu_info():
    print("GPU Information:")
    w = wmi.WMI()
    for video_controller in w.query("SELECT * FROM Win32_VideoController"):
        print(f"Name: {video_controller.Name}")
        print(f"VRAM: {int(video_controller.AdapterRAM) / 1024 / 1024} MB")
        print(f"Video Processor: {video_controller.VideoProcessor}")
        print(f"Resolution: {video_controller.VideoModeDescription}")
        print(f"Driver Version: {video_controller.DriverVersion}")
        print(f"Driver Date: {video_controller.DriverDate}")

def storage_info():
    print("\nStorage Information:")
    w = wmi.WMI()
    for disk in w.query("SELECT * FROM Win32_DiskDrive"):
        print(f"Drive Model: {disk.Model}")
        print(f"Drive Interface: {disk.InterfaceType}")
        print(f"Drive Size: {int(disk.Size) / 1024 / 1024 / 1024} GB")
        print(f"Drive Serial Number: {disk.SerialNumber}")
        print(f"Drive Partitions: {disk.Partitions}")
        print(f"Drive Signature: {disk.Signature}")
        print(f"Drive Firmware: {disk.FirmwareRevision}")
        print(f"Drive Type: {disk.MediaType}")

def motherboard_info():
    print("\nMotherboard Information:")
    w = wmi.WMI()
    for baseboard in w.query("SELECT * FROM Win32_BaseBoard"):
        print(f"Motherboard Manufacturer: {baseboard.Manufacturer}")
        print(f"Motherboard Product: {baseboard.Product}")
        print(f"Motherboard Serial Number: {baseboard.SerialNumber}")
        print(f"Motherboard Version: {baseboard.Version}")
        print(f"Motherboard Name: {baseboard.Name}")
        print(f"Chipset: {baseboard.Chipset}")

def main():
    cpu_info()
    ram_info()
    network_info()
    gpu_info()
    storage_info()
    motherboard_info()

if __name__ == "__main__":
    main()
