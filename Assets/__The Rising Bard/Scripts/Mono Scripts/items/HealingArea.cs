using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingArea : MonoBehaviour
{
    public delegate void noParam();
    internal static event noParam HealPlayer;
    internal static event noParam updateUI;

    [SerializeField] Material defultMat;
    [SerializeField] Material matToChange;
    [SerializeField] Color BaseColor;


    [SerializeField] PlayerData PD;


    private Animator playerAnimator;
    bool isHealing;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine("StartHealing", collision);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (isHealing)
        {
            collision.GetComponent<Renderer>().material = defultMat;

            StopCoroutine("StartHealing");

        }


    }
    IEnumerator StartHealing(Collider2D collision)
    {
        while (PD.playerHP < PD.playerMaxHP || PD.playerMana < PD.playerMaxMana)
        {
            isHealing = true;
            playerAnimator = collision.GetComponent<Animator>();
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("W_Idle") &&
            playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            collision.GetComponent<Renderer>().material = matToChange;
            HealPlayer.Invoke();
            updateUI.Invoke();
            yield return new WaitForSeconds(0.25F);

        }
        isHealing = false;
        collision.GetComponent<Renderer>().material = defultMat;

    }
}
