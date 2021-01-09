using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStore.Models
{
    public class RecordLabel
    {
        public int ID { get; set; }
        [Display(Name = "Record Label name")]
        public string RecordLabelName { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}
