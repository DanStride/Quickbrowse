#pragma checksum "..\..\..\View\VSAWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C160A4D9AF3EE6A8B3017F61BFE38C42DD1CC1C7BD07E9FFEE04CAB9EFFF84BF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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
using VSA_Viewer.ViewModel;
using VSA_Viewer.ViewModel.ValueConverters;


namespace VSA_Viewer {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 392 "..\..\..\View\VSAWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem closeApp;
        
        #line default
        #line hidden
        
        
        #line 430 "..\..\..\View\VSAWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button restoreButton;
        
        #line default
        #line hidden
        
        
        #line 436 "..\..\..\View\VSAWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scrollViewer;
        
        #line default
        #line hidden
        
        
        #line 443 "..\..\..\View\VSAWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView filesListView;
        
        #line default
        #line hidden
        
        
        #line 477 "..\..\..\View\VSAWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button saveButton;
        
        #line default
        #line hidden
        
        
        #line 484 "..\..\..\View\VSAWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button nextFolderButton;
        
        #line default
        #line hidden
        
        
        #line 491 "..\..\..\View\VSAWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button previousFolderButton;
        
        #line default
        #line hidden
        
        
        #line 501 "..\..\..\View\VSAWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_display;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/VSA Viewer;component/view/vsawindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\VSAWindow.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.closeApp = ((System.Windows.Controls.MenuItem)(target));
            
            #line 397 "..\..\..\View\VSAWindow.xaml"
            this.closeApp.Click += new System.Windows.RoutedEventHandler(this.closeApp_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.restoreButton = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.scrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            
            #line 442 "..\..\..\View\VSAWindow.xaml"
            this.scrollViewer.PreviewMouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.scrollViewer_PreviewMouseWheel);
            
            #line default
            #line hidden
            return;
            case 4:
            this.filesListView = ((System.Windows.Controls.ListView)(target));
            
            #line 447 "..\..\..\View\VSAWindow.xaml"
            this.filesListView.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.filesListView_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.saveButton = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.nextFolderButton = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.previousFolderButton = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.img_display = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

