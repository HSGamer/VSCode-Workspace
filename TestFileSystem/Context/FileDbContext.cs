using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestFileSystem.Models;

namespace TestFileSystem.Context
{
    public class FileDbContext : DbContext
    {
        public DbSet<FileEntry> Files { get; set; }

        public FileDbContext(DbContextOptions<FileDbContext> options) : base(options) { }
    }
}