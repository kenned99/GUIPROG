using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServerSide;
using ServerSide.Model;

using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace H3GUI.Pages
{
    public class LoginModel : PageModel
    {

        private readonly IServerSideAccess serverSideAccess;


        [BindProperty]
        public Member Member { get; set; }

        public string ConfirmPassword { get; set; }



        public IEnumerable<Member> Members => serverSideAccess.GetMembersByName(Member.Username).OrderBy(x => x.Id);

        [BindProperty]
        public int MemberId { get; set; }

        public LoginModel(IServerSideAccess serverSideAccess)
        {
            this.serverSideAccess = serverSideAccess;

        }

        public void OnGet()
        {
        }

        private byte[] GetSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        private string EncryptPassword (string Password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public IActionResult OnPostRegister(Member Member)
        {
            Member.salt = GetSalt();
            string PlainPassword = Member.Password;
            Member.Password = EncryptPassword(PlainPassword, Member.salt);

            if (ModelState.IsValid && Members.Count() == 0)
            {
                serverSideAccess.AddMember(Member);
                serverSideAccess.Commit();
                OnPostLogin(Member.Username, PlainPassword);
            }
            return Page();
        }

        public IActionResult OnPostLogin(string Username, string Password)
        {
            Member = serverSideAccess.GetMemberByName(Username);
            if (Member != null && Member.Password == EncryptPassword(Password, Member.salt))
            {

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(Member);
                HttpContext.Session.SetInt32("sessionUser", Member.Id);
                

                MemberId = Member.Id;

                return RedirectToPage("/privacy");
                //TODO: Login functionality
            }
            TempData.Add("LastAction", "FAIL");
            return Page();

            //TODO: Failed login
        }
    }
}