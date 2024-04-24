using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zong
{
    public interface IInteractable
    {
        void Interact();

        void PickUp(Transform transform);

        void PlaceObject(Transform placeTransform);
        
        void Drop();
    }
}
