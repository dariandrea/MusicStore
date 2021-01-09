using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStore.Models
{
    public class Genre
    {
        public int ID { get; set; }
        [Display(Name = "Genre")]
        public string GenreName { get; set; }
        public ICollection<AlbumGenre> AlbumGenres { get; set; }
    }
}
