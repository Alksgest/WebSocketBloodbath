using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class GameState
{
    public List<string> ConnectionIds { get; set; }
    public List<Player> Players { get; set; }
}

[Serializable]
public class Player
{
    public string Id { get; set; }
    public Vector3 Position { get; set; }
}