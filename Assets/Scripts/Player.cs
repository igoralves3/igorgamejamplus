using System.Runtime.InteropServices;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class Player : MonoBehaviour
{

    public GameObject playerTroop;

    private PlayerInputActions inputActions;

    public GameObject navMesh;

    public Canvas canvas;
    public Text text;

    public Camera camera;

    public static int coins = 0;

    public int frames = 0;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        camera = Camera.main;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coins < 8) {
            frames += 1;
            if (frames >= 600) {
                frames = 0;
                coins += 1;
            }
        }
        UpdateUI();
    }

    void SetTargetPosition()
    {

    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.SpawnOffensive.ClickLeftMouseButton.performed += OnOffense;
        inputActions.SpawnDeffensive.ClickRightMouseButton.performed += OnDefense;
    }

    private void OnDisable()
    {
        inputActions.SpawnOffensive.ClickLeftMouseButton.performed -= OnOffense;
        inputActions.SpawnDeffensive.ClickRightMouseButton.performed -= OnDefense;
        inputActions.Disable();
    }

    private void OnOffense(InputAction.CallbackContext context)
    {
        int layerIndex = LayerMask.NameToLayer("NavMesh");

        Vector3 mousePos = Mouse.current.position.ReadValue();//Input.mousePosition;
        var worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        var pt = Instantiate(playerTroop, worldPosition, Quaternion.identity);

        
        pt.layer = layerIndex;

        pt.GetComponent<PlayerTroop>().offensive = true;
    }

    private void OnDefense(InputAction.CallbackContext context)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        var worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        var pt = Instantiate(playerTroop, worldPosition, Quaternion.identity);

        pt.GetComponent<PlayerTroop>().offensive = false;
    }

    void UpdateUI()
    {
        text.text = ("Coins : " + coins.ToString());
    }

}


