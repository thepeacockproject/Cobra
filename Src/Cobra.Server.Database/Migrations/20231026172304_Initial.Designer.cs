﻿// <auto-generated />
using System;
using Cobra.Server.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cobra.Server.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20231026172304_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("Cobra.Server.Database.Models.Contract", b =>
                {
                    b.Property<uint>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CheckpointIndex")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Difficulty")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DisplayId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ExitId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LevelIndex")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OutfitToken")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Restrictions")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Target1Id")
                        .HasColumnType("INTEGER");

                    b.Property<uint?>("Target2Id")
                        .HasColumnType("INTEGER");

                    b.Property<uint?>("Target3Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<ulong>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WeaponToken")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DisplayId")
                        .IsUnique();

                    b.HasIndex("Target1Id");

                    b.HasIndex("Target2Id");

                    b.HasIndex("Target3Id");

                    b.HasIndex("UserId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Cobra.Server.Database.Models.ContractTarget", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmmoType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OutfitToken")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpecialSituation")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WeaponToken")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Name", "WeaponToken", "OutfitToken", "AmmoType", "SpecialSituation")
                        .IsUnique();

                    b.ToTable("ContractTargets");
                });

            modelBuilder.Entity("Cobra.Server.Database.Models.ScoreSniper", b =>
                {
                    b.Property<ulong>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.ToTable("ScoresSniper");
                });

            modelBuilder.Entity("Cobra.Server.Database.Models.ScoreStory", b =>
                {
                    b.Property<uint>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ScoresStory");
                });

            modelBuilder.Entity("Cobra.Server.Database.Models.ScoreTutorial", b =>
                {
                    b.Property<ulong>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.ToTable("ScoresTutorial");
                });

            modelBuilder.Entity("Cobra.Server.Database.Models.User", b =>
                {
                    b.Property<ulong>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompetitionPlays")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ContractPlays")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Country")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Trophies")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Wallet")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Country");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Cobra.Server.Database.Models.UserContract", b =>
                {
                    b.Property<ulong>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("ContractId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastPlayedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("Liked")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Plays")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Queued")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Score")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "ContractId");

                    b.HasIndex("ContractId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId", "Queued");

                    b.ToTable("UserContracts");
                });

            modelBuilder.Entity("Cobra.Server.Database.Models.UserFriend", b =>
                {
                    b.Property<ulong>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("SteamId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "SteamId");

                    b.ToTable("UserFriends");
                });

            modelBuilder.Entity("Cobra.Server.Database.Models.Contract", b =>
                {
                    b.HasOne("Cobra.Server.Database.Models.ContractTarget", "Target1")
                        .WithMany()
                        .HasForeignKey("Target1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cobra.Server.Database.Models.ContractTarget", "Target2")
                        .WithMany()
                        .HasForeignKey("Target2Id");

                    b.HasOne("Cobra.Server.Database.Models.ContractTarget", "Target3")
                        .WithMany()
                        .HasForeignKey("Target3Id");

                    b.HasOne("Cobra.Server.Database.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Target1");

                    b.Navigation("Target2");

                    b.Navigation("Target3");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cobra.Server.Database.Models.ScoreSniper", b =>
                {
                    b.HasOne("Cobra.Server.Database.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cobra.Server.Database.Models.ScoreStory", b =>
                {
                    b.HasOne("Cobra.Server.Database.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cobra.Server.Database.Models.ScoreTutorial", b =>
                {
                    b.HasOne("Cobra.Server.Database.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cobra.Server.Database.Models.UserContract", b =>
                {
                    b.HasOne("Cobra.Server.Database.Models.Contract", "Contract")
                        .WithMany()
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cobra.Server.Database.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cobra.Server.Database.Models.UserFriend", b =>
                {
                    b.HasOne("Cobra.Server.Database.Models.User", "User")
                        .WithMany("Friends")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cobra.Server.Database.Models.User", b =>
                {
                    b.Navigation("Friends");
                });
#pragma warning restore 612, 618
        }
    }
}
