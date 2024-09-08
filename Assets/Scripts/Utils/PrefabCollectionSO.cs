using UnityEngine;

[CreateAssetMenu(menuName = "Prefab Collection")]
public class PrefabCollectionSO : ScriptableObject
{
    public GameObject[] Prefabs;

    public T Get<T>() where T : MonoBehaviour
    {
        foreach (var prefab in Prefabs)
        {
            var component = prefab.GetComponent<T>();
            if (component != null) return component;
        }

        return null;
    }
}