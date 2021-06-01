using Microsoft.Extensions.Configuration;
using PrinterAgentLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SnmpSharpNet;

namespace PrinterAgentLibrary
{
    public class PrinterSnmpData : IPrinterSnmpData
    {
        private readonly IConfiguration _config;

        // Toner 1 - 4
        string[] toner1oidArray = new string[]
        {
            "1.3.6.1.2.1.43.11.1.1.6.1.1",
            "1.3.6.1.2.1.43.11.1.1.8.1.1",
            "1.3.6.1.2.1.43.11.1.1.9.1.1"
        };
        string[] toner2oidArray = new string[]
        {
            "1.3.6.1.2.1.43.11.1.1.6.1.2",
            "1.3.6.1.2.1.43.11.1.1.8.1.2",
            "1.3.6.1.2.1.43.11.1.1.9.1.2"
        };
        string[] toner3oidArray = new string[]
        {
            "1.3.6.1.2.1.43.11.1.1.6.1.3",
            "1.3.6.1.2.1.43.11.1.1.8.1.3",
            "1.3.6.1.2.1.43.11.1.1.9.1.3"
        };
        string[] toner4oidArray = new string[]
        {
            "1.3.6.1.2.1.43.11.1.1.6.1.4",
            "1.3.6.1.2.1.43.11.1.1.8.1.4",
            "1.3.6.1.2.1.43.11.1.1.9.1.4"
        };

        // Paper tray 1 - 4 
        string[] paperTray1oidArray = new string[]
        {
            "1.3.6.1.2.1.43.8.2.1.18.1.1",
            "1.3.6.1.2.1.43.8.2.1.9.1.1",
            "1.3.6.1.2.1.43.8.2.1.10.1.1"
        };
        string[] paperTray2oidArray = new string[]
        {
            "1.3.6.1.2.1.43.8.2.1.18.1.2",
            "1.3.6.1.2.1.43.8.2.1.9.1.2",
            "1.3.6.1.2.1.43.8.2.1.10.1.2"
        };
        string[] paperTray3oidArray = new string[]
        {
            "1.3.6.1.2.1.43.8.2.1.18.1.3",
            "1.3.6.1.2.1.43.8.2.1.9.1.3",
            "1.3.6.1.2.1.43.8.2.1.10.1.3"
        };
        string[] paperTray4oidArray = new string[]
        {
            "1.3.6.1.2.1.43.8.2.1.18.1.4",
            "1.3.6.1.2.1.43.8.2.1.9.1.4",
            "1.3.6.1.2.1.43.8.2.1.10.1.4"
        };



        // Constructor for DI
        public PrinterSnmpData(IConfiguration config)
        {
            _config = config;
        }

        // This model will be returned if the printer is unreadhable.
        //PrinterModel unReachablePrinter = new PrinterModel()
        //{
        //    IpAddress = "",
        //    UpTime = ""
        //};


        public PrinterModel GetPrinterData(string ipAddress, string snmpCommunity = "public")
        {

            PrinterModel printer = new PrinterModel();
            printer.IpAddress = ipAddress;

            printer.UpTime = UpTime(ipAddress, snmpCommunity);

            // If method UpTime return 'unreachable', return unreachable printer object of type PrinterModel. 
            // (IP address that has been provided is not valid or agent has no access to provided printer).
            if (printer.UpTime.ToLower() == "unreachable")
            {
                // This object will be returned if the printer is unreachable.
                PrinterModel unreachablePrinter = new PrinterModel()
                {
                    IpAddress = printer.IpAddress,
                    UpTime = printer.UpTime,
                    Reachable = false
                };

                return unreachablePrinter;
            }

            // Otherwise continue getting data/info from the printer.
            printer.Manufacturer = Manufacturer(ipAddress, snmpCommunity);
            printer.Model = Model(ipAddress, snmpCommunity);
            printer.SerialNumber = SerialNumber(ipAddress, snmpCommunity);
            printer.LifeCount = PageLifeCount(ipAddress, snmpCommunity);
            printer.CountSinceReboot = PageCountSinceReboot(ipAddress, snmpCommunity);
            printer.Color = Color(ipAddress, snmpCommunity);
            printer.Status = DeviceStatus(ipAddress, snmpCommunity);
            
            printer.Toner1ColorInPercentage = TonerColorInPercentage(ipAddress, snmpCommunity, toner1oidArray);
            printer.Toner2ColorInPercentage = TonerColorInPercentage(ipAddress, snmpCommunity, toner2oidArray);
            printer.Toner3ColorInPercentage = TonerColorInPercentage(ipAddress, snmpCommunity, toner3oidArray);
            printer.Toner4ColorInPercentage = TonerColorInPercentage(ipAddress, snmpCommunity, toner4oidArray);

            printer.Tray1InPercentage = TrayInPercentage(ipAddress, snmpCommunity, paperTray1oidArray);
            printer.Tray2InPercentage = TrayInPercentage(ipAddress, snmpCommunity, paperTray2oidArray);
            printer.Tray3InPercentage = TrayInPercentage(ipAddress, snmpCommunity, paperTray3oidArray);
            printer.Tray4InPercentage = TrayInPercentage(ipAddress, snmpCommunity, paperTray4oidArray);

            return printer;
        }



