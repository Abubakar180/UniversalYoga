using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalYoga.Services.Interface;
using IToast = UniversalYoga.Services.Interface.IToast;

namespace UniversalYoga.Services.Implementation
{
    public class ToastMessage : IToast
    {
        /*CommunityToolkit.Maui plugin must be installed from nuget package*/
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public async Task Show(string message)
        {
            string text = message;
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }
}
