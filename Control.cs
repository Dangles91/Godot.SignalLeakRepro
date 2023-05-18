using Godot;
using System;
using System.Collections.Generic;

namespace SignalLeak;

public partial class Control : Godot.Control
{
    [Signal]
    public delegate void CurrentObjectChangedEventHandler();

    public List<AnObject> ListOfObjects { get; set; } = new();

    private AnObject _currentObject;
    public AnObject CurrentObject
    {
        get => _currentObject;
        set
        {
            _currentObject = value;
            EmitSignal(Control.SignalName.CurrentObjectChanged,
                new CurrentObjectChangedEventArgs
                {
                    Reason = "Because"
                }
            );
        }
    }

	public Control()
	{
		
	}

    // Called when the node enters the scene tree for the first time.
    private ItemList GetItemList() => GetNode<ItemList>("ItemList");
    private LineEdit GetLineEdit() => GetNode<LineEdit>("LineEdit");
	private Button GetButtion() => GetNode<Button>("Button");

    public override void _Ready()
    {
        for (int i = 0; i < 20; i++)
		{
			ListOfObjects.Add(new AnObject { Name = $"Name {i}"});
		}

        var list = GetItemList();
        foreach (var anObject in ListOfObjects)
        {
            list.AddItem(anObject.Name);
        }

        GetLineEdit().TextChanged += (v) =>
        {
            if (CurrentObject != null)
                CurrentObject.Name = v;
        };

        GetItemList().ItemSelected += (v) =>
        {
            var prevItem = CurrentObject;
            if (prevItem != null)
            {
                prevItem.NameChanged -= OnNameChanged;
            }


            CurrentObject = ListOfObjects[(int)v];
            CurrentObject.NameChanged += OnNameChanged;
        };

		GetButtion().Pressed += () => {
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GD.Print("Collecting the garbage.");
		};
    }

    public void OnNameChanged(CurrentObjectChangedEventArgs args)
    {
        GD.Print($"The reason for the name change was: {args.Reason}");
        GetItemList().SetItemText(ListOfObjects.IndexOf(CurrentObject), CurrentObject.Name);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }
}
