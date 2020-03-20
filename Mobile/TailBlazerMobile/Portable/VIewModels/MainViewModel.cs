using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Text;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace TailBlazerMobile.Portable.VIewModels
{
    public class MainViewModel : ViewModelBase
    {
        [Reactive] public string FirstName { get; set; }
        [Reactive] public string LastName { get; set; }

        public string FormattedName { [ObservableAsProperty] get; }

        public MainViewModel(
            IScheduler mainThreadScheduler = null,
            IScheduler taskPoolScheduler = null,
            IScreen hostScreen = null
            ) : base("Main View", mainThreadScheduler, taskPoolScheduler, hostScreen)
        {
            this.WhenAnyValue(
                x => x.FirstName,
                x => x.LastName,
                (first, last) => $"{last}, {first}")
                .ToPropertyEx(this, x => x.FormattedName);
            ;
        }
    }
}
