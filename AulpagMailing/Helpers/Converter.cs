using System;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace AulpagMailing.Helpers
{
    class Converter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FlowDocument doc = new FlowDocument();

            string s = value as string;
            if (s != null)
            {
                using (StringReader reader = new StringReader(s))
                {
                    string newLine;
                    while ((newLine = reader.ReadLine()) != null)
                    {
                        Paragraph paragraph = null;
                        if (newLine.EndsWith(":."))
                        {
                            paragraph = new Paragraph
                        (new Run(newLine.Replace(":.", string.Empty)));
                            paragraph.Foreground = new SolidColorBrush(Colors.Blue);
                            paragraph.FontWeight = FontWeights.Bold;
                        }
                        else
                        {
                            paragraph = new Paragraph(new Run(newLine));
                        }

                        doc.Blocks.Add(paragraph);
                    }
                }
            }

            return doc;
        }
    }
}
