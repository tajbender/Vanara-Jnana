using System;
using System.Collections.Generic;
using System.Numerics;

namespace Jnana.ViewModels;

public sealed class TugorGameState
{
    public bool IsRunning { get; private set; }
    public int ChaosLevel { get; private set; }
    public Vector2 TugotPosition { get; private set; }
    public Vector2 PlayerPosition { get; private set; }
    public IReadOnlyList<string> ActiveModifiers { get; private set; }

    public void Start()
    {
        this.IsRunning = true;
        this.ChaosLevel = 0;
        this.ActiveModifiers = Array.Empty<string>();
    }

    public void ApplyChaos(string modifier)
    {
        this.ChaosLevel++;
        // TODO: ActiveModifiers = ActiveModifiers.Append(modifier).ToList();
    }

    public void UpdatePositions(Vector2 tugot, Vector2 player)
    {
        this.TugotPosition = tugot;
        this.PlayerPosition = player;
    }

    public void Stop()
    {
        this.IsRunning = false;
    }
}
