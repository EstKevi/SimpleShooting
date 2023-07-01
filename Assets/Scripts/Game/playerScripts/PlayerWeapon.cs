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

        public void Shoot(Vector2 direction) => CmdSpawnBoll(direction);

        [Command]
        private void CmdSpawnBoll(Vector2 direction)
        {
            // var bollPrefab = Instantiate(boll, transform.position, transform.rotation);
            
            var bollPrefab = Instantiate(boll, transform.position , transform.rotation);
            if (bollPrefab.TryGetComponent<Boll>(out var isBoll))
            {
                isBoll.Kick(direction, power,gameObject);
            }
            NetworkServer.Spawn(bollPrefab);
            StartCoroutine(Destroy(bollPrefab));
        }

        private IEnumerator Destroy(GameObject bollPrefab)
        {
            yield return new WaitForSeconds(lifeTime);
            NetworkServer.Destroy(bollPrefab);
        }
    }
}