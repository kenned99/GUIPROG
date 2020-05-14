using System;
using System.Collections.Generic;
using System.Text;
using ServerSide.Model;
using System.Linq;
using System.Data.Entity;
using ServerSide.DTOObject;
using System.Linq.Expressions;

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
        public GpsLocation UpdateGpsLocation(GpsLocation NewGps)
        {

            var OldGps = GetGpsLocation(NewGps.Id);
            db.Entry(OldGps).CurrentValues.SetValues(NewGps);
            return OldGps;
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

        public IEnumerable<Message> RecieveMessage(RecieveMessageInfo GTO)
        {

             return db.Messages.Where(x => ((x.SenderPersonId == GTO.SenderId) && (x.RecipientPersonId == GTO.RecipientId) || (x.RecipientPersonId == GTO.SenderId) && (x.SenderPersonId == GTO.RecipientId))).ToList();
        }
        
        public GpsLocation GetGpsLocations(int id)
        {

            return db.GpsLocations.FirstOrDefault(x => x.Id == id);

        }

        public DTOGps AddGpsLoc(DTOGps gps)
        {


            if (gps.UserId != 0)
            {

                if ( db.Members.FirstOrDefault(x => x.Id == gps.UserId)?.LastKnownLocationId == null )
                {
                    GpsLocation dbgps = new GpsLocation();
                    dbgps.Latitude = gps.lat;
                    dbgps.Longtitude = gps.lng;

                    db.Add(dbgps);
                    Commit();

                    var person = db.Members.Find(gps.UserId);
                 

                    person.LastKnownLocationId = dbgps.Id;
                
                    UpdateMember(person);
                    Commit();
                
                }
                else
                {
                    Member member = GetMember(gps.UserId);
                    GpsLocation dbgps = new GpsLocation();
                    dbgps.Id = (int)member.LastKnownLocationId;

                    dbgps.Latitude = gps.lat;
                    dbgps.Longtitude = gps.lng;

                    UpdateGpsLocation(dbgps);
                    Commit();
                }
            
            }
                

            return gps;
        }




    }
}