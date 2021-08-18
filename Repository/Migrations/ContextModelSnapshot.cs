﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository;

namespace Repository.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Entity.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Body")
                        .HasColumnType("text");

                    b.Property<int?>("FromUserId")
                        .HasColumnType("integer");

                    b.Property<int?>("MessageGroupId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FromUserId");

                    b.HasIndex("MessageGroupId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Entity.MessageGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MessageGroups");
                });

            modelBuilder.Entity("Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("MessageGroupId")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MessageGroupId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Entity.Message", b =>
                {
                    b.HasOne("Entity.User", "FromUser")
                        .WithMany()
                        .HasForeignKey("FromUserId");

                    b.HasOne("Entity.MessageGroup", null)
                        .WithMany("Messages")
                        .HasForeignKey("MessageGroupId");

                    b.Navigation("FromUser");
                });

            modelBuilder.Entity("Entity.User", b =>
                {
                    b.HasOne("Entity.MessageGroup", null)
                        .WithMany("Users")
                        .HasForeignKey("MessageGroupId");
                });

            modelBuilder.Entity("Entity.MessageGroup", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
