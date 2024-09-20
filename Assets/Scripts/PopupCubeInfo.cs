using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupCubeInfo : MonoBehaviour
{

    [SerializeField] private SideInfo top; 
    [SerializeField] private SideInfo bottom; 
    [SerializeField] private SideInfo left; 
    [SerializeField] private SideInfo right; 
    [SerializeField] private SideInfo front; 
    [SerializeField] private SideInfo back;
    [SerializeField] private Button done;

    private void Start()
    {
        done.onClick.AddListener(HidePopup);
    }
    public void SetData(HandData data)
    {

        top.SetData(data.Pictures.TakeTexture(data.Top.Type), data.Top.TakeString());
        bottom.SetData(data.Pictures.TakeTexture(data.Bottom.Type), data.Bottom.TakeString());
        left.SetData(data.Pictures.TakeTexture(data.Left.Type), data.Left.TakeString());
        right.SetData(data.Pictures.TakeTexture(data.Right.Type), data.Right.TakeString());
        front.SetData(data.Pictures.TakeTexture(data.Front.Type), data.Front.TakeString());
        back.SetData(data.Pictures.TakeTexture(data.Backward.Type), data.Backward.TakeString());

        ShowPopup();


    }

    private void ShowPopup()
    {
        gameObject.SetActive(true);
    }
    private void HidePopup()
    {
        gameObject.SetActive(false);
    }
}
