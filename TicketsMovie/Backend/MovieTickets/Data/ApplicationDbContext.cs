using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieTickets.Data;
using MovieTickets.Entity;
using System.Reflection.Emit;

namespace ZingMp3API.Data
{
    public class ApplicationDbContext :IdentityDbContext <AccountIdentity,ApplicationRole,string>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<UserInfor> userInfors { get; set; }

        public DbSet<TypeMovie> TypeMovie { get; set; }
        public DbSet<LanguageMovie> LanguageMovie { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<DetailsTypeMovie> DetailsTypeMovie { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<DetailsTicket> DetailsTicket { get; set; }
        public DbSet<AccumulationAccount> AccumulationAccount { get; set; }
        public DbSet<HistoryPay> HistoryPay { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<ProductOrther> ProductOrther { get; set; }
        public DbSet<DetailsProductOrther> DetailsProductOrther { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DetailsProductOrther>()
           .HasKey(d => new { d.TicketId, d.ProductOrtherId });

            builder.Entity<DetailsTicket>()
          .HasKey(d => new { d.TicketId, d.MovieId });

            builder.Entity<DetailsTypeMovie>()
        .HasKey(d => new { d.MovieId, d.TypeMovieId });

            builder.Entity<DetailsTypeMovie>()
     .HasOne(d => d.TypeMovie)
     .WithMany(p => p.Details)
     .HasForeignKey(d => d.TypeMovieId)
     .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DetailsTypeMovie>()
    .HasOne(d => d.movie)
    .WithMany(p => p.DetailsTypeMovie)
    .HasForeignKey(d => d.MovieId)
    .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<DetailsProductOrther>()
      .HasOne(d => d.Ticket)
      .WithMany(p => p.DetailsProductOrthers)
      .HasForeignKey(d => d.TicketId)
      .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DetailsProductOrther>()
            .HasOne(d => d.ProductOrther)
            .WithMany(p => p.DetailsProducts)
            .HasForeignKey(d => d.ProductOrtherId)
            .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<DetailsTicket>()
            .HasOne(d => d.Ticket)
            .WithMany(p => p.DetailsTickets)
            .HasForeignKey(d => d.TicketId)
            .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<DetailsTicket>()
            .HasOne(d => d.Movie)
            .WithMany(p => p.DetailsTickets)
            .HasForeignKey(d => d.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
