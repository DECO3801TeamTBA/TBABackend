using System;
using WanderListAPI.Models;

namespace WanderListAPI.Utility.Poco
{
    public class ContentHistoryResponse
    {
        public DateTime Date { get; set; }
        public string UserId { get; set; }

        public ContentHistoryResponse(History history)
        {
            Date = history.Date;
            UserId = history.UserId;
        }
    }

    public class UserHistoryResponse
    {
        public DateTime Date { get; set; }
        public Guid ContentId { get; set; }
        
        public UserHistoryResponse(History history)
        {
            Date = history.Date;
            ContentId = history.ContentId;
        }
    }
}
