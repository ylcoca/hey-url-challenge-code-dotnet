using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace hey_url_challenge_code_dotnet.Models
{
   
    public class Metric
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MetricId { get; set; }
        public Guid URLId { get; set; }
        public string Broswer { get; set; }
        public string OS { get; set; }
        public DateTime Clicked { get; set; }
    }
}
