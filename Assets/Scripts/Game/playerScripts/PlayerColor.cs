using System;
using UnityEngine;
using Mirror;

public class PlayerColor : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnColorChange))]
    private Color playerColor;

    private DataSaveColor dataSaveColor;

    public SpriteRenderer playerSprite;

    private void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        dataSaveColor = FindObjectOfType<DataSaveColor>();
        if (isLocalPlayer)
        {
            playerColor = dataSaveColor.Color;
        }
    }

    private void Start()
    {
        if (isLocalPlayer)
        {

            SetPlayerColor(dataSaveColor.Color);
        }
        else
        {
            SetRendererColor(playerColor);
        }
    }

    public void SetPlayerColor(Color color)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        CmdSetColor(color);
    }

    [Command]
    private void CmdSetColor(Color color)
    {
        playerColor = color;

        RpcSetColor(color);
    }

    [ClientRpc]
    private void RpcSetColor(Color color)
    {
        if (!isLocalPlayer)
        {
            SetRendererColor(color);
        }
    }

    private void SetRendererColor(Color color)
    {
        playerSprite.color = color;
    }

    private void OnColorChange(Color oldColor, Color newColor)
    {
        if (isLocalPlayer)
        {
            return;
        }

        SetRendererColor(newColor);
    }
}