﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieVote.Db;

#nullable disable

namespace MovieVote.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220609153411_ReleaseDateNullable")]
    partial class ReleaseDateNullable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("MovieVote.Db.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AddedById")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImdbId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Overview")
                        .HasColumnType("TEXT");

                    b.Property<string>("Poster")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tagline")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TmdbId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AddedById");

                    b.HasIndex("TmdbId")
                        .IsUnique();

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieVote.Db.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OAuthAccessToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OAuthId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OAuthProvider")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OAuthRefreshToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SessionExpiry")
                        .HasColumnType("TEXT");

                    b.Property<string>("SessionId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OAuthId", "OAuthProvider")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MovieVote.Db.Movie", b =>
                {
                    b.HasOne("MovieVote.Db.User", "AddedBy")
                        .WithMany()
                        .HasForeignKey("AddedById");

                    b.Navigation("AddedBy");
                });
#pragma warning restore 612, 618
        }
    }
}
