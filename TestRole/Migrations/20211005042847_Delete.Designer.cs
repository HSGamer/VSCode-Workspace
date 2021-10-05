﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestRole.Context;

namespace TestRole.Migrations
{
    [DbContext(typeof(RoleDbContext))]
    [Migration("20211005042847_Delete")]
    partial class Delete
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("TestRole.Models.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("TestRole.Models.Classroom", b =>
                {
                    b.Property<int>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("TrainerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ClassId");

                    b.HasIndex("TrainerId");

                    b.ToTable("Classrooms");
                });

            modelBuilder.Entity("TestRole.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ClassId")
                        .HasColumnType("INTEGER");

                    b.HasKey("StudentId");

                    b.HasIndex("ClassId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("TestRole.Models.Trainer", b =>
                {
                    b.Property<int>("TrainerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TrainerId");

                    b.ToTable("Trainers");
                });

            modelBuilder.Entity("TestRole.Models.Classroom", b =>
                {
                    b.HasOne("TestRole.Models.Trainer", "Trainer")
                        .WithMany("Classrooms")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("TestRole.Models.Student", b =>
                {
                    b.HasOne("TestRole.Models.Classroom", "Classroom")
                        .WithMany("Students")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TestRole.Models.Account", "Account")
                        .WithOne("Student")
                        .HasForeignKey("TestRole.Models.Student", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Classroom");
                });

            modelBuilder.Entity("TestRole.Models.Trainer", b =>
                {
                    b.HasOne("TestRole.Models.Account", "Account")
                        .WithOne("Trainer")
                        .HasForeignKey("TestRole.Models.Trainer", "TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("TestRole.Models.Account", b =>
                {
                    b.Navigation("Student");

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("TestRole.Models.Classroom", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("TestRole.Models.Trainer", b =>
                {
                    b.Navigation("Classrooms");
                });
#pragma warning restore 612, 618
        }
    }
}
