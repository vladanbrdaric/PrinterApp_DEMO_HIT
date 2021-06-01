using PrinterApplicationLibrary.DatabaseAccess;
using PrinterApplicationLibrary.Data;
using PrinterAgentLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrinterApplicationLibrary.Models;

namespace PrinterApplicationLibrary.Data
{
    // Business Logic Layer
    public class SqliteData : IDatabaseData
    {
        private const string connectionStringName = "SQLiteDb";
        private readonly ISqliteDataAccess _db;

        public SqliteData(ISqliteDataAccess db)
        {
            _db = db;
        }


        /// <summary>
        /// Insert the printer from the user interface. If printers ip address already exsist in the database then update its description and location,
        /// otherwise insert provided printer with its properties in the database.
        /// </summary>
        /// <param name="printer">Printer basic object consist of three properties, ip address, description and location.</param>
        public void InsertNewPrinter(PrinterBasicModel printer)
        {
            string IpAddress = printer.IpAddress;
            string Description = printer.Description;
            string Location = printer.Location;

            // Queries
            string sqlSelect = @"SELECT * 
                                FROM Printers
                                WHERE IpAddress = @IpAddress";

            string sqlInsert = @"INSERT INTO Printers (IpAddress, Description, Location)
                                VALUES (@IpAddress, @Description, @Location)";

            string sqlUpdate = @"UPDATE Printers
	                            SET Description = @Description, Location = @Location
	                            WHERE IpAddress = @IpAddress";


            // Check if there is already provided IP Address in the database
            var ipExist = _db.LoadData<PrinterModel, dynamic>(sqlSelect, new { IpAddress, Description, Location }, connectionStringName).FirstOrDefault();

            if (ipExist == null)
            {
                // Insert provided IP address, Description and Location into database
                _db.SaveData(sqlInsert, new { IpAddress, Description, Location }, connectionStringName);
            }
            else
            {
                // Update existing record where provided IP address match IP address that already exist in the database.
                _db.SaveData(sqlUpdate, new { IpAddress, Description, Location }, connectionStringName);
            }
        }


        /// <summary>
        /// Gets all the ip addresses from the database.
        /// </summary>
        /// <returns>A list of database of type string.</returns>
        public List<string> GetIpAddressesFromDB()
        {
            List<string> ipAddresses = new List<string>();
            string sqlSelect = @"SELECT IpAddress
                                FROM Printers";

            ipAddresses = _db.LoadData<string, dynamic>(sqlSelect, new { }, connectionStringName);

            return ipAddresses;
        }


        /// <summary>
        /// A bit complicated method.
        /// </summary>
        /// <param name="printer"></param>
        public void InsertPrinterValuesFromAgent(PrinterModel printer)
        {

            if (printer.Reachable != false)
            {
                List<TonerColorModel> toners = new List<TonerColorModel>()
                {
                    printer.Toner1ColorInPercentage,
                    printer.Toner2ColorInPercentage,
                    printer.Toner3ColorInPercentage,
                    printer.Toner4ColorInPercentage
                };

                List<PaperTrayModel> trays = new List<PaperTrayModel>()
                {
                    printer.Tray1InPercentage,
                    printer.Tray2InPercentage,
                    printer.Tray3InPercentage,
                    printer.Tray4InPercentage
                };

                // Nulls can't be stored in the db.
                int Black = -1;
                int Cyan = -1;
                int Magenta = -1;
                int Yellow = -1;

                int Tray1 = -1;
                int Tray2 = -1;
                int Tray3 = -1;
                int Tray4 = -1;

                try
                {
                    Black = toners.Where(x => x.Name == "Black").Select(x => x.CurrentLevelInPercentage).First();
                    Cyan = toners.Where(x => x.Name == "Cyan").Select(x => x.CurrentLevelInPercentage).First();
                    Magenta = toners.Where(x => x.Name == "Magenta").Select(x => x.CurrentLevelInPercentage).First();
                    Yellow = toners.Where(x => x.Name == "Yellow").Select(x => x.CurrentLevelInPercentage).First();
                }
                catch (System.NullReferenceException)
                {

                }

                try
                {
                    Tray1 = trays.Where(x => x.Name.ToLower() == "tray 1" || x.Name.ToLower() == "fack 1").Select(x => x.CurrentLevelInPercentage).First();
                    Tray2 = trays.Where(x => x.Name.ToLower() == "tray 2" || x.Name.ToLower() == "fack 2").Select(x => x.CurrentLevelInPercentage).First();
                    Tray3 = trays.Where(x => x.Name.ToLower() == "tray 3" || x.Name.ToLower() == "fack 3").Select(x => x.CurrentLevelInPercentage).First();
                    Tray4 = trays.Where(x => x.Name.ToLower() == "tray 4" || x.Name.ToLower() == "fack 4").Select(x => x.CurrentLevelInPercentage).First();
                }
                catch (Exception)
                {

                }

                string sql = @"UPDATE Printers
                            SET Manufacturer = @Manufacturer, Model = @Model, SerialNumber = @SerialNumber,
                            UpTime = @UpTime, LifeCount = @LifeCount, CountSinceReboot = @CountSinceReboot,
                            Color = @Color, Status = @Status, Black = @Black, Cyan = @Cyan,
                            Magenta = @Magenta, Yellow = @Yellow, Tray1 = @Tray1, Tray2 = @Tray2,
                            Tray3 = @Tray3, Tray4 = @Tray4
                            WHERE IpAddress = @IpAddress";


                _db.SaveData(sql,
                              new
                              {
                                  IpAddress = printer.IpAddress,
                                  Manufacturer = printer.Manufacturer,
                                  Model = printer.Model,
                                  SerialNumber = printer.SerialNumber,
                                  UpTime = printer.UpTime,
                                  LifeCount = printer.LifeCount,
                                  CountSinceReboot = printer.CountSinceReboot,
                                  Color = printer.Color,
                                  Status = printer.Status,
                                  Black,
                                  Cyan,
                                  Magenta,
                                  Yellow,
                                  Tray1,
                                  Tray2,
                                  Tray3,
                                  Tray4
                              },
                              connectionStringName);
            }
            else
            {
                string sql = @"UPDATE Printers
                               SET UpTime = @UpTime
                               WHERE IpAddress = @IpAddress";

                _db.SaveData(sql,
                              new
                              {
                                  IpAddress = printer.IpAddress,
                                  UpTime = printer.UpTime
                              },
                              connectionStringName);
            }
        }


        /// <summary>
        /// Gets all the printers from the db.
        /// </summary>
        /// <returns></returns>
        public List<PrinterFullModel> GetAllPrintersFromDB()
        {
            string sql = @"SELECT *
                           FROM Printers";

            var printers = _db.LoadData<PrinterFullModel, dynamic>(sql, new { }, connectionStringName);

            return printers;
        }
    }
}
