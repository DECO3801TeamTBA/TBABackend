using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WanderListAPI.Data;
using WanderListAPI.Models;

namespace WanderListAPI.Utility.Poco
{
    public class ResourceResponse
    {
        public Guid ResourceId { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public FileResult ResourceFile { get; set; }

        public ResourceResponse(ResourceMeta resourceMeta)
        {
            var resource = resourceMeta.Resource;
            ResourceId = resource.ResourceId;
            FileName = resourceMeta.FileName;
            Description = resourceMeta.Description;

            if (resourceMeta.OnDisk)
            {
                //Assuming virtual, havent decided yet, probably virtual....
                ResourceFile = new VirtualFileResult(resource.FilePath, resourceMeta.MimeType);
            }
            else
            {
                ResourceFile = new FileContentResult(resource.Data, resourceMeta.MimeType);
            }
        }
    }
}
