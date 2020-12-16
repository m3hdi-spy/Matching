using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    public byte Counter = 0;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.GetComponent<FloatingButton>().isDragging)
        {
            if (!collision.gameObject.GetComponent<FloatingButton>().backToOriginals)
            {
                Counter++;

                if (GameManager.Instance.IsMainGame)
                {
                    g_UIManager.Instance.DecreaseHeal();
                    
                }
                collision.gameObject.GetComponentInParent<ButtonsPlace>().MoveToTop(collision.transform);
                
            }
            
        }
    }
}
