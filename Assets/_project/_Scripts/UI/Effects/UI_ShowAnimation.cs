using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.UI
{
    public class UI_ShowAnimation : MonoBehaviour
    {
        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void TogglePanel(bool isShow)
        {
            if (isShow)
            {
                gameObject.SetActive(true);
                animator.SetTrigger("Show");
            }
            else
            {
                animator.SetTrigger("Hide");
            }
        }

        public void HidePanle()
        {
            gameObject.SetActive(false);
        }
    }
}