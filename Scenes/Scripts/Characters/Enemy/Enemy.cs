using Godot;
using System;

public partial class Enemy : Character
{
    [Export(PropertyHint.Range, "0,20,0.1")] public float speed = 5;
}
