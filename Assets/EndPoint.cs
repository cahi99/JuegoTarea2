using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndPoint : MonoBehaviour
{
    [SerializeField] private bool final;
    public GameObject MenuCreditos;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !final)
        {
            other.GetComponent<CombateJugador>().saveLife();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (final)
        {
            Time.timeScale = 0f;
            MenuCreditos.SetActive(true);
        }
    }
}
