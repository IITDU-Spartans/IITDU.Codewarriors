﻿using CodeWarriors.IITDU.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace CodeWarriors.IITDU.Service
{
    public  class MailService
    {
        public  String pass="";


        public void SendMail(List<User> receivers, Product product )
        {
            Send(CreateMail(receivers,product));
        }

        private  void Send(MailMessage mailMessage)
        {
            var client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("abdussatterrifat92@gmail.com", "iit12345")
            };
            // client.UseDefaultCredentials = true;
            //client.Send();
            
            //client.Send("rifat92iitdu@gmail.com", "mvcprojectsa@gmail.com", "aaa", "aaa");           
            client.SendAsync(mailMessage, new object());
        }

        private  MailMessage CreateMail(List<User> receivers, Product product)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("abdussatterrifat92@gmail.com");
            
            foreach (var receiver in receivers)
            {
                mailMessage.To.Add(new MailAddress(receiver.Email));
            }
            mailMessage.To.Add(new MailAddress("mvcprojectsa@gmail.com"));
            mailMessage.Subject = "Vazar: New Product Available for You";
            mailMessage.Body += "<html>";
            mailMessage.Body += "<body>";
            mailMessage.Body += "<a href=\"http://localhost:33754/#/product/"+product.ProductId+"\">"+ product.ProductName+ "</a>";
            mailMessage.Body += "</body>";
            mailMessage.Body += "</html>";
            mailMessage.IsBodyHtml = true;
            return mailMessage;
        }
        

        

    }
}