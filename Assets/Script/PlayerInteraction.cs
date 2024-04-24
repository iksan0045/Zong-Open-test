using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Zong
{
    public class PlayerInteraction : MonoBehaviour
    {
        private Camera cam;

        [SerializeField]
        private PlayerController playerController;
        private GameObject interactableObj;

        [SerializeField]
        private Transform containerTransform;

        [SerializeField]
        private Transform targetPlace; 

        [SerializeField]
        private PickUpObj pickedUpObject; 

        [SerializeField]
        private GameObject dropTextObj;

        [SerializeField]
        private TextMeshProUGUI interactText;

        void Start()
        {
            cam = GetComponent<Camera>();
            interactText.text = "";
        }

        void Update()
        {
            CheckInteractable();

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (pickedUpObject != null)
                {
                    DropObject();
                }
                else
                {
                    InteractWithObject();
                }
            }
            else if (Input.GetKeyDown(KeyCode.F) && pickedUpObject != null)
            {
                if(CheckDestination())
                {
                    PlaceObject();
                }
                
            }
        }

        void CheckInteractable()
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out hit, 2f))
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    interactText.text = "[E] Pick Up";
                    
                    interactableObj = hit.collider.gameObject;
                }
                else if(hit.collider.CompareTag("Destination"))
                {
                    interactText.text = "[F] Place Object";
                }
                else
                {
                    interactText.text = "";
                }
            }
            else
            {
                interactText.text = "";
            }

        }

        private bool CheckDestination()
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out hit, 2f))
            {
                if (hit.collider.CompareTag("Destination"))
                {
                    interactText.text = "[F] Place Object";
                    targetPlace = hit.collider.gameObject.transform;
                    return true;
                }
            }

            return false;
        }

        void InteractWithObject()
        {
            if (interactableObj != null)
            {
                IInteractable interactable = interactableObj.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    interactable.Interact();
                    pickedUpObject = interactableObj.GetComponent<PickUpObj>();
                    dropTextObj.SetActive(true);
                    if (pickedUpObject != null)
                    {
                        GameManager.Instance.ShowMainUI();
                        pickedUpObject.PickUp(containerTransform);
                        GameManager.Instance.AddItemToInstrument(interactableObj.name);

                    }
                    Debug.Log("Interacted with: " + interactableObj.name);
                }
                else
                {
                    Debug.Log("InteractableObject component not found");
                }
            }
            else
            {
                Debug.Log("InteractableObject is null");
            }
        }

        void DropObject()
        {
            
            if (pickedUpObject != null)
            {
                pickedUpObject.Drop();
                pickedUpObject = null;
                dropTextObj.SetActive(false);
            }
        }

        void PlaceObject()
        {
            if (pickedUpObject != null)
            {
                pickedUpObject.PlaceObject(targetPlace);
                pickedUpObject = null;
                dropTextObj.SetActive(false);
                string coffinType = targetPlace.GetComponent<DestinationType>().boxType;
                if(coffinType == "C")
                {
                    playerController.BackToOrigin();
                    
                    Debug.Log("coffin type in c");
                }
                StartCoroutine(CompleteGame(coffinType));
            }
        }

        
        

        private IEnumerator CompleteGame(string type)
        {
            yield return new WaitForSeconds(2.3f);

            GameManager.Instance.Complete(type);
        }
    }
}
