using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using CBA.Data;
using CBA.Data.Implementation;
using CBA.Data.Interface;

namespace CBA.Logic
{   
 public  class UserLogic
    {
        private readonly UserRepository _db = new UserRepository(new ApplicationDbContext());
        private readonly BranchRepository _branch = new BranchRepository(new ApplicationDbContext());
        private readonly RoleRepository _role = new RoleRepository(new ApplicationDbContext());

        public void Save(User user)
        {
            //Console.WriteLine("trying to add user");
            _db.Add(user);
            _db.Save(user);
        }
        public User Get(int id)
        {

            var values = _db.Get(id);
            return values;
        }

        public void Update(User user)
        {
            _db.Save(user);
        }

        public IEnumerable<User> GetAll()
        {
            var values = _db.GetAll();
            return values;
        }

        public void Remove(User user)
        {
            _db.Remove(user);
            _db.Save(user);
        }

        public IEnumerable<User> GetAllWithBranch()
        {
            var values = _db.GetAllWithBranch();
            return values;
        }

        public IEnumerable<Branch> GetBranches()
        {
            return _branch.GetAll();
        }
        public IEnumerable<UserRole> GetRoles()
        {
            return _role.GetAll();
        }

        public bool UserIsUnique(string username)
        {
            bool isUnique = false;
            
            var user = _db.GetByUsername(username);
            if (user == null)
                isUnique = true;

            return isUnique;
        }
        public bool EmailIsUnique(string userMail)
        {
            bool isUnique = false;

            var email = _db.GetByEmail(userMail);
            if (email == null)
                isUnique = true;

            return isUnique;
        }

        public string GenerateRandomPassword()
        {
            {
                int passwordLength = 8;
                string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
                Random random = new Random();
                char[] chars = new char[passwordLength];

                for (int i = 0; i < passwordLength; i++)
                {
                    chars[i] = validChars[(int)((validChars.Length) * random.NextDouble())];
                }
                return new string(chars);
            }
        }

        public void SendEmail(string email, string password, string fullName)
        {
            var adminEmail = new MailAddress("moyinobey@gmail.com", "Moyin CBA app");
            var userEmail = new MailAddress(email);
            var adminMailPassword = "photography";
            string subject = $"{fullName}, Your login  details !!!";

            string body =
                $"<br/><br/>Your account has been created successfully. Do not disclose to anyone <br> Username:  {email}  <br> password: {password}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(adminEmail.Address, adminMailPassword)
            };

            using (var message = new MailMessage(adminEmail, userEmail)
            {

                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

                smtp.Send(message);

        }

        public IEnumerable<User> GetAllUnassigned()
        {
            var values = _db.GetAllUnassigned();
            return values;
        }

        public User GetUnassigned(int id)
        {
            var values = _db.GetUnassigned(id);
            return values;
        }
    }

}
