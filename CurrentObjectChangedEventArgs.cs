using Godot;

namespace SignalLeak;

public partial class CurrentObjectChangedEventArgs : RefCounted
{
    public string Reason { get; set; }
}