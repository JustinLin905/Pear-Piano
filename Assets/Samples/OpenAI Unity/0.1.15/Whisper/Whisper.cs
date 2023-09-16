using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;

namespace OpenAI
{
    public class Whisper : MonoBehaviour
    {
        [SerializeField] private Button recordButton;
        [SerializeField] private Image progressBar;
        [SerializeField] private Text message;
        [SerializeField] private Dropdown dropdown;
        
        private readonly string fileName = "output.wav";
        private readonly int duration = 5;
        
        private AudioClip clip;
        private bool isRecording;
        private float time;
        private OpenAIApi openai = new OpenAIApi();

        public Text resultText;

        private void Start()
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
            dropdown.options.Add(new Dropdown.OptionData("Microphone not supported on WebGL"));
            #else
            foreach (var device in Microphone.devices)
            {
                dropdown.options.Add(new Dropdown.OptionData(device));
            }
            recordButton.onClick.AddListener(StartRecording);
            dropdown.onValueChanged.AddListener(ChangeMicrophone);
            
            var index = PlayerPrefs.GetInt("user-mic-device-index");
            dropdown.SetValueWithoutNotify(index);
            #endif
        }

        private void ChangeMicrophone(int index)
        {
            PlayerPrefs.SetInt("user-mic-device-index", index);
        }
        
        private void StartRecording()
        {
            isRecording = true;
            recordButton.enabled = false;

            var index = PlayerPrefs.GetInt("user-mic-device-index");
            
            #if !UNITY_WEBGL
            clip = Microphone.Start(dropdown.options[index].text, false, duration, 44100);
            #endif
        }

        private async void EndRecording()
        {
            message.text = "Transcripting...";
            
            #if !UNITY_WEBGL
            Microphone.End(null);
            #endif
            
            byte[] data = SaveWav.Save(fileName, clip);
            
            var req = new CreateAudioTranscriptionsRequest
            {
                FileData = new FileData() {Data = data, Name = "audio.wav"},
                // File = Application.persistentDataPath + "/" + fileName,
                Model = "whisper-1",
                Language = "en"
            };
            var res = await openai.CreateAudioTranscription(req);

            progressBar.fillAmount = 0;
            message.text = res.Text;
            recordButton.enabled = true;
            wordSearch(res.Text);
        }

        private void wordSearch(string _text) {
            Dictionary<string, string> unorderedMap = new Dictionary<string, string> {
                { "c major.", "C major has no sharps or flats." },
          
                { "g major.", "G major has one sharp, F#."},
                { "d major.", "D major has two sharps,  F# and C#."},
                { "a major.", "A major has three sharps, F#, C#, and G#."},
                { "e major.", "E major has four sharps, F#, C#, G#, and D#."},
                { "b major.", "B major has five sharps, F#, C#, G#, D#, and A#."},
                { "f sharp major.", "F# major has six sharps, F#, C#, G#, D#, A#, and E#."},
                { "c sharp major.", "C# major has seven sharps, F#, C#, G#, D#, A#, E#, and B#."},

                { "a minor.", "A minor has no sharps or flats." },
            
                { "e minor.", "E minor has one sharp, F#."},
                { "b minor.", "B minor has two sharps,  F# and C#."},
                { "f sharp minor.", "F# minor has three sharps, F#, C#, and G#."},
                { "c sharp minor.", "C# minor has four sharps, F#, C#, G#, and D#."},
                { "g sharp minor.", "G# minor has five sharps, F#, C#, G#, D#, and A#."},
                { "d sharp minor.", "D# minor has six sharps, F#, C#, G#, D#, A#, and E#."},
                { "a sharp minor.", "A# minor has seven sharps, F#, C#, G#, D#, A#, E#, and B#."},

                { "f major.", "F major has one flat, B♭."},
                { "b flat major.", "B♭ major has two flats,  B♭ and E♭."},
                { "e flat major.", "E♭ major has three flats, B♭, E♭, and A♭."},
                { "a flat major.", "A♭ major has four flats, B♭, E♭, A♭, and D♭."},
                { "d flat major.", "D♭ major has five flats, B♭, E♭, A♭, D♭, and G♭."},
                { "g flat major.", "G♭ major has six flats, B♭, E♭, A♭, D♭, G♭, and C♭."},
                { "c flat major.", "C♭ major has seven flats, B♭, E♭, A♭, D♭, G♭, C♭, and F♭."},

                { "d minor.", "D minor has one flat, B♭."},
                { "g minor.", "G minor has two flats,  B♭ and E♭."},
                { "c minor.", "C minor has three flats, B♭, E♭, and A♭."},
                { "f minor.", "F minor has four flats, B♭, E♭, A♭, and D♭."},
                { "b flat minor.", "B♭ minor has five flats, B♭, E♭, A♭, D♭, and G♭."},
                { "e flat minor.", "E♭ minor has six flats, B♭, E♭, A♭, D♭, G♭, and C♭."},
                { "a flat minor.", "A♭ minor has seven flats, B♭, E♭, A♭, D♭, G♭, C♭, and F♭."}
            };
            _text = _text.ToLower();
            string[] text = _text.Split(new char[] {' ', '-'}); 
            int sze = text.Length-1; 
            string search = text[sze];
            for (int i = sze - 1; i > 0; i--) {
                if (text[i].Length >= 1) search = text[i] + " " + search;
                if (text[i].Length == 1) break;
            }


            // debug

            // string test = "";
            // for (int i = 0; i < text.Length; i++) test += text[i] + " ";
            
            resultText.text = unorderedMap[search];

            // resultText.text = unorderedMap[search];
        }

        private void Update()
        {
            if (isRecording)
            {
                time += Time.deltaTime;
                progressBar.fillAmount = time / duration;
                
                if (time >= duration)
                {
                    time = 0;
                    isRecording = false;
                    EndRecording();
                }
            }
        }
    }
}
