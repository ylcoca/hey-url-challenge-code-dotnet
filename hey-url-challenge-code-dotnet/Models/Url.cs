using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hey_url_challenge_code_dotnet.Models
{
    public class Url: Identifiable
    {
        [Attr]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [StringLength(5)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ShortUrl { get; set; }
        public string OriginalUrl { get; set; }
        public int Count { get; set; }
        public DateTime Created { get; set; }
        public List<Metric> Metrics { get; set; }
    }
}