        /// <summary>
        /// This method send a snmp.Get request with a spefic OID number and returns the Up time of given device ip address.
        /// </summary>
        /// <param name="ipAddress">Device IP address</param>
        /// <param name="snmpCommunity">snmp community</param>
        /// <returns>(Up time) The time since the network management portion of the system was last re-initialized.</returns>
        public string UpTime(string ipAddress, string snmpCommunity)
        {
            string sysUpTimeOID = "1.3.6.1.2.1.1.3.0";
            string upTime = "";

            string[] oidArray = new string[]
            {
                sysUpTimeOID
            };

            // Get UpTime info about printer.
            Dictionary<Oid, AsnType> result = snmpGetRequest(ipAddress, snmpCommunity, oidArray);

            if (result != null)
            {
                // Responde can be i.e. ([3 days 12 minutes 15 seconds] or [ 3d 12m 15s]
                string[] upTimeArray = result.Values.ElementAt(0).ToString().Split();

                // Desired format is [xd xh xm]
                upTime = GetDesiredUpTimeFormat(ipAddress, snmpCommunity, upTimeArray);
            }
            else
            {
                //upTime = "Offline";
                upTime = "Unreachable";
            }
            return upTime;
        }


        /// <summary>
        /// This method send a snmp.Get request with a spefic OID number and returns the manufacturer of given device ip address.
        /// </summary>
        /// <param name="ipAddress">Device IP address</param>
        /// <param name="snmpCommunity">snmp community</param>
        /// <returns>Manufacturer of given device IP</returns>
        public string Manufacturer(string ipAddress, string snmpCommunity)
        {
            string sysDescrOID = "1.3.6.1.2.1.1.1.0";
            string manufacturer = "";

            string[] oidArray = new string[]
            {
                sysDescrOID
            };

            Dictionary<Oid, AsnType> result = snmpGetRequest(ipAddress, snmpCommunity, oidArray);

            if (result != null)
            {
                string[] resultArray = result.Values.ElementAt(0).ToString().Split();
                manufacturer = GetManufacturer(resultArray);
            }
            else
            {
                //manufacturer = "Unknown";
                manufacturer = "";
            }
            return manufacturer;
        }


        /// <summary>
        /// This method send a snmp.Get request with a spefic OID number and returns the model of given device ip address.
        /// </summary>
        /// <param name="ipAddress">Device IP address</param>
        /// <param name="snmpCommunity">snmp community</param>
        /// <returns>Model of given device IP address</returns>
        public string Model(string ipAddress, string snmpCommunity)
        {
            // hrDeviceDescr
            string deviceDescrOID = "1.3.6.1.2.1.25.3.2.1.3.1";
            string model = "";

            string[] oidArray = new string[] {
                deviceDescrOID
            };

            Dictionary<Oid, AsnType> result = snmpGetRequest(ipAddress, snmpCommunity, oidArray);

            if (result != null)
            {
                model = GetModelName(ipAddress, snmpCommunity, result);
            }
            else
            {
                //model = "Unknown";
                model = "";
            }
            return model;
        }


        /// <summary>
        /// This method send a snmp.Get request with a spefic OID number and returns the serial number of given device ip address.
        /// </summary>
        /// <param name="ipAddress">Device IP address</param>
        /// <param name="snmpCommunity">snmp community</param>
        /// <returns>Serial number of given device IP address</returns>
        public string SerialNumber(string ipAddress, string snmpCommunity)
        {
            string serialNumberOID = "1.3.6.1.2.1.43.5.1.1.17.1";
            string serialModel = "";

            string[] oidArray = new string[]
            {
                serialNumberOID
            };

            Dictionary<Oid, AsnType> result = snmpGetRequest(ipAddress, snmpCommunity, oidArray);

            if (result != null)
            {
                serialModel = result.Values.ElementAt(0).ToString();
            }
            else
            {
                //serialModel = "Unknown";
                serialModel = "";
            }
            return serialModel;
        }


