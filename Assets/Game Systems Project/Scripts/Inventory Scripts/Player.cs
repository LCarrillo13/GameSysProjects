using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryScripts;

namespace Plr
{
    public class Player : MonoBehaviour
    {


        public enum EquiptmentSlot
        {
            Helmet,
            Chestplate,
            Pantaloons,
            Booties,
            StabbyThing,
            ProtectyThing
        }

        private Dictionary<EquiptmentSlot, EquiptmentItem> slots = new Dictionary<EquiptmentSlot, EquiptmentItem>();
        // Start is called before the first frame update
        void Start()
        {
            foreach (EquiptmentSlot slot in System.Enum.GetValues(typeof(EquiptmentSlot)))
            {
                slots.Add(slot, null);
            }
        }


        /// <summary>
        /// DO NOT PASS NULL INTO THIS. IT WILL BREAK
        /// </summary>
       
        public EquiptmentItem EquipItem(EquiptmentItem _toEquip)
        {

            if(_toEquip == null)
            {
                Debug.LogError("WHY WOULD YOU PASS NULL INTO THIS. YOU WERE WARNED!");
                return null;
            }
            //Attempt to get ANYTHING out of slot, be it null or not
            if(slots.TryGetValue(_toEquip.slot, out EquiptmentItem item))
            {
                //create a copy of the original, set the slot item to the passed value
                EquiptmentItem original = item;
                slots[_toEquip.slot] = _toEquip;
                // Return what was originally in the slot to prevent losing items when equipping
                return original;
            }

            //SOMEHOW the slot didnt exist, so lets create it and return null as no item
            //would be in the slot anyway
            slots.Add(_toEquip.slot, _toEquip);
            return null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
