using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AniraSP.BLL;
using AniraSP.BLL.Models;
using AniraSP.DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using AniraSP.DAL.Handles;
using AniraSP.Utilities.Storage;
using Newtonsoft.Json;

namespace AniraSP.PerformanceTest {
    public class PortionUploader : StorageUploader<AniraSpOffer> {
        private readonly AniraSpDbContext _aniraSpDbContext;

        public PortionUploader(AniraSpDbContext aniraSpDbContext) {
            _aniraSpDbContext = aniraSpDbContext;
            OfferPortions = 100000;
        }

        public override void UpdateToStorage(List<AniraSpOffer> offers) {
            // connect to SQL

            string connectionString = _aniraSpDbContext.Database.GetConnectionString();
            using var connection = new SqlConnection(connectionString);
            // make sure to enable triggers
            // more on triggers in next post
            var loader = new SqlBulkCopy
                (
                    connection,
                    //SqlBulkCopyOptions.TableLock |
                    SqlBulkCopyOptions.FireTriggers |
                    SqlBulkCopyOptions.UseInternalTransaction,
                    null
                )
                {DestinationTableName = "OffersTemp"};
            loader.ColumnMappings.Add(0, 1);
            loader.ColumnMappings.Add(1, 2);
            loader.ColumnMappings.Add(2, 3);

            // set the destination table name
            connection.Open();

            // write the data in the "dataTable"
            var dataTable = new DataTable();
            dataTable.Columns.Add("SiteId", typeof(int));
            dataTable.Columns.Add("OfferId", typeof(string));
            dataTable.Columns.Add("OfferInfo", typeof(string));
            foreach (AniraSpOffer offer in offers) {
                string offerInfo = JsonConvert.SerializeObject(offer);
                DataRow row = dataTable.NewRow();
                row[0] = 1;
                row[1] = offer.OfferId;
                row[2] = offerInfo;
                dataTable.Rows.Add(row);
            }

            // dataTable.Columns.Add("Birthday", typeof(DateTime) );
            loader.BulkCopyTimeout = 60 * 10;
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            loader.WriteToServer(dataTable);
            connection.Close();
            dataTable.Clear();
            stopWatch.Stop();
            Console.WriteLine(stopWatch.Elapsed);
        }
    }
}