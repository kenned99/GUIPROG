using System;
using System.Collections.Generic;
using System.Text;
using ServerSide.Model;
using System.Linq;

namespace ServerSide
{
    public class ServerSideAccess : IServerSideAccess
    {
        private readonly MembersDBContext db;

        public ServerSideAccess(MembersDBContext db)
        {
            this.db = db;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Member GetMember(int Id)
        {
            return db.Members.Find(Id);
        }


        public IEnumerable<Member> GetMemberByName(string name = null)
        {
            var query = from d in db.Members
                        where d.Username.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby d.Username
                        select d;
            return query;
        }

        public Member AddMember(Member NewPerson)
        {
            db.Add(NewPerson);
            return NewPerson;
        }

        public Member UpdateMember(Member UpdatedPerson)
        {
            db.Update(UpdatedPerson);
            return UpdatedPerson;
        }

        public int DeleteMember(int id)
        {
            var person = GetMember(id);
            if (person != null)
            {
                db.Members.Remove(person);
                return 1;
            }
            return 0;
        }
    }
}