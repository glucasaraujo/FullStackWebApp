using FullStackWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FullStackWebApp.Pages_Pessoas
{
    public class CreateModel : PageModel
    {
        private readonly FullStackMvcApp.Data.ApplicationDbContext _context;

        public CreateModel(FullStackMvcApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Pessoa Pessoa { get; set; } = default!;

        [BindProperty]
        public List<Dependente> Dependentes { get; set; } = new List<Dependente>();
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Pessoas == null || Pessoa == null)
            {
                return Page();
            }
            Pessoa.Dependentes = Dependentes;

            _context.Pessoas.Add(Pessoa);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Pessoa e Dependente adicionado com sucesso!";

            return RedirectToPage("./Index");
        }
    }
}
