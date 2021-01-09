using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Pages.RecordLabels
{
    public class IndexModel : PageModel
    {
        private readonly MusicStore.Data.MusicStoreContext _context;

        public IndexModel(MusicStore.Data.MusicStoreContext context)
        {
            _context = context;
        }

        public IList<RecordLabel> RecordLabel { get;set; }

        public async Task OnGetAsync()
        {
            RecordLabel = await _context.RecordLabel.ToListAsync();
        }
    }
}
