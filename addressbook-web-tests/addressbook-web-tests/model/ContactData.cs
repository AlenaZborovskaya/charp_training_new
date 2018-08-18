using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmail;
        private string allContactInformation;

        public ContactData()
        {
        }
        public ContactData(string firstName)
        {
            Firstname = firstName;
        }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }
        [Column(Name = "middlename")]
        public string Middlename { get; set; }
        [Column(Name = "lastname")]
        public string Lastname { get; set; }
        [Column(Name = "address")]
        public string Address { get; set; }
        [Column(Name = "home")]
        public string HomePhone { get; set; }
        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }
        [Column(Name = "work")]
        public string WorkPhone { get; set; }
        [Column(Name = "fax")]
        public string Fax { get; set; }
        [Column(Name = "email")]
        public string Email { get; set; }
        [Column(Name = "email2")]
        public string Email2 { get; set; }
        [Column(Name = "email3")]
        public string Email3 { get; set; }
        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }
        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }


        public static List<ContactData> GetAllContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Contacts select g).ToList();
            }
        }

        public string AllEmail
        {
            get
            {
                if (allEmail != null)
                {
                    return allEmail;
                }
                else
                {
                    return (Transfer(Email) + Transfer(Email2) + Transfer(Email3)).Trim();
                }
            }
            set
            {
                allEmail = value;
            }
        }
        public string AllContactInformation
        {
            get
            {
                if (allContactInformation != null)
                {
                    return allContactInformation;
                }
                else
                {
                    return Gap(Firstname) + Transfer(Lastname) + Transfer(Address) + "\r\n" + "H: " + Transfer(HomePhone) + "M: " + Transfer(MobilePhone) + "W: " + Transfer(WorkPhone).Trim();
                }
            }
            set
            {
                allContactInformation = value;
            }

        }


        public string AllPhones

        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone).Trim();
                }
            }
            set
            {
                allPhones = value;
            }

        }

        public string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ --()]", "") + "\r\n";
        }

        public string Transfer(string name)
        {
            if (name == null || name == "")
            {
                return "";
            }
            return name + "\r\n";
        }

        public string Gap(string name2)
        {
            if (name2 == null || name2 == "")
            {
                return "";
            }
            return name2 + " ";
        }




        public string Nickname { get; set; }

        

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null)) // если тот объект с которым мы сравниваем это null
            {
                return false; //так как текущий объект есть и он не null
            }
            if (Object.ReferenceEquals(this, other)) // те объекты совпадают
            {
                return true;
            }
            return Firstname == other.Firstname
           && Lastname == other.Lastname;

        }

        public override int GetHashCode()
        {
            return Firstname.GetHashCode();
        }

        public override string ToString()
        {
            return "firstname=" + Firstname + "\nlastname= " + Lastname + "\naddress" + Address + "\nhome" + HomePhone + "\nmobile" + MobilePhone + "\nwork" + WorkPhone;
        }


        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 0;
            }
            int srav = Lastname.CompareTo(other.Lastname);
            if (srav != 0)
            {
                return srav;
            }
            else
            {
                return Firstname.CompareTo(other.Firstname);
            }
        }
        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts where c.Deprecated == "0000-00-00 00:00:00" select c).ToList();
            }
        }
    }
}
