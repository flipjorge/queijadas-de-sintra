using System.Threading.Tasks;
using UnityEngine;

public class UIBasePanel : MonoBehaviour
{
    public virtual Task Show(int duration = 0)
    {
        return Task.Delay(duration);
    }

    public virtual Task Hide(int duration = 0)
    {
        return Task.Delay(duration);
    }
}