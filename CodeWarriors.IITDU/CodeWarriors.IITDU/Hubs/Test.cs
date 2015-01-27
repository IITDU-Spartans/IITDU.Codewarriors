using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace CodeWarriors.IITDU.Hubs
{
    public class Test : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}