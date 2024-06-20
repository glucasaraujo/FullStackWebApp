using FullStackWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FullStackWebApp.Pages_Pessoas
{
    public class DetailsModel : PageModel
    {
        private readonly FullStackMvcApp.Data.ApplicationDbContext _context;

        public DetailsModel(FullStackMvcApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Pessoa Pessoa { get; set; } = default!;

        public List<Dependente> Dependentes { get; set; } = new List<Dependente>();


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pessoas == null)
            {
                return NotFound();
            }
            var pessoa = await _context.Pessoas.Include(p => p.Dependentes)
                                                   .FirstOrDefaultAsync(m => m.Id == id);

            if (pessoa == null)
            {
                return NotFound();
            }
            else
            {
                Pessoa = pessoa;
                Dependentes = pessoa.Dependentes.ToList();
            }
            return Page();
        }
    }
}
