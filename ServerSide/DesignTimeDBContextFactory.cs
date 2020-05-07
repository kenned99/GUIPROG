using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSide
{
    public class DesignTimeDBContextFactory : IDesignTimeDbContextFactory<MembersDBContext>
    {
        public MembersDBContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MembersDBContext>();
            builder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GPS-Friends;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            return new MembersDBContext(builder.Options);
        }
    }
}
