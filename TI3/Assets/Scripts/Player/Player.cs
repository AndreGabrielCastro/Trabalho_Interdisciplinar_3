using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [Header("Must be setted")]
    public Transform playerCameraPivotTransform;
    public Transform playerCameraFakeTransform;
    public Transform playerCameraTransform;

    public LayerMask unactiveEventObjectLayerMask;
    public LayerMask activeEventObjectLayerMask;
    public int activeEventObjectLayerMaskValue; // For complete explanation, go to Event Object

    [Header("Setted during playtime")]
    public PlayerIntegrity playerIntegrity;
    public PlayerMovement playerMovement;
    public bool isEventRunning;
    [HideInInspector] public bool isGameOver;

    [Header("Por enquanto")]
    public GameObject initialScreen;
    public void ResetPosition()
    { 
        this.transform.position = Vector3.zero;
        this.transform.rotation = Quaternion.identity;
    }
    public void IsEventRunning(bool result)
    { 
        isEventRunning = result;
        playerMovement.isEventRunning = result;

        if (result == true)
        {
            playerCameraTransform.position += Vector3.up * 15;
        }
        else
        {
            playerCameraTransform.localPosition = Vector3.zero;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
        #region ErrorTreatment
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Debug.LogWarning($" ----- There is more than one Player!!! ----- {this.transform.position} ----- {this.gameObject} -----");
            Destroy(this.gameObject);
            return;
        }
        #endregion

        playerIntegrity = this.GetComponent<PlayerIntegrity>();
        playerMovement = this.GetComponent<PlayerMovement>();
    }
    private void Start() { initialScreen.SetActive(true); }
    private void FixedUpdate()
    {
        if (isGameOver == true) { return; }
        if (isEventRunning == false) { return; }
        Vector3 halfExtentsVector = new Vector3(GridSystem.Instance.width * 0.5f, 0.5f, GridSystem.Instance.lenght * 0.5f);
        Collider[] eventObjectCollidersArray = Physics.OverlapBox(this.transform.position, halfExtentsVector, this.transform.rotation, unactiveEventObjectLayerMask.value);
        for (int i = 0; i < eventObjectCollidersArray.Length; i++)
        { eventObjectCollidersArray[i].GetComponent<EventObject>().SetActive(); }
    }
}