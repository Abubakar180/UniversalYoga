namespace UniversalYoga.Controls;

public partial class CustomEntry : ContentView
{
    /*Bindable Properties are created here for Custom Entry. So the entry's property can be changed according to desire*/
    public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
          nameof(TitleText),
          typeof(string),
          typeof(CustomEntry),
          defaultValue: string.Empty,
          defaultBindingMode: BindingMode.TwoWay,
          propertyChanged: TitleTextPropertyChanged);

    private static void TitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CustomEntry)bindable;
        control.Title.Text = newValue?.ToString();
    }

    public string TitleText
    {
        get
        {
            return base.GetValue(TitleTextProperty)?.ToString();
        }

        set
        {
            base.SetValue(TitleTextProperty, value); OnPropertyChanged();
        }
    }
    public static readonly BindableProperty PlacHolderProperty = BindableProperty.Create(
       nameof(Placeholder),
       typeof(string),
       typeof(CustomEntry),
       defaultValue: string.Empty,
       defaultBindingMode: BindingMode.TwoWay,
       propertyChanged: PlacHolderPropertyChanged);

    private static void PlacHolderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CustomEntry)bindable;
        control.PlaceHolder.Text = newValue?.ToString();
    }

    public string Placeholder
    {
        get
        {
            return base.GetValue(PlacHolderProperty)?.ToString();
        }

        set
        {
            base.SetValue(PlacHolderProperty, value); OnPropertyChanged();
        }
    }


    public static readonly BindableProperty EntryTextProperty = BindableProperty.Create(
                                               propertyName: nameof(EntryText),
                                               returnType: typeof(string),
                                               declaringType: typeof(CustomEntry),
                                               defaultValue: null,
                                               defaultBindingMode: BindingMode.TwoWay);
    public string EntryText
    {
        get { return GetValue(EntryTextProperty)?.ToString(); }
        set { SetValue(EntryTextProperty, value); }
    }

    public static BindableProperty KeyboardProperty = BindableProperty.Create(
        nameof(Keyboard),
        typeof(Keyboard),
        typeof(CustomEntry),
        defaultBindingMode: BindingMode.TwoWay);

    public Keyboard Keyboard
    {
        get => (Keyboard)GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }

    public static BindableProperty IsPasswordProperty = BindableProperty.Create(
        nameof(IsPassword),
        typeof(bool),
        typeof(CustomEntry),
        defaultBindingMode: BindingMode.TwoWay);

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }
    public static BindableProperty EntryBehaviorProperty = BindableProperty.Create(
        nameof(EntryBehavior),
        typeof(Behavior),
        typeof(CustomEntry),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: EntryBehaviorPropertyChanged);

    public Behavior EntryBehavior
    {
        get => (Behavior)GetValue(EntryBehaviorProperty);
        set => SetValue(EntryBehaviorProperty, value);
    }
    private static void EntryBehaviorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CustomEntry)bindable;
        control.Entry.Behaviors.Add((Behavior)(newValue));
    }
    public static BindableProperty EntryTextColorProperty = BindableProperty.Create(
        nameof(EntryTextColor),
        typeof(Color),
        typeof(CustomEntry),
        defaultBindingMode: BindingMode.TwoWay);

    public Color EntryTextColor
    {
        get => (Color)GetValue(EntryTextColorProperty);
        set => SetValue(EntryTextColorProperty, value);
    }

    public static BindableProperty IsNoteVisibleProperty = BindableProperty.Create(
        nameof(IsNoteVisible),
        typeof(bool),
        typeof(CustomEntry),
        defaultBindingMode: BindingMode.TwoWay);

    public bool IsNoteVisible
    {
        get => (bool)GetValue(IsNoteVisibleProperty);
        set => SetValue(IsNoteVisibleProperty, value);
    }

    public static readonly BindableProperty NoteTextProperty = BindableProperty.Create(
                                               propertyName: nameof(NoteText),
                                               returnType: typeof(string),
                                               declaringType: typeof(CustomEntry),
                                               defaultValue: null,
                                               defaultBindingMode: BindingMode.TwoWay);
    public string NoteText
    {
        get { return GetValue(NoteTextProperty)?.ToString(); }
        set { SetValue(NoteTextProperty, value); }
    }

    public CustomEntry()
	{
		InitializeComponent();
        this.Entry.SetBinding(Entry.TextProperty, new Binding(nameof(EntryText), source: this));
        this.Entry.SetBinding(Entry.KeyboardProperty, new Binding(nameof(Keyboard), source: this));
        this.Entry.SetBinding(Entry.IsPasswordProperty, new Binding(nameof(IsPassword), source: this));
        this.Entry.SetBinding(Entry.TextColorProperty, new Binding(nameof(EntryTextColor), source: this));
    }
    /*When Entry is Focused. It animates the Title of Entry upwards by changing its color.*/
    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        titleFrame.TranslateTo(0, -25, 200, Easing.CubicInOut);
        Title.TextColor = Color.FromHex("#E70B89");
        PlaceHolder.IsVisible = false;
    }
    /*When Entry is Focused. It animates back the Title of Entry where it was before.*/
    private void Entry_Unfocused(object sender, FocusEventArgs e)
    {
        if (Entry.Text == "" || Entry.Text == null || Entry.Text == string.Empty)
        {
            titleFrame.TranslateTo(0, 0, 200, Easing.CubicInOut);
            Title.TextColor = Color.FromHex("#C5C6C7");
            PlaceHolder.IsVisible = true;
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        if (Entry.Text == "" || Entry.Text == null || Entry.Text == string.Empty)
        {
            Entry.Focus();
        }
    }

}