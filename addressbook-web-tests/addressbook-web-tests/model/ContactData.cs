using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{

    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allContactInformation;

        public ContactData()
        {
        }
        public ContactData(string firstName)
        {
            Firstname = firstName;
        }
        public string Firstname { get; set; }

        public string Middlename { get; set; }

        public string Lastname { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }

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

        public string Id { get; set; }

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
           &&Lastname == other.Lastname;

        }

        public override int GetHashCode()
        {
            return Firstname.GetHashCode();
        }
        
        public override string ToString()
        {
            return "firstname=" + Firstname + "\nlastname= " + Lastname + "\naddress" + Address + "\nhome" + HomePhone +"\nmobile" + MobilePhone + "\nwork" + WorkPhone;
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
    }
    }
