using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState
{
    public List<string> connectionIds;
    public List<Player> players;
}

[Serializable]
public class Player
{
    public string Id { get; set; }
    public Vector3 Position { get; set; }
}