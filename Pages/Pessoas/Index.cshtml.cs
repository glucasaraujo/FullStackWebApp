using FullStackWebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FullStackWebApp.Pages_Pessoas
{
    public class IndexModel : PageModel
    {
        private readonly FullStackMvcApp.Data.ApplicationDbContext _context;

        public IndexModel(FullStackMvcApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Pessoa> Pessoa { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Pessoas != null)
            {
                Pessoa = await _context.Pessoas.ToListAsync();
            }
        }
    }
}
