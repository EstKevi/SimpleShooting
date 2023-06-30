using UnityEngine;

namespace Game.playerScripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMove : MonoBehaviour
    {
        private new Rigidbody2D rigidbody;

        private void Awake() => rigidbody = GetComponent<Rigidbody2D>();

        public void Move(Vector2 direction) => rigidbody.AddForce(direction);
    }
}