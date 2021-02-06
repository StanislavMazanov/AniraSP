using System;
using System.Data;
using System.Diagnostics;
using AniraSP.DAL.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AniraSP.DAL.Handles {
    public class BulkCopyUploader : IBulkCopyUploader {
        private readonly AniraSpDbContext _aniraSpDbContext;

        public BulkCopyUploader(AniraSpDbContext aniraSpDbContext) {
            _aniraSpDbContext = aniraSpDbContext;
        }


        public void Upload(OffersTemp[] offers) {
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
            foreach (OffersTemp offer in offers) {
                string offerInfo = JsonConvert.SerializeObject(offer);
                DataRow row = dataTable.NewRow();
                row[0] = 1;
                row[1] = offer.OfferId;
                row[2] = offerInfo;
                dataTable.Rows.Add(row);
            }

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