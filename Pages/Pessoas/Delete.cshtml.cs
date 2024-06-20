using FullStackWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FullStackWebApp.Pages_Pessoas
{
    public class DeleteModel : PageModel
    {
        private readonly FullStackMvcApp.Data.ApplicationDbContext _context;

        public DeleteModel(FullStackMvcApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Pessoas == null)
            {
                return NotFound();
            }
            var pessoa = await _context.Pessoas.FindAsync(id);

            if (pessoa != null)
            {
                Pessoa = pessoa;
                _context.Pessoas.Remove(Pessoa);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Pessoa excluída com sucesso!";
            }

            return RedirectToPage("./Index");
        }
    }
}
