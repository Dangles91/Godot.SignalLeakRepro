# Godot.SignalLeakRepro

Emitting events that send args as GodotObject or RefCounted object creates orphansed objects that are not garbage collected

Explicitly calling RefCounted.Unreference in the subscribed method handler appears to resolve the behaviour
