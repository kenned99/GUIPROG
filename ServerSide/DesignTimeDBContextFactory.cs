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
            builder.UseSqlServer(@"Server=tcp:gps-friends.database.windows.net,1433;Initial Catalog=GPS-Friends;Persist Security Info=False;User ID=seppi;Password=AdderBadder!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            return new MembersDBContext(builder.Options);
        }
    }
}
