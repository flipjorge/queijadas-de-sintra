using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    [SerializeField] private Transform PanelsContainer;
    [SerializeField] private Transform GUIContainer;
    [SerializeField] private PrefabCollectionSO PrefabsCollection;

    public T InstantiatePanel<T>() where T : UIBasePanel
    {
        var prefab = PrefabsCollection.Get<T>();
        return Instantiate<T>(prefab, PanelsContainer);
    }

    public T InstantiateGUI<T>() where T : UIBasePanel
    {
        var prefab = PrefabsCollection.Get<T>();
        return Instantiate<T>(prefab, GUIContainer);
    }
}