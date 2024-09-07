using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UIGameStartingPanel : UIBasePanel
{
    [SerializeField] private TextMeshProUGUI Textfield;
    
    public void SetMessage(string message)
    {
        Textfield.text = message;
    }

    public override async Task Hide(int duration = 0)
    {
        await Task.Delay(duration);
        
        Destroy(gameObject);
    }
}