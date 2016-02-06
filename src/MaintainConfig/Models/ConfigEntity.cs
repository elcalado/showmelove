using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace MaintainConfig.Models
{
    public class ConfigEntity: TableEntity
    {
        const String PARTITION_KEY = "SHOWMELOVE";
        const String ROW_KEY = "PHOTO_INTERVAL";

        public ConfigEntity()
        {
            PartitionKey = PARTITION_KEY;
            RowKey = ROW_KEY;
            Value = "0";
        }
        public string Value { get; set; }

    }
}