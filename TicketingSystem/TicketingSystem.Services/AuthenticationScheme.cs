using System;

namespace TicketingSystem.Services
{
    public class AuthenticationScheme
    {
        public AuthenticationScheme(string name, string displayName, Type handlerType)
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.HandlerType = handlerType;
        }
        
        public string Name { get; }
        
        public string DisplayName { get; }
        
        public Type HandlerType { get; }
    }
}
