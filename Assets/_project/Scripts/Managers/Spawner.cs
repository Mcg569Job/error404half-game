
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Pool[] pools;

    [Header("Spawner Settings")]
    [SerializeField] [Range(1, 20)] private float _checkTime = 10;
    [SerializeField] [Range(1, 20)] private float _spawnArea;
    private void Start()
    {
        for (int i = 0; i < pools.Length; i++)
            pools[i].CreatePool(transform);
        StartCoroutine(CheckObjects());
    }

    private void Spawn(int index)
    {
        for (int i = 0; i < pools[index]._ObjectCount; i++)
        {
            Vector3 position = transform.position + new Vector3(Random.Range(-_spawnArea, _spawnArea), 0, Random.Range(-_spawnArea, _spawnArea));
            GameObject g = pools[index].GetObject();

            g.transform.position = position;
        }

    }

    private IEnumerator CheckObjects()
    {
        while (true)
        {
            for (int a = 0; a < pools.Length; a++)
            {
                int count = 0;
                for (int i = 0; i < pools[a]._ObjectCount; i++)
                {
                    if (pools[a]._Objects[i].activeSelf)
                        count++;
                }
                if (count <= 0)
                    Spawn(a);
            }

            yield return new WaitForSeconds(_checkTime);

            if (GameManager.Instance.gameOver) break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _spawnArea);
    }
}
