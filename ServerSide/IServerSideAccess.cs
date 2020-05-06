﻿using ServerSide.Model;
using ServerSide;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSide
{
    public interface IServerSideAccess
    {
        public Member GetMember(int id);
        public IEnumerable<Member> GetMemberByName(string navn = null);
        public Member AddMember(Member Member);
        public Member UpdateMember(Member UpdatedMember);
        public int DeleteMember(int id);
        public int Commit();
    }
}
