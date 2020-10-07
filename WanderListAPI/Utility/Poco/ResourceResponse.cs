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

        public ResourceResponse(ResourceMeta resourceMeta)
        {
            ResourceId = resourceMeta.ResourceMetaId;
            FileName = resourceMeta.FileName;
            Description = resourceMeta.Description;
        }

        public static FileResult GetFile(ResourceMeta resourceMeta)
        {
            if (resourceMeta == null)
            {
                return null;
            }

            var resource = resourceMeta.Resource;

            if (resourceMeta.OnDisk)
            {
                //Assuming virtual, havent decided yet, probably virtual....
                return new VirtualFileResult(resource.FilePath, resourceMeta.MimeType);
            }
            else
            {
                return new FileContentResult(resource.Data, resourceMeta.MimeType);
            }
        }
    }
}
