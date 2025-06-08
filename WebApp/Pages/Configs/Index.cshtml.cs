using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace WebApp.Pages_Configs
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<Configuration> Configuration { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (!_context.Configurations.Any())
            {
                foreach (var config in RepoManager.ConfigRepositoryHardcoded.GetConfigurations())
                {
                    _context.Configurations!.Add(
                        new Configuration()
                        {
                            Name = config.Name,
                            BoardSizeWidth = config.BoardSizeWidth,
                            BoardSizeHeight = config.BoardSizeHeight,
                            WinCondition = config.WinCondition,
                            AmountOfPieces = config.AmountOfPieces
                        }
                        );
                }
            
                await _context.SaveChangesAsync();
            }
            Configuration = await _context.Configurations.ToListAsync();
        }
    }
}
