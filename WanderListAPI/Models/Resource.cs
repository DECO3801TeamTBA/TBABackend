using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WanderListAPI.Models
{
    public class Resource
    {
        // Table Properties
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ResourceId { get; set; }

        //For the case that the resource is in the database as a blob
        [JsonIgnore]
        public byte[] Data { get; set; }

        //public ResourceMeta ResourceMeta { get; set; }
        //If the file is stored on disk
        public string FilePath { get; set; }

        public class ResourceString
        {
            public Guid ResourceId { get; set; }
            public string Data { get; set; }
            public string FilePath { get; set; }

            public ResourceString(Resource resource)
            {
                ResourceId = resource.ResourceId;
                Data = "{data}";
                FilePath = resource.FilePath;
            }
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
