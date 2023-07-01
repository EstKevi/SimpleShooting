using Mirror;
using UnityEngine;

namespace Game.playerScripts
{
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
            playerColor = dataSaveColor.Color;
            playerSprite.color = dataSaveColor.Color;
        }

        private void Start()
        {
            if (isLocalPlayer)
            {
                SetPlayerColor(dataSaveColor.Color);
            }
            else
            {
                playerSprite.color = playerColor;
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
                playerSprite.color = playerColor;
            }
        }

        private void OnColorChange(Color oldColor, Color newColor)
        {
            if (isLocalPlayer)
            {
                return;
            }

            playerSprite.color = playerColor;
        }
    }
}