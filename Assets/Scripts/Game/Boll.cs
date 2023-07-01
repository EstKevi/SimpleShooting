using System;
using Game.playerScripts;
using Mirror;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Boll : NetworkBehaviour
    {
        private new Rigidbody2D rigidbody2D;
        [SerializeField] private GameObject parent;
        private void Awake() => rigidbody2D ??= GetComponent<Rigidbody2D>();

        public void Kick(Vector2 direction, float power, GameObject parentBoll = null)
        {
            parent = parentBoll;
            rigidbody2D.AddForce(direction * power);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Player>(out _))
            {
                if (other.gameObject == parent)
                {
                    return;
                }
                if(isServer)
                    NetworkServer.Destroy(gameObject);
            }
        }
    }
}