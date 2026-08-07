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
        IsRunning = true;
        ChaosLevel = 0;
        ActiveModifiers = Array.Empty<string>();
    }

    public void ApplyChaos(string modifier)
    {
        ChaosLevel++;
        // TODO: ActiveModifiers = ActiveModifiers.Append(modifier).ToList();
    }

    public void UpdatePositions(Vector2 tugot, Vector2 player)
    {
        TugotPosition = tugot;
        PlayerPosition = player;
    }

    public void Stop()
    {
        IsRunning = false;
    }
}
