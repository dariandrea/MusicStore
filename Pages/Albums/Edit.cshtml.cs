using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;


namespace MusicStore.Pages.Albums
{
    public class EditModel : AlbumGenresPageModel

    {
        private readonly MusicStore.Data.MusicStoreContext _context;

        public EditModel(MusicStore.Data.MusicStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Album Album { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Album = await _context.Album
                .Include(b => b.RecordLabel)
                .Include(b => b.AlbumGenres).ThenInclude(b => b.Genre)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Album == null)
            {
                return NotFound();
            }

            PopulateAssignedGenreData(_context, Album);
            ViewData["RecordLabelID"] = new SelectList(_context.Set<RecordLabel>(), "ID", "RecordLabelName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedGenres)
        {
            if (id == null)
            {
                return NotFound();
            }
            var albumToUpdate = await _context.Album
            .Include(i => i.RecordLabel)
            .Include(i => i.AlbumGenres)
            .ThenInclude(i => i.Genre)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (albumToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Album>(
            albumToUpdate,
            "Album",
            i => i.Title, i => i.Artist,
            i => i.Price, i => i.ReleaseDate, i => i.RecordLabel))
            {
                UpdateAlbumGenres(_context, selectedGenres, albumToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            //Apelam UpdateBookCategories pentru a aplica informatiile din checkboxuri la entitatea Books care
            //este editata
            UpdateAlbumGenres(_context, selectedGenres, albumToUpdate);
            PopulateAssignedGenreData(_context, albumToUpdate);
            return Page();
        }
    }
}
