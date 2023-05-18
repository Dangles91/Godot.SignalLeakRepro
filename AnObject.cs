using Godot;

namespace SignalLeak;

public partial class AnObject : GodotObject
{
    [Signal]
    public delegate void NameChangedEventHandler(CurrentObjectChangedEventArgs args);

    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; EmitSignal(AnObject.SignalName.NameChanged, new CurrentObjectChangedEventArgs{Reason = "Because"}); }
    }
}