        /// <summary>
        /// This method send a snmp.Get request with a specific OID number and returns the number of printed pages during the lifetime of the printer.
        /// </summary>
        /// <param name="ipAddress">Device IP address</param>
        /// <param name="snmpCommunity">Device IP address name</param>
        /// <returns>The count of pages printer during the lifetime of the printer.</returns>
        public string PageLifeCount(string ipAddress, string snmpCommunity)
        {
            string pageLifeCountOID = "1.3.6.1.2.1.43.10.2.1.4.1.1";
            string pageLifeCount = "";

            string[] oidArray = new string[]
            {
                pageLifeCountOID
            };

            Dictionary<Oid, AsnType> result = snmpGetRequest(ipAddress, snmpCommunity, oidArray);

            if (result != null)
            {
                pageLifeCount = result.Values.ElementAt(0).ToString();
            }
            else
            {
                //pageLifeCount = "Unknown";
                pageLifeCount = "";
            }
            return pageLifeCount;
        }


        /// <summary>
        /// This method send a snmp.Get request with a specific OID number and returns the number of printed pages since last reboot.
        /// </summary>
        /// <param name="ipAddress">Device IP address</param>
        /// <param name="snmpCommunity">Device IP address name</param>
        /// <returns>The count of pages printed since the printer was last booted rebooted.</returns>
        public string PageCountSinceReboot(string ipAddress, string snmpCommunity)
        {
            string pageCountSinceRebootOID = "1.3.6.1.2.1.43.10.2.1.5.1.1";
            string pageCountSinceReboot = "";

            string[] oidArray = new string[]
            {
                pageCountSinceRebootOID
            };

            Dictionary<Oid, AsnType> result = snmpGetRequest(ipAddress, snmpCommunity, oidArray);

            if (result != null)
            {
                pageCountSinceReboot = result.Values.ElementAt(0).ToString();
            }
            else
            {
                //pageCountSinceReboot = "Unknown";
                pageCountSinceReboot = "";
            }

            return pageCountSinceReboot;
        }


        /// <summary>
        /// This method send a snmp.Get request with a specific OID number and returns "Mono" if printer has only one color, otherwise "Color".
        /// </summary>
        /// <param name="ipAddress">Device IP address</param>
        /// <param name="snmpCommunity">Device IP address name</param>
        /// <returns>"Mono" if printer has only one color, otherwise "Color"</returns>
        public string Color(string ipAddress, string snmpCommunity)
        {
            string secondColorOID = "1.3.6.1.2.1.43.11.1.1.6.1.2";
            string color = "";

            string[] oidArray = new string[]
            {
                secondColorOID
            };

            var checkForSecondColor = snmpGetRequest(ipAddress, snmpCommunity, oidArray);

            if (checkForSecondColor != null)
            {
                color = "Color";
            }
            else
            {
                color = "Mono";
            }
            return color;
        }


        // === TO DO: ===
        public string ServiceCode(string ipAddress, string snmpCommunity)
        {
            //return "Not available";
            return "";
        }

        // === TO DO: ===
        public string DeviceStatus(string ipAddress, string snmpCommunity)
        {
            string deviceStatusOID = "1.3.6.1.2.1.25.3.2.1.5.1";
            string deviceStatus = "";

            string[] oidArray = new string[]
            {
                deviceStatusOID
            };

            Dictionary<Oid, AsnType> result = snmpGetRequest(ipAddress, snmpCommunity, oidArray);

            if (result != null)
            {
                string returnCode = result.Values.ElementAt(0).ToString();
                deviceStatus = DetermineDeviceStatus(returnCode);
            }
            else
            {
                //deviceStatus = "Unknown";
                deviceStatus = "";
            }
            return deviceStatus;
        }


        /// <summary>
        /// This method send a snmp.Get request with a specific OID numbers, do some calculation 
        /// and returns an object of type TonerColorModel that contains color name and current level in percentage.
        /// </summary>
        /// <param name="ipAddress">Device IP address</param>
        /// <param name="snmpCommunity">Device IP address name</param>
        /// <returns>An object of type TonerColorModel that contains color name and current level in percentage.</returns>
        public TonerColorModel TonerColorInPercentage(string ipAddress, string snmpCommunity, string[] tonerOidArray)
        {
            TonerColorModel toner = new TonerColorModel();

            Dictionary<Oid, AsnType> result = snmpGetRequest(ipAddress, snmpCommunity, tonerOidArray);

            if (result != null)
            {
                // result[0] = TonerName 
                string tonerColorToBeChecked = result.Values.ElementAt(0).ToString();
                toner.Name = GetTonerColor(tonerColorToBeChecked);

                // result[1] = TonerMaxCapacity
                double maxCapacity = double.Parse(result.Values.ElementAt(1).ToString());

                // result[2] = TonerCurrentLevel
                double currentLevel = double.Parse(result.Values.ElementAt(2).ToString());

                // count toner level in percentage
                toner.CurrentLevelInPercentage = CountLevelInPercentage(maxCapacity, currentLevel);
            }
            else
            {
                toner.Name = "";
                toner.CurrentLevelInPercentage = -1;
            }
            return toner;
        }


