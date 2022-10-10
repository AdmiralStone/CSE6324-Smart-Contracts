using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NethereumWebUI.Data
{
    public partial class SmartContractsContext : DbContext
    {
        public SmartContractsContext()
        {
        }

        public SmartContractsContext(DbContextOptions<SmartContractsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contract> Contracts { get; set; } = null!;
        public virtual DbSet<ToDoList> ToDoLists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-EAIV8GK\\SQLEXPRESS;Initial Catalog=SmartContracts;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasKey(e => e.FileId)
                    .IsClustered(false);

                entity.ToTable("Contract");

                entity.Property(e => e.FileName).HasMaxLength(50);

                entity.Property(e => e.FileType).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("date");

                entity.Property(e => e.Size).HasMaxLength(50);

                entity.Property(e => e.UploadedBy).HasMaxLength(50);
            });

            modelBuilder.Entity<ToDoList>(entity =>
            {
                entity.HasKey(e => e.ToDoId);

                entity.ToTable("ToDoList");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
