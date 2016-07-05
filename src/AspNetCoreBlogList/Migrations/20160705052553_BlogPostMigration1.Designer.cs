using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AspNetCoreBlogList.Models;

namespace AspNetCoreBlogList.Migrations
{
    [DbContext(typeof(BlogPostContext))]
    [Migration("20160705052553_BlogPostMigration1")]
    partial class BlogPostMigration1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AspNetCoreBlogList.Models.BlogPost", b =>
                {
                    b.Property<int>("BlogPostId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AspNetCoreVersion");

                    b.Property<string>("BlogAuthor");

                    b.Property<byte[]>("BlogPostImage");

                    b.Property<string>("BlogPostLink")
                        .IsRequired();

                    b.Property<string>("BlogPostTitle")
                        .IsRequired();

                    b.Property<string>("TwitterHandle");

                    b.HasKey("BlogPostId");

                    b.ToTable("BlogPosts");
                });
        }
    }
}
