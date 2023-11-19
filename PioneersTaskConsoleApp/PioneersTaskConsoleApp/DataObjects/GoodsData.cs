using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using PioneersTaskConsoleApp.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PioneersTaskConsoleApp.DataObjects
{
    internal class GoodsData
    {
        public string? GoodId { get; set; }
        public string? TransactionId { get; set; }
        public string? TransactionDate { get; set; }
        public string? Amount { get; set; }
        public string? Direction { get; set; }
        public string? Comments { get; set; }

    }
}
