using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualExpo.Model.Enums
{
  
    public enum UserRoleType
    {
        Admin=1,
        Organizer=2,
        Exhibitor=3,
        Attendee=4
    }

    public enum RegisterForExibitionAs
    {
        Exhibitor=1,
        Sponsor=2
    }
    public enum WorkingStatus
    {
        Past=1,
        Current=2
    }

    public enum ExhibitionStatus//Approved By Admin
    {
        Pending=1,
        Approved=2,
        Rejected=3
    }
    public enum GenderType
    {
        Male=1,
        Female=2,
        Other=3
    }
    public enum ExhibitionStatusActiveOrNot
    {
        Active=1,
        Inactive=2,
        End=3
    }


}
