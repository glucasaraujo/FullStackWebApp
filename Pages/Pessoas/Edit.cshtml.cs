using FullStackWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FullStackWebApp.Pages_Pessoas
{
    public class EditModel : PageModel
    {
        private readonly FullStackMvcApp.Data.ApplicationDbContext _context;

        public EditModel(FullStackMvcApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pessoa Pessoa { get; set; } = default!;

        [BindProperty]
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
            Pessoa = pessoa;
            Dependentes = pessoa.Dependentes.ToList();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Pessoa).State = EntityState.Modified;

            foreach (var dependente in Dependentes)
            {
                if (dependente.Id == 0)
                {
                    _context.Dependente.Add(dependente);
                }
                else
                {
                    _context.Attach(dependente).State = EntityState.Modified;
                }
            }
                try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Pessoa e dependentes atualizados com sucesso!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(Pessoa.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PessoaExists(int id)
        {
            return (_context.Pessoas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
