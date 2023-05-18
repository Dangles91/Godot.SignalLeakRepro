using Godot;

namespace SignalLeak;

public partial class CurrentObjectChangedEventArgs : GodotObject
{
    public string Reason { get; set; }
}