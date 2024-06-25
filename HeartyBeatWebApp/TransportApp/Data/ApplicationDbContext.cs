using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HeartyBeat.Data;
using HeartyBeatApp.Models;

namespace HeartyBeatApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tracker>? Tracker { get; set; }

        public DbSet<Reward>? Reward{ get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<HeartyBeat.Data.AddYourTips>? HealthyTIpsPersonal { get; set; }
    }
}