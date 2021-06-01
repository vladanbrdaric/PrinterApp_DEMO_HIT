using PrinterAgentLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterAgentLibrary.Printers
{
    public class Printers
    {
        public PrinterFullModel GetPrinter1()
        {
            PrinterFullModel printer = new PrinterFullModel()
            {
                IpAddress = "10.10.10.10",
                Description = "Laser Skrivare",
                Location = "Hall 1",
                Manufacturer = "Xerox",
                Model = "CX5240",
                UpTime = "8d 22h 17m",
                LifeCount = "9876",
                CountSinceReboot = "987",
                Color = "Color",
                Status = "Running",
                Black = 32,
                Cyan = 98,
                Magenta = 63,
                Yellow = 45,
                Tray1 = 91,
                Tray2 = 89,
                Tray3 = 44,
                Tray4 = 23
            };

            return printer;
        }

        public PrinterFullModel GetPrinter2()
        {
            PrinterFullModel printer = new PrinterFullModel()
            {
                IpAddress = "10.10.10.20",
                Description = "Svart Skrivare",
                Location = "Ekonomi",
                Manufacturer = "HP",
                Model = "JetSky",
                UpTime = "16d 8h 56m",
                LifeCount = "476",
                CountSinceReboot = "65",
                Color = "Mono",
                Status = "Running",
                Black = 95,
                Cyan = 0,
                Magenta = 0,
                Yellow = 0,
                Tray1 = 65,
                Tray2 = 100,
                Tray3 = 0,
                Tray4 = 0
            };

            return printer;
        }

        public PrinterFullModel GetPrinter3()
        {
            PrinterFullModel printer = new PrinterFullModel()
            {
                IpAddress = "10.10.10.30",
                Description = "Gemensam",
                Location = "Kopiatorrum",
                Manufacturer = "HP",
                Model = "LaserJet",
                UpTime = "68d 12h 6m",
                LifeCount = "18896",
                CountSinceReboot = "6985",
                Color = "Color",
                Status = "Warning",
                Black = 85,
                Cyan = 56,
                Magenta = 74,
                Yellow = 33,
                Tray1 = 20,
                Tray2 = 100,
                Tray3 = 98,
                Tray4 = 100
            };

            return printer;
        }

        public PrinterFullModel GetPrinter4()
        {
            PrinterFullModel printer = new PrinterFullModel()
            {
                IpAddress = "10.10.10.40",
                Description = "Laser Skrivare",
                Location = "Hall 2",
                Manufacturer = "MURATEC",
                Model = "MFX-2030",
                UpTime = "3d 14h 55m",
                LifeCount = "3876",
                CountSinceReboot = "63",
                Color = "Color",
                Status = "Running",
                Black = 32,
                Cyan = 55,
                Magenta = 48,
                Yellow = 29,
                Tray1 = 60,
                Tray2 = 82,
                Tray3 = 100,
                Tray4 = 0
            };

            return printer;
        }

        public PrinterFullModel GetPrinter5()
        {
            PrinterFullModel printer = new PrinterFullModel()
            {
                IpAddress = "10.10.10.50",
                Description = "Multi-skrivare",
                Location = "IT",
                Manufacturer = "CANON",
                Model = "IR-ADV C50",
                UpTime = "12h 12m",
                LifeCount = "6984",
                CountSinceReboot = "17",
                Color = "Color",
                Status = "Warning",
                Black = 32,
                Cyan = 65,
                Magenta = 63,
                Yellow = 45,
                Tray1 = 8,
                Tray2 = 22,
                Tray3 = 68,
                Tray4 = 100
            };

            return printer;
        }

        public PrinterFullModel GetPrinter6()
        {
            PrinterFullModel printer = new PrinterFullModel()
            {
                IpAddress = "10.10.10.60",
                Description = "Test",
                Location = "IT",
                Manufacturer = "SAMSUNG",
                Model = "MX4580F",
                UpTime = " ",
                LifeCount = " ",
                CountSinceReboot = " ",
                Color = "Mono",
                Status = "Down",
                Black = 0,
                Cyan = 0,
                Magenta = 0,
                Yellow = 0,
                Tray1 = 0,
                Tray2 = 0,
                Tray3 = 0,
                Tray4 = 0
            };

            return printer;
        }

        public PrinterFullModel GetPrinter7()
        {
            PrinterFullModel printer = new PrinterFullModel()
            {
                IpAddress = "10.10.10.70",
                Description = "Multiskrivare",
                Location = "Mottagning",
                Manufacturer = "Xerox",
                Model = "AltaLink",
                UpTime = "9d 16h 2m",
                LifeCount = "12634",
                CountSinceReboot = "655",
                Color = " ",
                Status = "Running",
                Black = 89,
                Cyan = 62,
                Magenta = 55,
                Yellow = 50,
                Tray1 = 100,
                Tray2 = 63,
                Tray3 = 100,
                Tray4 = 100,

            };

            return printer;
        }

    }
}