        /// <summary>
        /// This method send a snmp.Get request with a specific OID numbers, do some calculation 
        /// and returns an object of type PaperTrayModel that contains paper tray name and current paper level in percentage.
        /// </summary>
        /// <param name="ipAddress">Device IP address</param>
        /// <param name="snmpCommunity">Device IP address name</param>
        /// <returns>An object of type PaperTrayModel that contains paper tray name and current paper level in percentage.</returns>
        public PaperTrayModel TrayInPercentage(string ipAddress, string snmpCommunity, string[] oidArray)
        {
            PaperTrayModel paperTray = new PaperTrayModel();

            Dictionary<Oid, AsnType> result = snmpGetRequest(ipAddress, snmpCommunity, oidArray);

            if (result != null)
            {
                // result[0] = TrayName
                paperTray.Name = result.Values.ElementAt(0).ToString();

                // result[1] = TrayMaxCapacity
                double maxCapacity = double.Parse(result.Values.ElementAt(1).ToString());

                // result[2] = TrayCurrentLevel
                double currentLevel = double.Parse(result.Values.ElementAt(2).ToString());

                // Count tray level in percentage
                paperTray.CurrentLevelInPercentage = CountLevelInPercentage(maxCapacity, currentLevel);
            }
            else
            {
                paperTray.Name = "";
                paperTray.CurrentLevelInPercentage = -1;
            }
            return paperTray;
        }


        // ================================================================================================================
        // === HELPING METHODS ===

        // Method that return just a manufacturer name
        public string GetManufacturer(string[] resultArray)
        {
            return resultArray.ElementAt(0);
        }

        // Method that determine toner color and return proper name of it.
        public string GetTonerColor(string tonerColorToBeChecked)
        {
            // split string into words
            string[] tonerColorArray = tonerColorToBeChecked.Split();
            string output = "";

            // TO DO: Check for the color name if its black, cyan, magenta, yellow, svart, gul.

            switch (tonerColorArray[0].ToLower())
            {
                case "black":
                    output = "Black";
                    break;
                case "cyan":
                    output = "Cyan";
                    break;
                case "magenta":
                    output = "Magenta";
                    break;
                case "yellow":
                    output = "Yellow";
                    break;
                case "svart":
                    output = "Black";
                    break;
                case "gul":
                    output = "Yellow";
                    break;
                case "tri-color":
                    output = "Tri-color";
                    break;
                case "ymcko":
                    output = "YMCKO";
                    break;
                default:
                    output = "Unknown";
                    break;
            }

            return output;
        }

        // This method counts toner level in percentage
        public int CountLevelInPercentage(double maxCapacity, double currentLevel)
        {
            double temp = 0.0;
            int output = 0;

            // check if maxCapacity or currentLevel are negativ number.
            if (maxCapacity > 0 && currentLevel > 0)
            {
                temp = Math.Round(((currentLevel / maxCapacity) * 100), 0);
                output = (int)temp;
            }
            else
            {
                output = -1;
            }

            return output;
        }

        // This method check for the UpTime format and return format that I want to return.
        public string GetDesiredUpTimeFormat(string ipAddress, string snmpCommunity, string[] upTimeArray)
        {
            //string[] upTimeArray = result.Values.ElementAt(0).ToString().Split( );
            string upTimeArrayFirstElement = upTimeArray[0];
            string upTime = "";

            // Check if upTimeArrayFirstElement is just a number or mix of numbers and letters.
            // if it's "2d" or "2 days".  
            bool upTimeFormatContainLetter = Regex.IsMatch(upTimeArrayFirstElement, "[a-z]");

            if (upTimeFormatContainLetter == true)
            {
                // remove leading zeroes
                upTime = RemoveLeadingZeroes(upTimeArray);
            }
            else
            {
                upTime = GetShortTimeFormat(upTimeArray);
            }
            return upTime;
        }

