using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleDropped : MonoBehaviour
{
    [SerializeField] GameObject collectible;
    [SerializeField] GameObject enemy;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null && collectible != null)
        {
            collectible.transform.localScale = Vector3.Slerp(transform.localScale, new Vector3(1, 1, 1),
                                        0.3f);

            if (Vector3.Distance(collectible.transform.position, target) > 0.1f)
            {
                collectible.transform.position = Vector3.MoveTowards(collectible.transform.position, target, 0.1f);
            }
        }
        else if (collectible != null)
        {
            collectible.transform.localScale = enemy.transform.localScale * 0.5f; ;
            collectible.transform.position = new Vector3(enemy.transform.position.x,
                                                           enemy.transform.position.y + 2.5f,
                                                           enemy.transform.position.z);

            target = new Vector3(collectible.transform.position.x, collectible.transform.position.y - 1.5f, collectible.transform.position.z);
        }
    }
}
