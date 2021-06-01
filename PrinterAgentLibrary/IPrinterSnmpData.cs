using PrinterAgentLibrary.Models;
using SnmpSharpNet;
using System.Collections.Generic;

namespace PrinterAgentLibrary
{
    public interface IPrinterSnmpData
    {
        string Color(string ipAddress, string snmpCommunity);
        int CountLevelInPercentage(double maxCapacity, double currentLevel);
        string DeviceStatus(string ipAddress, string snmpCommunity);
        string GetDesiredUpTimeFormat(string ipAddress, string snmpCommunity, string[] upTimeArray);
        string GetManufacturer(string[] resultArray);
        string GetModelName(string ipAddress, string snmpCommunity, Dictionary<Oid, AsnType> result);
        PrinterModel GetPrinterData(string ipAddress, string snmpCommunity = "public");
        string GetShortTimeFormat(string[] upTimeArray);
        string GetTonerColor(string tonerColorToBeChecked);
        string Manufacturer(string ipAddress, string snmpCommunity);
        string Model(string ipAddress, string snmpCommunity);
        string PageCountSinceReboot(string ipAddress, string snmpCommunity);
        string PageLifeCount(string ipAddress, string snmpCommunity);
        string RemoveLeadingZeroes(string[] upTimeArray);
        string SerialNumber(string ipAddress, string snmpCommunity);
        string ServiceCode(string ipAddress, string snmpCommunity);
        Dictionary<Oid, AsnType> snmpGetRequest(string ipAddress, string snmpCommunity, string[] oidArray);
        TonerColorModel TonerColorInPercentage(string ipAddress, string snmpCommunity, string[] tonerOidArray);
        PaperTrayModel TrayInPercentage(string ipAddress, string snmpCommunity, string[] oidArray);
        string UpTime(string ipAddress, string snmpCommunity);
    }
}