        // This method belongs "ReturnDesiredUpTimeFormat"
        public string GetShortTimeFormat(string[] upTimeArray)
        {
            string upTime = "";

            for (int i = 0; i < upTimeArray.Length; i += 2)
            {
                if (upTimeArray[i + 1].ToLower() == "days" || upTimeArray[i + 1].ToLower() == "hours" || upTimeArray[i + 1].ToLower() == "minutes")
                {
                    upTime += ($"{ upTimeArray[i] }{ upTimeArray[i + 1].Substring(0, 1) } ");
                }
            }

            return upTime.Trim();
        }

        // Remove leading zeroes from the date so that (0d 0h 15m 12s -> 15m 12s)
        public string RemoveLeadingZeroes(string[] upTimeArray)
        {
            string output = "";
            foreach (var item in upTimeArray)
            {
                if (item.StartsWith('0') == false && item.Substring(item.Length - 2, 2) != "ms" && item.Substring(item.Length - 1, 1) != "s")
                {
                    output += $"{ item } ";
                }
            }
            return output.Trim();
        }

        // This method do "snmp.Get request" and return a result of type "Dictionary<Oid, AsnType>.
        public Dictionary<Oid, AsnType> snmpGetRequest(string ipAddress, string snmpCommunity, string[] oidArray)
        {
            SimpleSnmp snmp = new SimpleSnmp(ipAddress, snmpCommunity);
            Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver1, oidArray);

            return result;
        }

        // This method belongs method "Model".
        public string GetModelName(string ipAddress, string snmpCommunity, Dictionary<Oid, AsnType> result)
        {
            string output = "";
            int counter = 0;

            // split result into words
            string[] modelArray = result.Values.ElementAt(0).ToString().Split();

            // find out manufacturer
            string manufacturer = Manufacturer(ipAddress, snmpCommunity);

            // loop through all elements in array
            for (int i = 0; i < modelArray.Length; i++)
            {
                // check if any of elements in the array (words in modelArray) is equal with those words in if statement.
                if (modelArray[i].ToLower() != "printer" && modelArray[i] != manufacturer && modelArray[i].ToLower() != "skrivare")
                {
                    // if true increase counter by 1
                    counter++;

                    // add that word to output.
                    output += modelArray[i] + " ";

                    // check if counter is not more than 2, dvs I want only two words to export t.ex. "AltaLink C8045"
                    if (counter >= 2)
                    {
                        break;
                    }
                }
            }

            return output;
        }

        // TO DO:
        private static string DetermineDeviceStatus(string result)
        {
            /*
                hrDeviceStatus OBJECT-TYPE
                SYNTAX INTEGER {
                unknown(1), running(2), warning(3), testing(4), down(5)}
                MAX-ACCESS read-only
                STATUS current
                DESCRIPTION
                    "The current operational state of the device described by this row of the table. 
                    A value unknown(1) indicates that the current state of the device is unknown. running(2) indicates that the device is up and running and that no unusual error conditions are known. 
                    The warning(3) state indicates that agent has been informed of an unusual error condition by the operational software (e.g., a disk device driver) but that the device is still 'operational'. 
                    An example would be a high number of soft errors on a disk. A value of testing(4), indicates that the device is not available for use because it is in the testing state.
                    The state of down(5) is used only when the agent has been informed that the device is not available for any use."
             */


            string errorState = "";
            switch (result.ToString())
            {
                case "1":
                    errorState = "Unknown";
                    return errorState;
                case "2":
                    errorState = "Running";
                    return errorState;
                case "3":
                    errorState = "Warning";
                    return errorState;
                case "4":
                    errorState = "Testing";
                    return errorState;
                case "5":
                    errorState = "Down";
                    return errorState;
                default:
                    return errorState;
                    //return "Unknown";


                    //case "1":
                    //    errorState = "UNKNOWN (1) => The current state of the device is unknown.";
                    //    return errorState;
                    //case "2":
                    //    errorState = "RUNNING (2) => Device is up and running and that unusual error conditions are known.";
                    //    return errorState;
                    //case "3":
                    //    errorState = "WARNING (3) => Unusual error condition by theoperational software but that the device is still 'operational'";
                    //    return errorState;
                    //case "4":
                    //    errorState = "TESTING (4) => Device is not available for use because it is in the testing state.";
                    //    return errorState;
                    //case "5":
                    //    errorState = "DOWN (5) => Device is NOT available for any use.";
                    //    return errorState;
                    //default:
                    //    return "Entered value not in the range [1 - 5]...";
            }
        }
    }
}
