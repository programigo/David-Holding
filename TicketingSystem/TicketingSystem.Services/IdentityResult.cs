using System.Collections.Generic;

namespace TicketingSystem.Services
{
    public class IdentityResult
    {
        public static IdentityResult Success { get; set; }
        
        public bool Succeeded { get; set; }
       
        public IEnumerable<IdentityError> Errors { get; set; }

        //public static IdentityResult Failed(params IdentityError[] errors);
    }
}
