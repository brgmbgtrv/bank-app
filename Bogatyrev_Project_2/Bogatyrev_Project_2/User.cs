using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bogatyrev_Project_2
{
    class User : IEquatable<User>
    {
        public string _fullName { get; set; }
        public string _birthDate { get; set; }
        public string _phoneNumber { get; set; }
        
        public override string ToString()
        {
            return $"Full name: {this._fullName}\nDate of birth: {_birthDate}\nPhone number: {_phoneNumber}";
        }

        public bool Equals(User other)
        {
            if (this.ToString() == other.ToString())
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
