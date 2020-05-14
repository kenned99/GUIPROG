using ServerSide.Model;
using ServerSide;
using System;
using System.Collections.Generic;
using System.Text;
using ServerSide.DTOObject;

namespace ServerSide
{
    public interface IServerSideAccess
    {
        public Member GetMember(int id);
        public GpsLocation GetGpsLocation(int Id);
        public IEnumerable<Member> GetMembersByName(string name = null);
        public Member GetMemberByName(string Username);
        public Member AddMember(Member Member);
        public Member UpdateMember(Member UpdatedMember);
        public int DeleteMember(int id);
        public int Commit();
        public GpsLocation GetGpsLocations(int id);
        public DTOGps AddGpsLoc(DTOGps gps);
        public GpsLocation UpdateGpsLocation(GpsLocation gps);
        public Message SendMessage(SendMessageInfo DTO);
        public IEnumerable<Message> RecieveMessage(RecieveMessageInfo GTO);
    }
}
