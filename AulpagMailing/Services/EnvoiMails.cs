using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AulpagMailing.Data;
using AulpagMailing.Models;
using System.Text.RegularExpressions;
using System.Linq;

namespace AulpagMailing.Services
{

    public class EnvoiMail
    {    
        
        public static  SmtpClient Connexion 
        {
            get
            {
                var smtp = Database.GetSmtpActif;
                
                SmtpClient SmtpServer = new SmtpClient
                {
                    Host = smtp.host,  //
                    Port = smtp.port,              //                                            
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                 
                Credentials = new NetworkCredential(smtp.compte, smtp.mdp),  //
                    Timeout = 20000,
                    EnableSsl =smtp.port > 400  
                };
                return SmtpServer;
            }
        }
    
        public static  string Mail(Envoi contact, mailings brouillon, ObservableCollection<pjs> ListPieces)       
        {                      
                try
                {                    
                    string from    = "contact@paris-granville.org";                
                    string copie = App.Staticparametres.FirstOrDefault(x => x.id_parametre == 1).parametre;
                    string subject =  brouillon.objet_mailing;
                 
                           MailMessage message = new MailMessage(from, contact.email);
                           message.Bcc.Add(copie);                      
                           message.Subject = brouillon.objet_mailing;
                           message.Headers.Add("Disposition-Notification-To", copie);           
                           message.Headers.Add("Return-Receipt-To", copie);
                           message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;


                     try
                           {
                               LinkedResource LinkedImage = new LinkedResource(brouillon.signature);
                               LinkedImage.ContentId = "MyPic";
                               AlternateView htmlView = AlternateView.CreateAlternateViewFromString(contact.contenu + "<img src=cid:MyPic>", null, "text/html");
                               htmlView.LinkedResources.Add(LinkedImage);
                               message.AlternateViews.Add(htmlView);
                           }
                           catch
                           {
                               AlternateView htmlView = AlternateView.CreateAlternateViewFromString(contact.contenu, null, "text/html");
                               message.AlternateViews.Add(htmlView);
                           }
                           foreach (var item in ListPieces)
                           {
                               Attachment PJ = new Attachment(item.piece);
                               message.Attachments.Add(PJ);
                           }

                           App.SmtpServer.Send(message);                           
                       return  "";

                }
                catch (Exception e)
                {
                     return e.Message;        
                }
           
          

        }

        /*
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;             
                "philippe.andanson@gmail.com", "qblivxkbtmadaatj

                  smtpClient.Host = "smtp.gmail.com";
                  smtpClient.Port = 587;             
                 "aulpag.test@gmail.com", "I3cTest123 "j
        */


        public static class CheckEmail
        {
            // Expression régulière utilisée pour valider l'adresse e-mail.
            public const string motif =
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            public static bool IsEmail(string email)
            {
                if (email != null) return Regex.IsMatch(email, motif);
                else return false;
            }
        }
    }
}
