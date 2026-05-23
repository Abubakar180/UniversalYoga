using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalYoga.Controls
{
    /*This class is inheriting the BoxView. This created for loading items*/
    public class SkeletonView: BoxView
    {
        public SkeletonView()
        {
            /*In the contructor this function repeats after 1.5 seconds.*/
            Device.StartTimer(TimeSpan.FromSeconds(1.5), () =>
            {
                /*BoxView is opacity is set to 0.5 and 1 again after 750 mili seconds.*/
                this.FadeTo(0.5, 750, Easing.CubicInOut).ContinueWith((x) =>
                {
                    this.FadeTo(1, 750, Easing.CubicInOut);
                });

                return true;
            });
        }
    }
}
