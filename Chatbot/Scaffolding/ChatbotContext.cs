﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Chatbot
{
    public partial class ChatbotContext : DbContext
    {
        public ChatbotContext()
        {
        }

        public ChatbotContext(DbContextOptions<ChatbotContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Association> Associations { get; set; } = null!;
        public virtual DbSet<Sentence> Sentences { get; set; } = null!;
        public virtual DbSet<Word> Words { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(Program.settings.Connection, Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.15-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");

            modelBuilder.Entity<Association>(entity =>
            {
                entity.ToTable("associations");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.SentenceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("sentence_id");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.Property(e => e.WordId)
                    .HasColumnType("int(11)")
                    .HasColumnName("word_id");
            });

            modelBuilder.Entity<Sentence>(entity =>
            {
                entity.ToTable("sentences");

                entity.HasIndex(e => e.Sentence1, "sentence_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Sentence1).HasColumnName("sentence");

                entity.Property(e => e.Used)
                    .HasColumnType("int(11)")
                    .HasColumnName("used");
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.ToTable("words");

                entity.HasIndex(e => e.Word1, "word_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Word1).HasColumnName("word");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}