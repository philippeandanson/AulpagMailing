// Updated by XamlIntelliSenseFileGenerator 23/05/2022 22:11:32
#pragma checksum "..\..\..\Views\SaveLoadPrintRTB.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E1CAA2537ABF4AE8633445619243970A2322546DFD43E895837B6EE37BCB4753"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace AulpagMailing.Views
{


    /// <summary>
    /// SaveLoadPrintRTB
    /// </summary>
    public partial class SaveLoadPrintRTB : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AulpagMailing;component/views/saveloadprintrtb.xaml", System.UriKind.Relative);

#line 1 "..\..\..\Views\SaveLoadPrintRTB.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.mainPanel = ((System.Windows.Controls.DockPanel)(target));
                    return;
                case 2:
                    this.mainToolBar = ((System.Windows.Controls.ToolBar)(target));
                    return;
                case 3:
                    this.richTB = ((System.Windows.Controls.RichTextBox)(target));
                    return;
                case 4:

#line 117 "..\..\..\Views\SaveLoadPrintRTB.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveRTBContent);

#line default
#line hidden
                    return;
                case 5:

#line 118 "..\..\..\Views\SaveLoadPrintRTB.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LoadRTBContent);

#line default
#line hidden
                    return;
                case 6:

#line 119 "..\..\..\Views\SaveLoadPrintRTB.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PrintRTBContent);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.RichTextBox rtf;
        internal System.Windows.Controls.Primitives.ToggleButton BoldButton;
        internal System.Windows.Controls.Primitives.ToggleButton ItalicButton;
    }
}
