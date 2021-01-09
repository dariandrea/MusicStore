using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Pages.Albums
{
    public class CreateModel : AlbumGenresPageModel
    {
        private readonly MusicStore.Data.MusicStoreContext _context;

        public CreateModel(MusicStore.Data.MusicStoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["RecordLabelID"] = new SelectList(_context.Set<RecordLabel>(), "ID", "RecordLabelName");
            var album = new Album();
            album.AlbumGenres = new List<AlbumGenre>();
            PopulateAssignedGenreData(_context, album);
            return Page();
        }

        [BindProperty]
        public Album Album { get; set; }

        public async Task<IActionResult> OnPostAsync(string[] selectedGenres)
        {
            var newAlbum = new Album();
            if (selectedGenres != null)
            {
                newAlbum.AlbumGenres = new List<AlbumGenre>();
                foreach (var cat in selectedGenres)
                {
                    var catToAdd = new AlbumGenre
                    {
                        GenreID = int.Parse(cat)
                    };
                    newAlbum.AlbumGenres.Add(catToAdd);
                }
            }
            if (await TryUpdateModelAsync<Album>(
            newAlbum,
            "Album",
            i => i.Title, i => i.Artist,
            i => i.Price, i => i.ReleaseDate, i => i.RecordLabelID))
            {
                _context.Album.Add(newAlbum);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedGenreData(_context, newAlbum);
            return Page();
        }
    }
}
