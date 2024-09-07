using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    [SerializeField] private Transform PopupsContainer;
    [SerializeField] private PrefabCollectionSO PrefabsCollection;

    public T InstantiatePanel<T>() where T : UIBasePanel
    {
        var prefab = PrefabsCollection.Get<T>();
        return Instantiate<T>(prefab, PopupsContainer);
    }
}