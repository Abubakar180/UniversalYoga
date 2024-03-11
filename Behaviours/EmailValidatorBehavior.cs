using CommunityToolkit.Maui.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace UniversalYoga.Behaviours
{
    public class EmailValidatorBehavior : Behavior<Entry>
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        public static readonly BindableProperty IsValidProperty =
        BindableProperty.Create(
            nameof(IsValid), 
            typeof(bool), 
            typeof(EmailValidatorBehavior), 
            false, 
            BindingMode.TwoWay);

        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }
        //private static void IsValidChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    var button = ((EmailValidationBehavior)bindable);
        //    if (button != null)
        //        button.IsValid = (bool)newValue;
        //}
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = false;
            IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            ((Entry)sender).TextColor = IsValid ? Color.FromHex("#000000") : Color.FromHex("#FF0000");
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
