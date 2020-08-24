using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class Resource
    {
        //not sure if this annotation is needed at the moment
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ResourceId { get; set; }

        //For the case that the resource is in the database as a blob
        public byte[] Data { get; set; }

        public ResourceMeta ResourceMeta { get; set; }
        //If the file is stored on disk
        public string FilePath { get; set; }
    }
}
