using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using TailBlazerMobile.Portable.VIewModels;
using Xamarin.Forms;

namespace TailBlazerMobile.Portable
{
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppBootstrapper"/> class.
        /// </summary>
        public AppBootstrapper()
        {
            Router = new RoutingState();
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));
            Locator.CurrentMutable.Register(() => new MainPage(), typeof(IViewFor<MainViewModel>));

            Router.NavigateAndReset
                .Execute(new MainViewModel())
                .Subscribe();
        }
        public RoutingState Router { get; protected set; }

        /// <summary>
        /// Creates the first main page used within the application.
        /// </summary>
        /// <returns>The page generated.</returns>
        public static Page CreateMainPage()
        {
            // NB: This returns the opening page that the platform-specific
            // boilerplate code will look for. It will know to find us because
            // we've registered our AppBootstrappScreen.
            return new RoutedViewHost();
        }
    }
}
