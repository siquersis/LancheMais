using LancheMais.WebApp.Entities;
using LancheMais.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LancheMais.WebApp.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Lanche> Lanche { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItem { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<PedidoDetalhe> PedidoDetalhe { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Categoria>()
                .Property(x => x.Nome)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<Categoria>()
                .Property(x => x.Descricao)
                .HasMaxLength(500);

            builder.Entity<Lanche>()
                .Property(x => x.Nome)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<Lanche>()
                .Property(x => x.DescricaoCurta)
                .HasMaxLength(150)
                .IsRequired();

            builder.Entity<Lanche>()
                .Property(x => x.DescricaoLonga)
                .HasMaxLength(500)
                .IsRequired();

            builder.Entity<Lanche>()
                .Property(x => x.Preco)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Entity<Lanche>()
                .Property(x => x.ImagemUrl)
                .HasMaxLength(2500)
                .IsRequired();

            builder.Entity<Lanche>()
                .Property(x => x.ImagemThumbnailUrl)
                .HasMaxLength(2500)
                .IsRequired();

            builder.Entity<Lanche>()
                .Property(x => x.CategoriaId)
                .IsRequired();

            builder.Entity<CarrinhoCompraItem>()
                .Property(x => x.Quantidade)
                .IsRequired();

            builder.Entity<CarrinhoCompraItem>()
                .Property(x => x.CarrinhoCompraId)
                .HasMaxLength(200)
                .IsRequired(false);
        }
    }
}