using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemCheckerScript : MonoBehaviour
{
    public float interaction_range = 3f;
    public LayerMask items;
    private GameObject NowItnteract;
    public TMP_Text description;
    public GameObject holder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        
            if(other.tag == "interact" )
            {
                NowItnteract = other.gameObject;
                if(this.GetInteract().layer == 9)//КОСТЁР
                {
                    description.text = "Отдохнуть y костра (F)";
                }
                if(this.GetInteract().layer == 7)//ПРЕДМЕТ
                {
                    description.text = "Подобрать предмет (F)";
                }
            } 
    }

    // private void OnTriggerStay(Collider other) 
    // {

    // }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "interact" )
            { 
                try
                {
                    if(other.transform.GetComponent<ItemContainer>().transform.parent.name != "holder")
                    {
                        
                    }
                }
                catch
                {
                    description.text = "";
                    NowItnteract = null;
                }
                finally
                {

                }
            } 
    }

    public GameObject GetInteract()
    {
        return NowItnteract;
    }

    public void Clear()
    {
        description.text = "";
        NowItnteract = null; 
    }
    //ВОТ ТУТ ТЫ НАПИШЕШЬ МЕТОД К КОТОРОМУ ПОТОМ БУДЕШЬ ОБРАЩАТЬСЯ ИЗ СКРИПТА ПЕРСОНАЖА КОТОРЫЙ БУДЕТ ПОДНИМАТЬ ПРЕДМЕТ
}
