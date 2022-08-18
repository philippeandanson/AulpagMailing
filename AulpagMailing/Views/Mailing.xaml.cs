using AulpagMailing.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;


namespace AulpagMailing.Views
{
    /// <summary>
    /// Logique d'interaction pour mailing.xaml
    /// </summary>
    public partial class Mailing : Window
    {       
        public Mailing()
        {
           
            InitializeComponent();      
            DataContext = new MailingsViewModel();

            var installedFontCollection = new System.Drawing.Text.InstalledFontCollection();

            // Get the array of FontFamily objects.
            var fontFamilies = installedFontCollection.Families;
            foreach (var fontFamily in fontFamilies)
            {
                var mfont = new FontFamily(fontFamily.Name);
                FontFamilyCombo.Items.Add(mfont);
            }

    //       FontFamilyCombo.ItemsSource = Fonts.SystemFontFamilies.OrderBy(fontFamily => fontFamily.Source);
      
            FontSizeCombo.Items.Add("10");
            FontSizeCombo.Items.Add("12");
            FontSizeCombo.Items.Add("14");
            FontSizeCombo.Items.Add("18");
            FontSizeCombo.Items.Add("24");
            FontSizeCombo.Items.Add("36");
            FontSizeCombo.Items.Add("42");
            foreach (var item in App.Staticparametres)
            {
                int a = 6;
                switch (item.id_parametre)
                {                  
                    case 3:
                        bool t = int.TryParse(item.parametre,out a) ;
                        if (t) FontSizeCombo.SelectedIndex = int.Parse(item.parametre); else FontSizeCombo.SelectedIndex= 0;
                        break;
                    case 2:
                        bool y = int.TryParse(item.parametre, out a);
                        if (y) FontFamilyCombo.SelectedIndex = int.Parse(item.parametre); else FontFamilyCombo.SelectedIndex=0;
                        break;
                }
            }
                
      }


  

        /// <summary>
        /// Changes the font family of selected text.
        /// </summary>
        private void OnFontFamilyComboSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (FontFamilyCombo.SelectedItem == null) return;
            var fontFamily = FontFamilyCombo.SelectedItem.ToString();
            var textRange = new TextRange(TextBox.Selection.Start, TextBox.Selection.End);
            textRange.ApplyPropertyValue(TextElement.FontFamilyProperty, fontFamily);
        }

        /// <summary>
        /// Changes the font size of selected text.
        /// </summary>
        private void OnFontSizeComboSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Exit if no selection
            if (FontSizeCombo.SelectedItem == null) return;

            // clear selection if value unset
            if (FontSizeCombo.SelectedItem.ToString() == "{DependencyProperty.UnsetValue}")
            {
                FontSizeCombo.SelectedItem = null;
                return;
            }

            // Process selection
            var pointSize = FontSizeCombo.SelectedItem.ToString();
            var pixelSize = Convert.ToDouble(pointSize) * (96 / 72);
            var textRange = new TextRange(TextBox.Selection.Start, TextBox.Selection.End);
            textRange.ApplyPropertyValue(TextElement.FontSizeProperty, pixelSize);
        }

  
    }
}
public class SortAdorner : Adorner
{
    private static Geometry ascGeometry =
        Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

    private static Geometry descGeometry =
        Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

    public ListSortDirection Direction { get; private set; }

    public SortAdorner(UIElement element, ListSortDirection dir)
        : base(element)
    {
        this.Direction = dir;
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);

        if (AdornedElement.RenderSize.Width < 20)
            return;

        TranslateTransform transform = new TranslateTransform
            (
                AdornedElement.RenderSize.Width - 15,
                (AdornedElement.RenderSize.Height - 5) / 2
            );
        drawingContext.PushTransform(transform);

        Geometry geometry = ascGeometry;
        if (this.Direction == ListSortDirection.Descending)
            geometry = descGeometry;
        drawingContext.DrawGeometry(Brushes.Black, null, geometry);

        drawingContext.Pop();
    }

    internal class FontUtilities
    {
        internal static IEnumerable<FontFamily> Families
        {
            get
            {
                foreach (FontFamily font in Fonts.SystemFontFamilies)
                {
                    yield return font;
                }
                foreach (FontFamily fontFamily in Fonts.GetFontFamilies(new
                    Uri("pack://application:,,,/MyDllProject ;Component/Resources/")))
                {
                    yield return fontFamily;
                }
            }
        }
    }
}

