using CsvHelper;
using CsvHelper.Configuration;
using PioneersTaskConsoleApp.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;

namespace PioneersTaskConsoleApp.Mappers
{
    internal class GoodsDataMap : ClassMap<GoodsData>
    {
        public GoodsDataMap()
        {
            Map(m => m.GoodId).Index(0).Name("Good ID");
            Map(m => m.TransactionId).Index(1).Name("Transaction ID");
            Map(m => m.TransactionDate).Index(2).Name("Transaction Date");
            Map(m => m.Amount).Index(3).Name("Amount");
            Map(m => m.Direction).Index(4).Name("Direction");
            Map(m => m.Comments).Index(5).Name("Comments");
            //Map(m => m.Errors).Convert((row) =>
            //{
            //    var errors = new StringBuilder();
            //    if (string.IsNullOrEmpty(row.Value.GoodId))
            //    {
            //        errors.AppendLine("Id is null for record " + row.Value.LineNumber);
            //    }
            //    if (string.IsNullOrEmpty(row.Value.TransactionId))
            //    {
            //        errors.AppendLine("TransactionId is null for record " + row.Value.LineNumber);
            //    }
            //    if (string.IsNullOrEmpty(row.Value.TransactionDate))
            //    {
            //        errors.AppendLine("TransactionDate is null for record " + row.Value.LineNumber);
            //    }
            //    if (string.IsNullOrEmpty(row.Value.Amount))
            //    {
            //        errors.AppendLine("Amount is null for record " + row.Value.LineNumber);
            //    }
            //    if (string.IsNullOrEmpty(row.Value.Direction))
            //    {
            //        errors.AppendLine("Direction is null for record " + row.Value.LineNumber);
            //    }
            //    return errors.ToString();
            //});
        }
    }
}
