using ServerSide.Model;
using ServerSide;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSide
{
    public interface IServerSideAccess
    {
        public Member GetMember(int id);
        public IEnumerable<Member> GetMembersByName(string name = null);
        public Member GetMemberByName(string Username);
        public Member AddMember(Member Member);
        public Member UpdateMember(Member UpdatedMember);
        public int DeleteMember(int id);
        public int Commit();
        public Message SendMessage(int SenderID, int RecipientID, string MessageText);
        public int UpdateLocation(GpsLocation GpsLocation, Member Member);
    }
}
