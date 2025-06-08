using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;

namespace WebApp.Pages_Configs
{
    public class CreateModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public CreateModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Configuration Configuration { get; set; } = default!;
        [BindProperty] public string? Error { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (string.IsNullOrEmpty(Configuration.Name))
            {
                Error = "Please enter config's name";
                return Page();
            }
            if (Configuration.BoardSizeWidth < 3)
            {
                Error = "Board width must be at least 3 or more";
                return Page();
            }
            if (Configuration.BoardSizeHeight < 3)
            {
                Error = "Board height must be at least 3 or more";
                return Page();
            }
            if (Configuration.AmountOfPieces < 3)
            {
                Error = "Amount of pieces must be at least 3 or more";
                return Page();
            }
            if (Configuration.WinCondition > Configuration.AmountOfPieces)
            {
                Error = "Win condition can't be smaller than amount of pieces";
                return Page();
            }
            _context.Configurations.Add(Configuration);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
