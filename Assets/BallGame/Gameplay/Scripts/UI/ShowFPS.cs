using UnityEngine;
using TMPro;

namespace BallGame.Gameplay.UI
{
    public class ShowFPS : MonoBehaviour
    {
        [SerializeField] private TMP_Text fpsText;
        [SerializeField] private float updateInterval = 0.2f;

        private float timer;
        private int frames;

        //Calc and set Fps to text
        void Update()
        {

            frames++;
            timer += Time.unscaledDeltaTime;

            if (timer >= updateInterval)
            {
                int fps = Mathf.RoundToInt(frames / timer);

                fpsText.text = fps + " FPS";

                frames = 0;
                timer = 0f;
            }
        }
    }
}

