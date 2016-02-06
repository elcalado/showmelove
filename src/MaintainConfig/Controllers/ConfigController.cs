using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaintainConfig.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace MaintainConfig.Controllers
{
    public class ConfigController : Controller
    {
        const String PARTITION_KEY = "SHOWMELOVE";
        const String ROW_KEY = "PHOTO_INTERVAL";

        // GET: Config
        public ActionResult Index()
        {
            var key = "PHOTO_INTERVAL";
            ConfigEntity config = GetConfigByKey(key);
            //int configValue = GetConfigValueByKey(key);
            return View(config);
        }

        [HttpPost]
        public ActionResult Index(ConfigEntity config)
        {
            return Save(config);
        }


        public ActionResult Save(ConfigEntity config)
        {
            int newValue = Convert.ToInt32(config.Value);
            SaveConfigValue(config.RowKey, newValue);
            return View("Index", config);
        }

        public ConfigEntity GetConfigByKey(string key)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference("config");

            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<ConfigEntity>(PARTITION_KEY, key);

            // Execute the retrieve operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);
            ConfigEntity config = new ConfigEntity() { Value = "10" };
            if (retrievedResult.HttpStatusCode == 200)
            {
                config = (ConfigEntity)retrievedResult.Result;
            }


            return config;

        }

        public int GetConfigValueByKey(string key)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference("config");

            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<ConfigEntity>(PARTITION_KEY, key);

            // Execute the retrieve operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            var config = (ConfigEntity)retrievedResult.Result;
            string value = config.Value;

            return Convert.ToInt32(value);

        }

        public void SaveConfigValue(string key, int value)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference("config");

            // Create a new customer entity.
            var config = new ConfigEntity();
            config.PartitionKey = PARTITION_KEY;
            config.RowKey = key;
            config.Value = value.ToString();


            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.InsertOrReplace(config);

            // Execute the insert operation.
            table.Execute(insertOperation);

        }
    }
}