using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Livit.Data.EntityFramework;

namespace Livit.Data.EntityFramework.Migrations
{
    [DbContext(typeof(LivitDbContext))]
    [Migration("20170205004627_AddTokenResponseEntity")]
    partial class AddTokenResponseEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("Livit.Model.Entities.EmployeeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Livit.Model.Entities.RequestedLeaveEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AppliedFrom");

                    b.Property<DateTime>("AppliedTo");

                    b.Property<DateTime>("ApprovedDate");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<int>("EmployeeId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Leaves");
                });

            modelBuilder.Entity("Livit.Model.Entities.TokenResponseEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccessToken")
                        .IsRequired();

                    b.Property<long?>("ExpiresInSeconds");

                    b.Property<DateTime>("Issued");

                    b.Property<string>("RefreshToken");

                    b.Property<string>("Scope");

                    b.Property<string>("TokenType");

                    b.HasKey("Id");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("Livit.Model.Entities.RequestedLeaveEntity", b =>
                {
                    b.HasOne("Livit.Model.Entities.EmployeeEntity", "Employee")
                        .WithMany("Leaves")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
