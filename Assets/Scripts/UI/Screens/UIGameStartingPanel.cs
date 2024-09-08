using System;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIGameStartingPanel : UIBasePanel, IDisposable
{
    [SerializeField] private TextMeshProUGUI Textfield;

    private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

    public void SetMessage(string message)
    {
        Textfield.text = message;
        RectTransform.DOPunchScale(new Vector3(1.05f, 1.05f, 1f), .3f, 10, 0);
    }

    public override async Task Hide(int duration = 0)
    {
        await Task.Delay(duration);
        if (this == null || _cancellationToken.IsCancellationRequested) return;

        Destroy(gameObject);
    }

    public void Dispose()
    {
        _cancellationToken.Cancel();
    }
}