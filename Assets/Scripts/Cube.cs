using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private MeshRenderer top;
    [SerializeField] private TextMeshPro topPower;
    [SerializeField] private MeshRenderer bottom;
    [SerializeField] private TextMeshPro bottomPower;
    [SerializeField] private MeshRenderer left;
    [SerializeField] private TextMeshPro leftPower;
    [SerializeField] private MeshRenderer right;
    [SerializeField] private TextMeshPro rightPower;
    [SerializeField] private MeshRenderer front;
    [SerializeField] private TextMeshPro frontPower;
    [SerializeField] private MeshRenderer back;
    [SerializeField] private TextMeshPro backPower;
    [SerializeField] private float rollForce = 50f;
    [SerializeField] private float torqueForce = 15f;

    public Action<HandData> OnClickCubeAction;

    private Rigidbody rb;
    private Vector3 startPosition;
    HandData currentHandData;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit;

            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    OnCubeClick();
                }
            }
        }
    }

    public void SetSpritesInCube(HandData data)
    {
        currentHandData = data;
        top.material = data.Pictures.TakeTexture(data.Top.Type);
        topPower.text = data.Top.TakeString();
        bottom.material = data.Pictures.TakeTexture(data.Bottom.Type);
        bottomPower.text = data.Bottom.TakeString();
        left.material = data.Pictures.TakeTexture(data.Left.Type);
        leftPower.text = data.Left.TakeString();
        right.material = data.Pictures.TakeTexture(data.Right.Type);
        rightPower.text = data.Right.TakeString();
        front.material = data.Pictures.TakeTexture(data.Front.Type);
        frontPower.text = data.Front.TakeString();
        back.material = data.Pictures.TakeTexture(data.Backward.Type);
        backPower.text = data.Backward.TakeString();
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    public void RollDice()
    {

        transform.DOLocalMove(new Vector3(transform.position.x, transform.position.y, 0), 0.3f).OnComplete(Roll);

       
    }

    private void Roll()
    {
        rb.isKinematic = false;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Vector3 forceDirection = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;
        rb.AddForce(forceDirection * rollForce, ForceMode.Impulse);

        Vector3 torque = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.AddTorque(torque * torqueForce, ForceMode.Impulse);
    }
    
   
    public void ReturnToStartingPosition()
    {
        rb.isKinematic = true;

        transform.DOLocalMove(startPosition, 0.5f);

        Vector3 upVector = transform.up;
        Vector3 forwardVector = transform.forward;
        Vector3 rightVector = transform.right;

        

        if (upVector.y > 0.65)
        {
            currentHandData.CurrentSide = Sides.Top;
            transform.up = new Vector3(0, 1, 0);
        }
        if(upVector.y < -0.65)
        {
            currentHandData.CurrentSide = Sides.Bottom;
            transform.up = new Vector3(0, -1, 0);
        }
        if (forwardVector.y > 0.65)
        {
            currentHandData.CurrentSide = Sides.Front;
            transform.forward = new Vector3(0, 1, 0);
        }
        if (forwardVector.y < -0.65)
        {
            currentHandData.CurrentSide = Sides.Backward;
            transform.forward = new Vector3(0, -1, 0);
        }
        if (rightVector.y > 0.65)
        {
            currentHandData.CurrentSide = Sides.Right;
            transform.right = new Vector3(0, 1, 0);
        }
        if (rightVector.y < -0.65)
        {
            currentHandData.CurrentSide = Sides.Left;
            transform.right = new Vector3(0, -1, 0);
        }

    }

    public HandData TakeCubeHandData()
    {
        return currentHandData;
    }
    private void OnCubeClick()
    {
        OnClickCubeAction?.Invoke(currentHandData);
    }

}
