using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Mopups.Hosting;
using Syncfusion.Maui.Core.Hosting;
using System;
using UniversalYoga.Helpers;

namespace UniversalYoga
{
    public static class MauiProgram
    {
        /*Apps are bootstrapped using the .NET Generic Host it enables apps to be 
        initialized from a single location and provides the ability to configure
        fonts, services, and third-party libraries.*/
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
            /*Configuration of PLugins.*/
                .ConfigureMopups() 
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionCore()
            /*Configuration of Fonts.*/
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("materialdesignicons-webfont.ttf", "UserIcon");
                    fonts.AddFont("TextaProAlt-Black.otf", "Texta");
                    fonts.AddFont("TextaLightIt.ttf", "TextaLightIt");
                })
            /*Configuration of Platform Specific Functionality.*/
                .ConfigureLifecycleEvents(events =>
                {
                    /*This piece of code set the status bar color to transparent when the app is running.*/
#if ANDROID
                    events.AddAndroid(android => android.OnCreate((activity, bundle) => MakeStatusBarTranslucent(activity)));
                    static void MakeStatusBarTranslucent(Android.App.Activity activity)
                    {
                        activity.Window.SetFlags(Android.Views.WindowManagerFlags.LayoutNoLimits, Android.Views.WindowManagerFlags.LayoutNoLimits);
                        activity.Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
                        activity.Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
                    }
#endif
                });
            /*Configuration of Handlers To cutomize the native controls.
             The implementation is done in FormHandler class in Helpers Folder.*/
            FormHandler.RemoveBorders();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
