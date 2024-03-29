﻿using Microsoft.EntityFrameworkCore;
using ServerSide.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSide
{
    public class MembersDBContext : DbContext
    {
        public MembersDBContext(DbContextOptions<MembersDBContext> options)
        : base(options)
        {

        }
        
        public DbSet<Member> Members { get; set; }
        public DbSet<GpsLocation> GpsLocations  { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
