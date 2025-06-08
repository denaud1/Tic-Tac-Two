// See https://aka.ms/new-console-template for more information

using ConsoleApp;
using Microsoft.EntityFrameworkCore;


RepoManager.Context.Database.Migrate();
MainMenu.Run();
RepoManager.Context.SaveChanges();
