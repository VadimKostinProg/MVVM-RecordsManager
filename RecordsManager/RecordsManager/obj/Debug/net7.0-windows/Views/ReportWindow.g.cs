﻿#pragma checksum "..\..\..\..\Views\ReportWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6C1569E6B1AC798CF2AEF532202D1E59DDDEDA0C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using RecordsManager.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace RecordsManager.Views {
    
    
    /// <summary>
    /// ReportWindow
    /// </summary>
    public partial class ReportWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 2 "..\..\..\..\Views\ReportWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal RecordsManager.Views.ReportWindow Report_Window;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Views\ReportWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Calendar StartDate_Calendar;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Views\ReportWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Calendar EndDate_Calendar;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Views\ReportWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox StartDate_TextBox;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Views\ReportWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EndDate_TextBox;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\Views\ReportWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Incomes_TextBox;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\Views\ReportWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Expenses_TextBox;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\..\Views\ReportWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Profit_TextBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/RecordsManager;component/views/reportwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\ReportWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Report_Window = ((RecordsManager.Views.ReportWindow)(target));
            return;
            case 2:
            this.StartDate_Calendar = ((System.Windows.Controls.Calendar)(target));
            return;
            case 3:
            this.EndDate_Calendar = ((System.Windows.Controls.Calendar)(target));
            return;
            case 4:
            this.StartDate_TextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.EndDate_TextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.Incomes_TextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.Expenses_TextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.Profit_TextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

