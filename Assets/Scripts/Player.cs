using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
public class Player : MonoBehaviour
{

    public GameObject playerTroop;

    private PlayerInputActions inputActions;

    public GameObject navMesh;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

        Vector3 mousePos = Mouse.current.position.ReadValue();
        var pt = Instantiate(playerTroop, new Vector3(mousePos.x, mousePos.y, navMesh.transform.position.z), Quaternion.identity);

        pt.layer = layerIndex;

        pt.GetComponent<PlayerTroop>().offensive = true;
    }

    private void OnDefense(InputAction.CallbackContext context)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        var pt = Instantiate(playerTroop, new Vector3(mousePos.x, mousePos.y, navMesh.transform.position.z), Quaternion.identity);

        pt.GetComponent<PlayerTroop>().offensive = false;
    }
}
