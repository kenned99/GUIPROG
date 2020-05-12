using System;
using System.Collections.Generic;
using System.Text;
using ServerSide.Model;
using System.Linq;
using System.Data.Entity;

namespace ServerSide
{
    public class ServerSideAccess : IServerSideAccess
    {
        //Initial DB Setup
        private readonly MembersDBContext db;

        public ServerSideAccess(MembersDBContext db)
        {
            this.db = db;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        //Member stuff
        public Member GetMember(int Id)
        {
            return db.Members.Find(Id);
        }

        public GpsLocation GetGpsLocation(int Id)
        {
            return db.GpsLocations.Find(Id);
        }

        public IEnumerable<Member> GetMembersByName(string name = null)
        {
            return db.Members.Where(x => x.Username.StartsWith(name)|| name == null).Include(x => x.LastKnownLocation).ToList();
        }

        public Member GetMemberByName(string Username)
        {
            return db.Members.Where(x => x.Username == Username).FirstOrDefault();
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

        //GPS stuff
        public int UpdateLocation(GpsLocation GpsLocation, Member Member)
        {
            Member.LastKnownLocation = GpsLocation;
            UpdateMember(Member);
            return 1;
        }

        //Message stuff
        public Message SendMessage(int SenderID, int RecipientID, string MessageText)
        {
            Message Message = new Message();
            Message.SenderPersonId = SenderID;
            Message.RecipientPersonId = RecipientID;
            Message.MessageText = MessageText;

            db.Add(Message);
            return Message;
        }

        public GpsLocation GetGpsLocations(int id)
        {

            return db.GpsLocations.FirstOrDefault(x => x.Id == id);

        }
    }
}