using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class WeatherControl : MonoBehaviour
{
    public Image filter;
    public Camera cam;
    public TileGenerator tileGen;
    public ParticleSystem rain;
    public AudioSource rainSound;
    private Bloom bloom;

    // Tile order:
    // Neutral, Short, Tall, Fire, Shallow, Deep, Obstacles.
    void Start()
    {
        cam.GetComponent<PostProcessVolume>().profile.TryGetSettings(out bloom);
    }
    void Update()
    {
        // FOR TESTING.
        if (Input.GetKeyDown(KeyCode.Q)) Sunny();
        if (Input.GetKeyDown(KeyCode.W)) Rainy();
        if (Input.GetKeyDown(KeyCode.E)) Cloudy();
        if (Input.GetKeyDown(KeyCode.R)) Clear();
    }
    public void Generate(string weather)
    {
        if (weather == "Clear") Sunny();
        else if (weather == "Rain" || weather == "Drizzle" || weather == "Thunderstorm") Rainy();
        else if (weather == "Clouds") Cloudy();
        else Clear();

        //Vector2 mid = new Vector2(tileGen.height / 2, tileGen.width / 2);
        //Collider2D collider = Physics2D.OverlapCircle(mid, 0.5f);
        //if (collider.gameObject.tag == "Obstacle"
        //    || collider.gameObject.tag == "Food"
        //    || collider.gameObject.tag == "Shop"
        //    || collider.gameObject.tag == "Education") Generate(weather);
    }
    void Sunny()
    {
        tileGen.Generate(new float[]{0.8f, 0.83f, 0.86f, 0.95f, 0.95f, 0.95f, 1f});
        filter.color = new Color32(255, 30, 0, 30);
        bloom.intensity.value = 3;
        bloom.threshold.value = 0.5f;
        rain.Stop();
        rainSound.Stop();
    }
    void Rainy()
    {
        tileGen.Generate(new float[]{0.7f, 0.73f, 0.76f, 0.76f, 0.85f, 0.95f, 1f});
        filter.color = new Color32(0, 0, 0, 100);
        bloom.intensity.value = 1;
        bloom.threshold.value = 0.5f;
        rain.Play();
        rainSound.Play();
    }
    void Cloudy()
    {
        tileGen.Generate(new float[]{0.7f, 0.78f, 0.86f, 0.89f, 0.92f, 0.95f, 1f});
        filter.color = new Color32(0, 0, 0, 80);
        bloom.intensity.value = 1;
        bloom.threshold.value = 0.5f;
        rain.Stop();
        rainSound.Stop();
    }
    void Clear()
    {
        tileGen.Generate(new float[]{0.8f, 0.83f, 0.86f, 0.89f, 0.92f, 0.95f, 1f});
        filter.color = new Color32(0, 0, 0, 0);
        bloom.intensity.value = 1;
        bloom.threshold.value = 0.5f;
        rain.Stop();
        rainSound.Stop();
    }
}
