﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskPlanner.Context;

#nullable disable

namespace TaskPlanner.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("TaskPlanner.Models.Activities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ActivityEndTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("ActivityName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ActivityStartTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("PlannedTasksId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlannedTasksId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("TaskPlanner.Models.PlannedTasks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("PlannedTasks");
                });

            modelBuilder.Entity("TaskPlanner.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaskPlanner.Models.Activities", b =>
                {
                    b.HasOne("TaskPlanner.Models.PlannedTasks", "PlannedTasks")
                        .WithMany("Activities")
                        .HasForeignKey("PlannedTasksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlannedTasks");
                });

            modelBuilder.Entity("TaskPlanner.Models.PlannedTasks", b =>
                {
                    b.HasOne("TaskPlanner.Models.User", "User")
                        .WithOne("PlannedTasks")
                        .HasForeignKey("TaskPlanner.Models.PlannedTasks", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskPlanner.Models.PlannedTasks", b =>
                {
                    b.Navigation("Activities");
                });

            modelBuilder.Entity("TaskPlanner.Models.User", b =>
                {
                    b.Navigation("PlannedTasks")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
