using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;
using PioneersTaskConsoleApp.DataObjects;
using PioneersTaskConsoleApp.Mappers;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace PioneersTaskConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = SetupAppConfiguration();
            
            Dictionary<string, List<GoodsData>> GoodsInfoMap = new Dictionary<string, List<GoodsData>>();

            GoodsInfoMap = ReadDataFromCSVFile(configuration);

            GenerateCSVFileForEachProduct(GoodsInfoMap);
        }

        /// <summary>
        /// This sets up the application configuration file.
        /// </summary>
        /// <returns>The configurations</returns>
        internal static IConfigurationRoot SetupAppConfiguration()
        {
            // add configuration file for this application.
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

            return configuration;
        }

        /// <summary>
        /// Reads data from csv file and returns a dictionary with the good id as the key and transaction list as the value.
        /// </summary>
        /// <param name="configuration">The app configuration to get configuration for the app.</param>
        /// <returns></returns>
        internal static Dictionary<string, List<GoodsData>> ReadDataFromCSVFile(IConfigurationRoot configuration)
        {
            // get data file and error file Path from the configuration file.
            string dataFilePath = configuration["dataFilePath"] ?? "";

            string errorFilePath = configuration["errorsFilePath"] ?? "";

            // We will use map, it's easier this way to separate the data for each product.
            Dictionary<string, List<GoodsData>> GoodsInfoMap = new Dictionary<string, List<GoodsData>>();

            // check if file path exist
            if (!string.IsNullOrEmpty(dataFilePath))
            {
                string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                string fullPath = dir + dataFilePath;

                CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    Encoding = Encoding.UTF8,
                    HasHeaderRecord = false,
                    DetectDelimiter = true,
                    MissingFieldFound = (args) => {
                    }
                };

                // reading csv data from file
                using (var reader = new StreamReader(fullPath))
                {
                    using (var csvReader = new CsvReader(reader, csvConfiguration))
                    {
                        using (TextWriter errorFileWriter = File.AppendText(dir + errorFilePath))
                        {
                            csvReader.Context.RegisterClassMap<GoodsDataMap>();

                            var config = csvReader.Configuration;
                            bool headerSkipped = false;

                            while (csvReader.Read())
                            {
                                // trying to find the start of the data and skip the meta data at the start
                                if (!headerSkipped)
                                {
                                    var record = csvReader.GetRecord<dynamic>() as System.Dynamic.ExpandoObject;
                                    if (record != null)
                                    {
                                        var values = record.Select(a => a.Value).ToList();

                                        List<string> tokens = values[0].ToString().Split(";").Where(x => !string.IsNullOrEmpty(x)).ToList<string>();

                                        if (tokens != null && tokens.FirstOrDefault() != null && tokens.FirstOrDefault().StartsWith("Good ID"))
                                        {
                                            headerSkipped = true;
                                            continue;
                                        }

                                    }
                                }
                                else
                                {
                                    var record = csvReader.GetRecord<GoodsData>();
                                    if (record != null)
                                    {
                                        string importErrors = ValidateRecord(record, csvReader.CurrentIndex);
                                        errorFileWriter.WriteLine(importErrors);
                                        if (GoodsInfoMap.ContainsKey(record.GoodId))
                                        {
                                            GoodsInfoMap[record.GoodId].Add(record);
                                        }
                                        else
                                        {
                                            GoodsInfoMap.Add(record.GoodId, new List<GoodsData>() { record });
                                        }
                                    }
                                }
                            }
                        }

                    }
                }

            }
            return GoodsInfoMap;
        }

        /// <summary>
        /// This function takes the product map and generate file for each product. products with missing goodId will be all added in one file.
        /// </summary>
        /// <param name="GoodsInfoMap">The object map parsed from the csv file</param>
        internal static void GenerateCSVFileForEachProduct(Dictionary<string, List<GoodsData>> GoodsInfoMap)
        {
            foreach (var key in GoodsInfoMap.Keys)
            {
                string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                // appending date to name to keep old files.
                string writePath = dir + "/Files/" + key.ToString() + "_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".csv";
                using (var writer = new StreamWriter(writePath))
                {
                    using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csvWriter.WriteHeader<GoodsData>();
                        csvWriter.NextRecord();
                        foreach (var goodData in GoodsInfoMap[key])
                        {
                            csvWriter.WriteRecord<GoodsData>(goodData);
                            csvWriter.NextRecord();
                        }
                    }
                }
            }        
        }

        /// <summary>
        /// Validating records to add errors with line number in errors.txt
        /// </summary>
        /// <param name="goodsData">The record after being parsed to object</param>
        /// <param name="rowNumber">The line number in the excel file</param>
        /// <returns></returns>
        internal static string ValidateRecord(GoodsData goodsData, int rowNumber)
        {
            var errors = new StringBuilder();
            if (string.IsNullOrEmpty(goodsData.GoodId))
            {
                errors.AppendLine("Goods ID is null for record " + rowNumber);
            }
            if (string.IsNullOrEmpty(goodsData.TransactionId))
            {
                errors.AppendLine("TransactionId is null for record " + rowNumber);
            }
            if (string.IsNullOrEmpty(goodsData.TransactionDate))
            {
                errors.AppendLine("TransactionDate is null for record " + rowNumber);
            }
            if (string.IsNullOrEmpty(goodsData.Amount))
            {
                errors.AppendLine("Amount is null for record " + rowNumber);
            }
            if (string.IsNullOrEmpty(goodsData.Direction))
            {
                errors.AppendLine("Direction is null for record " + rowNumber);
            }
            return errors.ToString();
        }
    }

}