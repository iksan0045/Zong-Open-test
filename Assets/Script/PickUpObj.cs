using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zong
{
    public class PickUpObj : MonoBehaviour,IInteractable
    {
        private Rigidbody rb;

        [SerializeField]
        private Collider coll;

        [SerializeField]
        private bool equipped;

        private Transform playerContainer;
        
        private Transform targetPlace;

        private AudioSource audioSource;
        [SerializeField]
        public List<SoundEffect> soundEffects = new List<SoundEffect>();
        


        void Start()
        {
            rb = GetComponent<Rigidbody>();
            audioSource = gameObject.AddComponent<AudioSource>();
        }


        void Update()
        {
            
        }

        public void Interact()
        {
            Debug.Log("interacted");
        }

        public void PickUp(Transform targetTransform)
        {
            playerContainer = targetTransform;

            transform.SetParent(playerContainer);
           
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            transform.localScale = Vector3.one;
            PlaySfx("pick up");

            SetEquipped(true);
        }

        public void PlaceObject(Transform placeTransform)
        {
            SetEquipped(false);
            transform.SetParent(placeTransform);
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            rb.isKinematic = true;
            coll.isTrigger = false;
            PlaySfx("placed");
            GetComponentInParent<DestinationType>().PlayVFX();
        }

        public void Drop()
        {
            SetEquipped(false);
            PlaySfx("drop");
            transform.SetParent(null);

            rb.AddForce(playerContainer.forward * 3, ForceMode.Impulse);
            rb.AddForce(playerContainer.up * 3, ForceMode.Impulse);
            float random = UnityEngine.Random.Range(-1f, 1f);
            rb.AddTorque(new Vector3(random, random, random) * 10);
        }

        public void SetEquipped(bool isEquipped)
        {
            equipped = isEquipped;

            if (equipped)
            {
                rb.isKinematic = true;
                coll.isTrigger = true;
            }
            else
            {
                rb.isKinematic = false;
                coll.isTrigger = false;
            }
        }

        private void PlaySfx(string sfxName)
        {
            audioSource.PlayOneShot(soundEffects.Find(s => s.name == sfxName).clip);

        }

    }

    [System.Serializable]
    public class SoundEffect
    {
        public string name;
        public AudioClip clip;
    }

}