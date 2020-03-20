using System;
using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using TailBlazerMobile.Portable.VIewModels;

namespace TailBlazerMobile.Portable
{
    public partial class MainPage : ReactiveContentPage<MainViewModel>
    {
        public MainPage()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.FirstName, x => x.firstNameEntry.Text).DisposeWith(disposable);
                this.Bind(ViewModel, x => x.LastName, x => x.lastNameEntry.Text).DisposeWith(disposable);
                this.Bind(ViewModel, x => x.FormattedName, x => x.formattedNameLabel.Text).DisposeWith(disposable);
            });
        }
    }
}
