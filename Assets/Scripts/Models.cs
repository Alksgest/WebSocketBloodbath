using System;
using System.Collections.Generic;
using UnityEngine;

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
    public Position Position { get; set; }
}

public class Position
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public static implicit operator Position(Vector3 vec)
    {
        return new Position
        {
            X = vec.x,
            Y = vec.y,
            Z = vec.z
        };
    }
}