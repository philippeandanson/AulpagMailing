using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AulpagMailing.Data;
using AulpagMailing.Models;

namespace AulpagMailing.Services
{

    public class EnvoiMail
    {

        public static void Mail(
            ObservableCollection<Envoi> ListDestinataires,
            mailings brouillon, ObservableCollection<pjs> ListPieces,
            Smtp smtp
            )
        {     
            Task.Run(() =>
            {
                try
                {                    
                    string from = "contact@paris-granville.org";
                    string copie = "";
                    string subject = brouillon.objet_mailing;
                   
                    SmtpClient smtpServer = new SmtpClient
                    {                     
                        Host = smtp.host,  //
                        Port = smtp.port,              //                                            
                        UseDefaultCredentials = false,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new NetworkCredential(smtp.compte, smtp.mdp),  //
                        Timeout = 20000,
                        EnableSsl = true,
                    };                            
                    foreach (var destinataire in ListDestinataires)
                    {    
                        MailMessage message = new MailMessage(from, destinataire.email);
                        message.Subject = brouillon.objet_mailing;                                   
                        LinkedResource LinkedImage = new LinkedResource(brouillon.signature);
                        LinkedImage.ContentId = "MyPic";                     
                        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(destinataire.contenu +" <img src=cid:MyPic>",null, "text/html");
                        htmlView.LinkedResources.Add(LinkedImage);                     
                        message.AlternateViews.Add(htmlView);
                        
                        foreach(var item in ListPieces)
                        {
                            Attachment PJ = new Attachment(item.piece);
                            message.Attachments.Add(PJ);
                        }
                        smtpServer.Send(message);
                     //   destinataire.date_envoi = DateTime.Now;
                     //   Database.UpdateEnvoiFromEmail(destinataire);
                    }

             
                    return true;
                  
                }
                catch (Exception e)
                {
                    var a = e;
                    return false;
                }

            });
        }

    /*
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;             
            "philippe.andanson@gmail.com", "qblivxkbtmadaatj
    */

    }
}
