namespace Library.Domain.Entities
{
    public class Person : BaseEntity
    {

        public Person(string name, string email, bool isAdmin)
        {
            Name = name;
            Email = email;
            IsAdmin = isAdmin;
        }


        public string Name { get; private set; }
        public string Email { get; private set; }
        public bool IsAdmin { get; private set; }

        public void UpdatePerson(string name, string email, bool isAdmin)
        {
            Name = name;
            Email = email;
            IsAdmin = isAdmin;
        }

        public void RemovePerson()
        {

        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateEmail(string email)
        {
            Email = email;
        }

        public void GiveAdminPermissions()
        {
            IsAdmin = true;
        }

        public void RemoveAdminPermissions()
        {
            IsAdmin = false;
        }
    }
}