using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using CodeWarriors.IITDU.Models;

namespace CodeWarriors.IITDU.Hubs
{
    [HubName("productHub")]

    public class ProductHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void UpdateClientsAboutLastProductHub(Product lastProductData)
        {
            Clients.Others.updateAboutNewProduct(lastProductData);
        }
    }
}