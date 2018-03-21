using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;

namespace EventGridPublisher
{
    public class AzureEventGridEvent<T> : EventGridEvent where T : class
    {
        public AzureEventGridEvent(T data, string subject, string dataVersion = "1.0", string topic = null, string metadataVersion = null)
            : base(Guid.NewGuid().ToString(), subject, data, typeof(T).ToString(), DateTime.UtcNow, dataVersion, topic, metadataVersion)
        {
        }

        [JsonIgnore]
        public T EventData
        {
            get => base.Data as T;
            set => base.Data = value;
        }
    }
}
