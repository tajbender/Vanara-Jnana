using System.Collections.Generic;
using System.Numerics;

namespace Jnana.ViewModels;

public sealed class TugorGameState
{
    private bool _isRunning;
    private int _chaosLevel;
    private Vector2 _tugotPosition;
    private Vector2 _playerPosition;
    private IReadOnlyList<string> _activeModifiers;

    public bool IsRunning
    {
        get => _isRunning;
        private set => _isRunning = value;
    }

    public int ChaosLevel
    {
        get => _chaosLevel;
        private set => _chaosLevel = value;
    }

    public Vector2 TugotPosition
    {
        get => _tugotPosition;
        private set => _tugotPosition = value;
    }

    public Vector2 PlayerPosition
    {
        get => _playerPosition;
        private set => _playerPosition = value;
    }

    public IReadOnlyList<string> ActiveModifiers
    {
        get => _activeModifiers;
        private set => _activeModifiers = value;
    }

    public void Start()
    {
        IsRunning = true;
        ChaosLevel = 0;
        ActiveModifiers = [];
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