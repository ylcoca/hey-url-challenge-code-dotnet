using hey_url_challenge_code_dotnet.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HeyUrlChallengeCodeDotnet.Data
{
    public interface IProjectContext : IDisposable
    {
        public DbSet<Url> Urls { get; set; }
        public DbSet<Metric> Metric { get; set; }
        int SaveChanges();
    }

    public class ApplicationContext : DbContext, IProjectContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Url> Urls { get; set; }
        public DbSet<Metric> Metric { get; set; }
    }
}