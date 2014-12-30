﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaRecepcionMail
{
    class Program
    {
        static void Main(string[] args)
        {
            SmtPop.POP3Client pop = new SmtPop.POP3Client();
            pop.Open("pop.gmail.com", 110, "santiagotommasi92", "marlou1006");

            // Get message list from POP server
            SmtPop.POPMessageId[] messages = pop.GetMailList();
            if (messages != null) {

                // Walk attachment list
                foreach(SmtPop.POPMessageId id in messages) {
                    SmtPop.POPReader reader= pop.GetMailReader(id);
                    SmtPop.MimeMessage msg = new SmtPop.MimeMessage();

                    // Read message
                    msg.Read(reader);
                    if (msg.AddressFrom != null) {
                        String from= msg.AddressFrom[0].Name;
                        Console.WriteLine("from: " + from);
                    }
                    if (msg.Subject != null) {
                        String subject = msg.Subject;
                        Console.WriteLine("subject: "+ subject);
                    }
                    if (msg.Body != null) {
                        String body = msg.Body;
                        Console.WriteLine("body: " + body);
                    }
                    if (msg.Attachments != null && false) {
                        // Do something with first attachment
                        SmtPop.MimeAttachment attach = msg.Attachments[0];

                        if (attach.Filename == "data") {
                           // Read data from attachment
                           Byte[] b = Convert.FromBase64String(attach.Body);
                           System.IO.MemoryStream mem = new System.IO.MemoryStream(b, false);

                           //BinaryFormatter f = new BinaryFormatter();
                           // DataClass data= (DataClass)f.Deserialize(mem); 
                           mem.Close();
                        }                     

                        // Delete message
                        // pop.Dele(id.Id);
                    }
               }
           }    
           pop.Quit();
        
        }
    }
}
