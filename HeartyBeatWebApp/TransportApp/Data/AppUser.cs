using HeartyBeatApp.Models;
using Microsoft.AspNetCore.Identity;

namespace HeartyBeat.Data
{
    public class AppUser:IdentityUser
    {
        public virtual List<Reward>? Obtained { get; set; }
    }
}
