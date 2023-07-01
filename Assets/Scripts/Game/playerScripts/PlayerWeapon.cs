using System.Collections;
using Mirror;
using UnityEngine;

namespace Game.playerScripts
{
    public class PlayerWeapon : NetworkBehaviour
    {
        [SerializeField] private GameObject boll;
        [SerializeField] private float power;
        [SerializeField] private float lifeTime;
        private Vector2 direction;

        public void Shoot(Vector2 direction)
        {
            if(isLocalPlayer is false) return;
            this.direction = direction;
            CmdSpawnBoll();
        }

        [Command]
        private void CmdSpawnBoll()
        {
            var bollPrefab = Instantiate(boll, transform.position , transform.rotation);
            NetworkServer.Spawn(bollPrefab);
            
            if (bollPrefab.TryGetComponent<Boll>(out var isBoll))
            {
                isBoll.Kick(direction, power,gameObject);
            }
            
            StartCoroutine(Destroy(bollPrefab));
        }

        private IEnumerator Destroy(GameObject bollPrefab)
        {
            yield return new WaitForSeconds(lifeTime);
            if(isServer)
                NetworkServer.Destroy(bollPrefab);
        }
    }
}