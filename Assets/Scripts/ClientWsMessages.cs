using System;

[Serializable]
public class ClientMessagePlayerEnter
{

    public string messageType = "CLIENT_MESSAGE_TYPE_PLAYER_ENTER";
    public Player player;

    public ClientMessagePlayerEnter(Player playerModel)
    {
        player = playerModel;
    }

}

[Serializable]
public class ClientMessagePlayerUpdate
{

    public string messageType = "CLIENT_MESSAGE_TYPE_PLAYER_UPDATE";
    public Player player;

    public ClientMessagePlayerUpdate(Player playerModel)
    {
        player = playerModel;
    }

}
