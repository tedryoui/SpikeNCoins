using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclesSpawner : MonoBehaviour
{
        public int circlesAmount;
        public GameObject circlePrefab;
        public Vector2 spawnBounds;
        public LayerMask interfereLayers;

        private float itemRadius;
        
        void Start()
        {
            itemRadius = circlePrefab.GetComponent<CircleCollider2D>().radius * circlePrefab.transform.localScale.x;
            
            SpawnSpikes();
        }
    
        private void SpawnSpikes()
        {
            Vector2 randomPosition = Vector2.zero;
            
            for (int i = 0; i < circlesAmount; i++)
            {
                while (true)
                {
                    randomPosition = new Vector2(Random.Range(-spawnBounds.x, spawnBounds.x),
                        Random.Range(-spawnBounds.y, spawnBounds.y));
    
                    Collider2D[] results = new Collider2D[1];
                    if (Physics2D.OverlapCircleNonAlloc(randomPosition, itemRadius, results, interfereLayers) == 0)
                        break;
                }
    
                Object.Instantiate(circlePrefab, randomPosition, Quaternion.identity, transform);
            }
        }
    
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(transform.position, spawnBounds * 2);
        }
}
