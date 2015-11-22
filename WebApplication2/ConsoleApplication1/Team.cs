using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Team : ICloneable
    {
        public List<Employee> TeamMembers = new List<Employee>();

        public Team()
        {
        }

        private Team(List<Employee> members)
        {
            foreach (Employee e in members)
            {
                TeamMembers.Add(e.Clone() as Employee);
            }
        }

        // Adds an Employee object to the Team.
        public void AddMember(Employee member)
        {
            TeamMembers.Add(member);
        }

        // Override Object.ToString method to return a string representation of the team.
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Employee e in TeamMembers)
            {
                sb.AppendFormat("  {0}\r\n", e);
            }

            return sb.ToString();
        }

        // Implementation of ICloneable.Clone.
        public object Clone()
        {
            return new Team(this.TeamMembers);

            // the following code would create a shallow copy of the team.
            //return MemberwiseClone();
        }
    }

}
