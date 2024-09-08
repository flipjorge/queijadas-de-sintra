using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIGameStartingPanel : UIBasePanel
{
    [SerializeField] private TextMeshProUGUI Textfield;

    public void SetMessage(string message)
    {
        Textfield.text = message;
        RectTransform.DOPunchScale(new Vector3(1.05f, 1.05f, 1f), .3f, 10, 0);
    }

    public override async Task Hide(int duration = 0)
    {
        await Task.Delay(duration);

        Destroy(gameObject);
    }
}