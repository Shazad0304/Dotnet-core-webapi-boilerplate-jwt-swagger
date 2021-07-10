using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cronicle.Services
{
    public class EmailInfo
    {
        [Required]
        public string EmailTo { get; set; }

        [JsonIgnore]
        public string Subject { get; set; }

        [JsonIgnore]
        public string Body { get; set; }

        [JsonIgnore]
        public List<IFormFile> Attachments { get; set; }
    }
}
