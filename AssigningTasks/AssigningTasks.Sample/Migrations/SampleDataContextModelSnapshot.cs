﻿// <auto-generated />
using System;
using AssigningTasks.Sample.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AssigningTasks.Sample.Migrations
{
    [DbContext(typeof(SampleDataContext))]
    partial class SampleDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085");

            modelBuilder.Entity("AssigningTasks.Sample.Data.Candidate", b =>
                {
                    b.Property<string>("CandidateId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsAssigned");

                    b.Property<double>("Latitude");

                    b.Property<int>("Load");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<int>("TotalTravel");

                    b.HasKey("CandidateId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("AssigningTasks.Sample.Data.Target", b =>
                {
                    b.Property<string>("TargetId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastRequest");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.HasKey("TargetId");

                    b.ToTable("Targets");
                });

            modelBuilder.Entity("AssigningTasks.Sample.Data.Transaction", b =>
                {
                    b.Property<string>("TransactionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Algorithm");

                    b.Property<TimeSpan>("AlgorithmExecutionTime");

                    b.Property<DateTime>("AssigneeAt");

                    b.Property<string>("Candidates");

                    b.Property<double>("Distance");

                    b.Property<DateTime>("FinishAt");

                    b.Property<string>("FromTargetId");

                    b.Property<bool>("IsFinished");

                    b.Property<int>("MaxLoad");

                    b.Property<DateTime>("RequestAt");

                    b.Property<string>("ToCandidateId");

                    b.HasKey("TransactionId");

                    b.HasIndex("FromTargetId");

                    b.HasIndex("ToCandidateId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("AssigningTasks.Sample.Data.Transaction", b =>
                {
                    b.HasOne("AssigningTasks.Sample.Data.Target", "From")
                        .WithMany()
                        .HasForeignKey("FromTargetId");

                    b.HasOne("AssigningTasks.Sample.Data.Candidate", "To")
                        .WithMany()
                        .HasForeignKey("ToCandidateId");
                });
#pragma warning restore 612, 618
        }
    }
}
