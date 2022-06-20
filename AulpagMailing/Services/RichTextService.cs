using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using Xceed.Wpf.Toolkit;

namespace AulpagMailing.Services
{
    class RichTextService
    {

        public static TextRange TextFromRichTextBox(string contenu)
        {
            Paragraph ParagraphSrc = new Paragraph();
            TextRange textRangeSrc = new TextRange(ParagraphSrc.ContentStart, ParagraphSrc.ContentEnd);
            
            string backText = textRangeSrc.Text; //sauvegarde texte d'origine
            string hideText = "";
            string t = contenu.Replace("<nom>", "Nom");

            foreach (Char c in t) { hideText += c; }


            textRangeSrc.Text = hideText;
            Paragraph ParagraphDest = new Paragraph();
            TextRange textRangeDest = new TextRange(ParagraphDest.ContentStart, ParagraphDest.ContentEnd);
            // textRangeDest

            using (MemoryStream rtfMemoryStream = new MemoryStream())
            {
                textRangeSrc.Save(rtfMemoryStream, DataFormats.Xaml, true);
                textRangeDest.Load(rtfMemoryStream, DataFormats.Xaml);
            }

            // ne pas oublier de restorer le text d'origine ...qui est ecrase par l'operation 
            // precedente
            textRangeSrc.Text = backText;

            return textRangeSrc;


        }
  
    }

    public class MyXamlFormatter : ITextFormatter
    {
        public string GetText(System.Windows.Documents.FlowDocument document)
        {
            TextRange tr = new TextRange(document.ContentStart, document.ContentEnd);
            using (MemoryStream ms = new MemoryStream())
            {
                tr.Save(ms, DataFormats.Xaml);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public void SetText(System.Windows.Documents.FlowDocument document, string text)
        {
            try
            {
                if (String.IsNullOrEmpty(text))
                {
                    document.Blocks.Clear();
                }
                else
                {
                    TextRange tr = new TextRange(document.ContentStart, document.ContentEnd);
                    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(text)))
                    {
                        tr.Load(ms, DataFormats.Rtf);
                    }
                }
            }
            catch
            {
                throw new InvalidDataException("Data provided is not in the correct Xaml format.");
            }
        }
    }
}
