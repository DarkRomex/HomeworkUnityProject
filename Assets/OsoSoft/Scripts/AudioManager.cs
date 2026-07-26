using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instancia;

    [Header("Sources")]
    public AudioSource musicaSource;
    public AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip musicaInicio;
    public AudioClip sonidoMoneda;
    public AudioClip sonidoCambioEscena;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);

            if (musicaSource != null && musicaInicio != null)
            {
                musicaSource.clip = musicaInicio;
                musicaSource.loop = true;
                musicaSource.playOnAwake = false;
                if (!musicaSource.isPlaying)
                    musicaSource.Play();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ReproducirMoneda()
    {
        if (sfxSource != null && sonidoMoneda != null)
            sfxSource.PlayOneShot(sonidoMoneda);
    }

    public void ReproducirCambioEscena()
    {
        if (sfxSource != null && sonidoCambioEscena != null)
            sfxSource.PlayOneShot(sonidoCambioEscena);
    }

    public void CambiarEscenaConSonido(string nombreEscena)
    {
        StartCoroutine(CambiarEscenaCoroutine(nombreEscena));
    }

    IEnumerator CambiarEscenaCoroutine(string nombreEscena)
    {
        if (sfxSource != null && sonidoCambioEscena != null)
        {
            sfxSource.PlayOneShot(sonidoCambioEscena);
            yield return new WaitForSeconds(sonidoCambioEscena.length);
        }

        SceneManager.LoadScene(nombreEscena);
    }
}