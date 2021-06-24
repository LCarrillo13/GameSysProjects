using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;
namespace InventoryScripts
{

    public class Inventory : MonoBehaviour
    {

        [SerializeField] private Button ButtonPrefab;
        [SerializeField] private GameObject InventoryGameObject;
        [SerializeField] private GameObject InventoryContent;
        [SerializeField] private GameObject FilterContent;

        [NonSerialized] public Item selectedItem = null;



        [SerializeField] private List<Item> inventory = new List<Item>();
        [SerializeField] private bool showIMGUIInventory = true;

        [Header("selected Item Display")]
        [SerializeField] private RawImage itemImage;
        [SerializeField] private Text itemText;
        [SerializeField] private Text itemDesc;
        

        #region Display Inventory

        private Vector2 scrollPosition;
        private string sortType = "All";


        private void DisplaySelectedItems()
        {
            GUI.Box(new Rect(Screen.width / 4, Screen.height / 3,
                Screen.width / 5, Screen.height / 5),
                selectedItem.Icon);

            GUI.Box(new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height / 5),
               Screen.width / 7, Screen.height / 15),
               selectedItem.Name);

            GUI.Box(new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height / 3),
               Screen.width / 5, Screen.height / 5),
               selectedItem.Description +
               "\nValue: " + selectedItem.Value +
               "\nAmount: " + selectedItem.Amount);

        }


        void DisplaySelectedItemOnCanvas(Item item)
        {
            selectedItem = item;

            if(item == null)
            {
                itemImage.texture = null;
                //itemName.text
                
                //)
                {

                }
            }

        }

        private void Display()
        {
            scrollPosition = GUI.BeginScrollView(new Rect(0, 40, Screen.width, Screen.height - 40),
                scrollPosition,
                new Rect(0, 0, 0, inventory.Count * 30),
                false,
                true);
            int count = 0;
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].Type.ToString() == sortType || sortType == "All")
                {
                    if (GUI.Button(new Rect(30, 0 + (count + 30), 200, 30), inventory[i].Name))
                    {
                        selectedItem = inventory[i];
                    }
                    count++;
                }
            }
            GUI.EndScrollView();
        }


        #endregion

        public void AddItem(Item _item)
        {
            inventory.Add(_item);
        }

        public void RemoveItem(Item _item)
        {
            if(inventory.Contains(_item))
            {
                inventory.Remove(_item);
            }
        }

        private void OnGUI()
        {
            if (showIMGUIInventory)
            {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

                List<string> itemTypes = new List<string>(Enum.GetNames(typeof(Item.ItemType)));
                itemTypes.Insert(0, "All");

                for (int i = 0; i < itemTypes.Count; i++)
                {
                    if (GUI.Button(new Rect(
                        (Screen.width / itemTypes.Count) * i
                        , 10
                        , Screen.width / itemTypes.Count
                        , 20), itemTypes[i]))

                    {
                        sortType = itemTypes[i];
                    }                                            

                }
                Display();
                if(selectedItem != null)
                {
                    DisplaySelectedItems();
                }
        
                    
                       


            }

        }

        private void DisplayItemCanvas()
        {

            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].Type.ToString() ==  sortType || sortType == "All")
                {
                    Button buttonGO = Instantiate<Button>(ButtonPrefab, InventoryContent.transform);
                    Text buttonText = buttonGO.GetComponentInChildren<Text>();
                    buttonGO.name = inventory[i].Name + " button";
                    buttonText.text = inventory[i].Name;

                }
            }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                InventoryGameObject.SetActive(true);
                DisplayItemCanvas();
            }
        }
    }
}
