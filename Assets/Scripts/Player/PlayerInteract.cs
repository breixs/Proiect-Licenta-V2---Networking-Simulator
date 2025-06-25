using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Transform holdPos;
    public Material transparent;
    public Material activ;
    private Camera cam;
    [SerializeField]
    private float distance = 4f;
    [SerializeField]
    private LayerMask mask;

    private PlayerUI playerUI;

    private InputManager inputManager;

    private PickUpScript pickUpScript;

    private TaskScript taskScript;

    private bool isHolding = false;
    private bool isHoldingRope = false;
    private bool isHoldingConsole = false;
    private string holdTag;

    GameObject[] placeHoldersSW;
    GameObject[] placeHoldersR;
    GameObject[] placeHolders;

    public AudioSource audioSourceDoor;
    public AudioClip audioClipDoor;
    public AudioSource audioSourceConnect;
    public AudioClip audioClipConnect;
    public AudioSource backgroundSound;

    private void Awake()
    {
        audioSourceDoor.volume = PlayerPrefs.GetFloat("volume");
        audioSourceConnect.volume = PlayerPrefs.GetFloat("volume");
        backgroundSound.volume = PlayerPrefs.GetFloat("volume");
        audioSourceDoor.PlayOneShot(audioClipDoor);
    }

    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
        pickUpScript = gameObject.GetComponent<PickUpScript>();
        taskScript = gameObject.GetComponent<TaskScript>();

        placeHoldersSW = GameObject.FindGameObjectsWithTag("PlaceHolderSW");
        placeHoldersR = GameObject.FindGameObjectsWithTag("PlaceHolderR");
        placeHolders = new GameObject[placeHoldersSW.Length + placeHoldersR.Length];
        placeHoldersSW.CopyTo(placeHolders, 0);
        placeHoldersR.CopyTo(placeHolders, placeHoldersSW.Length);
    }

    void Update()
    {
        if (playerUI != null)
            playerUI.DeactivateText();

        foreach (var go in placeHolders)
        {
            go.GetComponent<MeshRenderer>().material = transparent;
        }

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;

        if (!PlayerUI.gameOver && isHolding && inputManager.onFoot.PickUp.triggered)
        {
            pickUpScript.StopClipping();
            pickUpScript.DropObject(false, null);
            isHolding = false;
            isHoldingRope = false;
            isHoldingConsole = false;
        }

        if (!PlayerUI.gameOver && Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(hitInfo.collider.GetComponent<Interactable>().promoptMessage);
                //interactionare cu obiect (tasta E)
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();

                    if (taskScript.completed == true && hitInfo.transform.gameObject.tag == "Exit")
                        interactable.ConditionalInterract();
                }
                //ridicarea unui obiect (tasta F)
                if (inputManager.onFoot.PickUp.triggered)
                {
                    if (isHolding == false)
                    {
                        if (hitInfo.transform.gameObject.tag == "CanPickUp" || hitInfo.transform.gameObject.tag == "Switch" || hitInfo.transform.gameObject.tag == "Router")
                        {
                            holdTag = hitInfo.transform.gameObject.tag;
                            Debug.Log(holdTag);
                            isHolding = true;
                            pickUpScript.PickUpObject(hitInfo.transform.gameObject);
                            pickUpScript.MoveObject();
                        }
                        if (hitInfo.transform.gameObject.tag == "PickUpRope")
                        {
                            isHolding = true;
                            isHoldingRope = true;
                            pickUpScript.PickUpObject(hitInfo.transform.gameObject);
                            pickUpScript.MoveObject();
                        }
                        if (hitInfo.transform.gameObject.tag == "PickUpConsole")
                        {
                            isHolding = true;
                            isHoldingConsole = true;
                            pickUpScript.PickUpObject(hitInfo.transform.gameObject);
                            pickUpScript.MoveObject();
                        }

                    }
                    else
                    {
                        pickUpScript.StopClipping();
                        pickUpScript.DropObject(false, null);
                        isHolding = false;
                        isHoldingRope = false;
                        isHoldingConsole = false;
                    }

                }
            }
            if (hitInfo.collider.CompareTag("PlaceHolderSW") || hitInfo.collider.CompareTag("PlaceHolderR"))
            {
                if (isHolding && !isHoldingRope && !isHoldingConsole && holdTag == "Switch")
                {
                    if (hitInfo.collider.CompareTag("PlaceHolderSW"))
                    {
                        hitInfo.collider.GetComponent<MeshRenderer>().material = activ;

                        if (inputManager.onFoot.Interact.triggered)
                        {
                            pickUpScript.SetObject(hitInfo.collider.transform.position);
                            isHolding = false;
                            
                        }
                    }
                }
                else if (isHolding && !isHoldingRope && !isHoldingConsole && holdTag == "Router")
                {
                    if (hitInfo.collider.CompareTag("PlaceHolderR"))
                    {
                        hitInfo.collider.GetComponent<MeshRenderer>().material = activ;

                        if (inputManager.onFoot.Interact.triggered)
                        {
                            pickUpScript.SetObject(hitInfo.collider.transform.position);
                            isHolding = false;
                            
                        }
                    }
                }

            }

            if ((isHoldingRope || isHoldingConsole) && (hitInfo.collider.CompareTag("SwitchPort") || hitInfo.collider.CompareTag("ConsolePort") || hitInfo.collider.CompareTag("LaptopPort") || hitInfo.collider.CompareTag("PanelPort")))
            {
                if (isHoldingRope && inputManager.onFoot.Interact.triggered && (hitInfo.collider.CompareTag("SwitchPort") || hitInfo.collider.CompareTag("PanelPort")))
                {
                    audioSourceConnect.PlayOneShot(audioClipConnect);
                    pickUpScript.ConnectObject(hitInfo.collider.transform.position, hitInfo.collider.transform.parent.gameObject, new Vector3(0, -90, 0));
                    isHoldingRope = false;
                    isHolding = false;
                }
                else if (isHoldingConsole && inputManager.onFoot.Interact.triggered && hitInfo.collider.CompareTag("LaptopPort"))
                {
                    audioSourceConnect.PlayOneShot(audioClipConnect);
                    pickUpScript.ConnectObject(hitInfo.collider.transform.position, hitInfo.collider.transform.parent.gameObject, new Vector3(0, 45, 0));
                    isHoldingConsole = false;
                    isHolding = false;
                }
                else if (isHoldingConsole && inputManager.onFoot.Interact.triggered && (hitInfo.collider.CompareTag("ConsolePort")))
                {
                    audioSourceConnect.PlayOneShot(audioClipConnect);
                    pickUpScript.ConnectObject(hitInfo.collider.transform.position, hitInfo.collider.transform.parent.gameObject, new Vector3(0, -90, 0));
                    isHoldingConsole = false;
                    isHolding = false;
                }
            }
        }
    }
}
