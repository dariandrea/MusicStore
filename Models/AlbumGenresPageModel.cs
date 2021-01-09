using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicStore.Data;


namespace MusicStore.Models
{
    public class AlbumGenresPageModel : PageModel
    {
        public List<AssignedGenreData> AssignedGenreDataList;
        public void PopulateAssignedGenreData(MusicStoreContext context, Album album)
        {
            var allGenres = context.Genre;
            var albumGenres = new HashSet<int>(
            album.AlbumGenres.Select(c => c.AlbumID));
            AssignedGenreDataList = new List<AssignedGenreData>();
            foreach (var cat in allGenres)
            {
                AssignedGenreDataList.Add(new AssignedGenreData
                {
                    GenreID = cat.ID,
                    Name = cat.GenreName,
                    Assigned = albumGenres.Contains(cat.ID)
                });
            }
        }
        public void UpdateAlbumGenres(MusicStoreContext context,
        string[] selectedGenres, Album albumToUpdate)
        {
            if (selectedGenres == null)
            {
                albumToUpdate.AlbumGenres = new List<AlbumGenre>();
                return;
            }
            var selectedGenresHS = new HashSet<string>(selectedGenres);
            var albumGenres = new HashSet<int>
            (albumToUpdate.AlbumGenres.Select(c => c.Genre.ID));
            foreach (var cat in context.Genre)
            {
                if (selectedGenresHS.Contains(cat.ID.ToString()))
                {
                    if (!albumGenres.Contains(cat.ID))
                    {
                        albumToUpdate.AlbumGenres.Add(
                        new AlbumGenre
                        {
                            AlbumID = albumToUpdate.ID,
                            GenreID = cat.ID
                        });
                    }
                }
                else
                {
                    if (albumGenres.Contains(cat.ID))
                    {
                        AlbumGenre courseToRemove
                        = albumToUpdate
                        .AlbumGenres
                        .SingleOrDefault(i => i.GenreID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
