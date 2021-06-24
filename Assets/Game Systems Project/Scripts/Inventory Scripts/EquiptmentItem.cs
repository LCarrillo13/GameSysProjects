using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plr;

namespace InventoryScripts
{

    [System.Serializable]
    public class EquiptmentItem : Item
    {



        public bool isEquipted = false;

        public Player.EquiptmentSlot slot = Player.EquiptmentSlot.Booties;

        public override void OnClicked()
        {
            base.OnClicked();

            Player player = GameObject.FindObjectOfType<Player>();
            EquiptmentItem oldItem = player.EquipItem(this);

            Inventory inventory = GameObject.FindObjectOfType<Inventory>();
            if(oldItem != null)
            {
                inventory.AddItem(oldItem);
            }

            inventory.RemoveItem(this);
        }

    }
}
