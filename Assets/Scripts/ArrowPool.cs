using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    public static ArrowPool SharedInstance;

    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private int poolSize = 20;
    private Queue<GameObject> pooledArrows;

    private void Awake()
    {
        SharedInstance = this;
        pooledArrows = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(arrowPrefab);
            obj.SetActive(false);
            pooledArrows.Enqueue(obj);
        }
    }

    public GameObject GetArrow()
    {
        if (pooledArrows.Count > 0)
        {
            var arrow = pooledArrows.Dequeue();
            arrow.SetActive(true);
            return arrow;
        }

        var obj = Instantiate(arrowPrefab);
        obj.SetActive(true);
        return obj;
    }

    public void ReturnArrowToPool(GameObject arrow)
    {
        var rb = arrow.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        arrow.SetActive(false);
        pooledArrows.Enqueue(arrow);
    }
   
}
