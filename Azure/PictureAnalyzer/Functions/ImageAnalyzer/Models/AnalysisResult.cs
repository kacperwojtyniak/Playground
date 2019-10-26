using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageAnalyzer.Models
{
    public class AnalysisResult : TableEntity
    {
        public string Tags { get; set; }
        public Uri ImageUrl { get; internal set; }

        public AnalysisResult(string partitionKey, string rowKey, string tags, Uri imageUrl ) : base(partitionKey, rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            Tags = tags;
            ImageUrl = imageUrl;
        }
    }
}
