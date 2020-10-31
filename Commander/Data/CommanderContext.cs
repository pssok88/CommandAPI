using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt): base(opt)
        {
            
        }

        public DbSet<Command> Commands { get; set; }
        
    }
}