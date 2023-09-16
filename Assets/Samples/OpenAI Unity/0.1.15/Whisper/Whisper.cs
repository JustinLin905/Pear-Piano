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

                // key signature
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
                { "a flat minor.", "A♭ minor has seven flats, B♭, E♭, A♭, D♭, G♭, C♭, and F♭."},

                // triads
                {"c major chord", "The notes in the C major chord are C, E, G."},
                {"g major chord", "The notes in the G major chord are G, B, D."},
                {"d major chord", "The notes in the D major chord are D, F#, A."},
                {"a major chord", "The notes in the A major chord are A, C#, E."},
                {"e major chord", "The notes in the E major chord are E, G#, B."},
                {"b major chord", "The notes in the B major chord are B, D#, F#."},
                {"f sharp major chord", "The notes in the F# major chord are F#, A#, C#."},
                {"c sharp major chord", "The notes in the C# major chord are C#, E#, G#."},

                {"a minor chord.", "The notes in the A minor chord are A, C, E."},

                {"e minor chord.", "The notes in the E minor chord are E, G, B."},
                {"b minor chord.", "The notes in the B flat minor chord are B, D, F sharp."},
                {"f sharp minor chord.", "The notes in the F sharp minor chord are F sharp, A, C sharp."},
                {"c sharp minor chord.", "The notes in the C sharp minor chord are C sharp, E, G sharp."},
                {"g sharp minor chord.", "The notes in the G sharp minor chord are G sharp, B, D sharp."},
                {"d sharp minor chord.", "The notes in the D sharp minor chord are D sharp, F sharp, A sharp."},
                {"a sharp minor chord.", "The notes in the A sharp minor chord are A sharp, C sharp, E sharp."},

                {"f major chord.", "The notes in the F major chord are F, A, C."},
                {"b flat major chord.", "The notes in the B flat major chord are B flat, D, F."},
                {"e flat major chord.", "The notes in the E flat major chord are E flat, G, B flat."},
                {"a flat major chord.", "The notes in the A flat major chord are A flat, C, E flat."},
                {"d flat major chord.", "The notes in the D flat major chord are D flat, F, A flat."},
                {"g flat major chord.", "The notes in the G flat major chord are G flat, B flat, D flat."},
                {"c flat major chord.", "The notes in the C flat major chord are C flat, E flat, G flat."},

                {"d minor chord.", "The notes in the D minor chord are D, F, A."},
                {"g minor chord.", "The notes in the G minor chord are G, B flat, D."},
                {"c minor chord.", "The notes in the C minor chord are C, E flat, G."},
                {"f minor chord.", "The notes in the F minor chord are F, A flat, C."},
                {"b flat minor chord.", "The notes in the B flat minor chord are B flat, D flat, F."},
                {"e flat minor chord.", "The notes in the E flat minor chord are E flat, G flat, B flat."},
                {"a flat minor chord.", "The notes in the A flat minor chord are A flat, C flat, E flat."},

                // dominant 7th
                {"c major dominant seventh chord.", "The dominant seventh chord of C major is C, E, G, and B."},
                {"g major dominant seventh chord.", "The dominant seventh chord of G major is G, B, D, and F♯."},
                {"d major dominant seventh chord.", "The dominant seventh chord of D major is D, F♯, A, and C♯."},
                {"a major dominant seventh chord.", "The dominant seventh chord of A major is A, C♯, E, and G♯."},
                {"e major dominant seventh chord.", "The dominant seventh chord of E major is E, G♯, B, and D♯."},
                {"b major dominant seventh chord.", "The dominant seventh chord of B major is B, D♯, F♯, and A#."},
                {"f sharp major dominant seventh chord.", "The dominant seventh chord of F♯ major is F♯, A#, C♯, and E#."},
                {"c sharp major dominant seventh chord.", "The dominant seventh chord of C♯ major is C♯, E#, G♯, and B#."},
            
                {"a minor dominant seventh chord.", "The dominant seventh chord of A minor is A, C, E, and G."},
            
                {"e minor dominant seventh chord.", "The dominant seventh chord of E minor is E, G, B, and D."},
                {"b minor dominant seventh chord.", "The dominant seventh chord of B♭ minor is B, D, F♯, and A."},
                {"f sharp minor dominant seventh chord.", "The dominant seventh chord of F♯ minor is F♯, A, C♯, and E."},
                {"c sharp minor dominant seventh chord.", "The dominant seventh chord of C♯ minor is C♯, E, G♯, and B."},
                {"g sharp minor dominant seventh chord.", "The dominant seventh chord of G♯ minor is G♯, B, D♯, and F♯"},
                {"d sharp minor dominant seventh chord.", "The dominant seventh chord of D♯ minor is D♯, F♯, A♯, and C♯."},
                {"a sharp minor dominant seventh chord.", "The dominant seventh chord of A♯ minor is A♯, C♯, E♯, and G♯."},
            
                {"f major dominant seventh chord.", "The dominant seventh chord of F major is F, A, C, and E."},
                {"b flat major dominant seventh chord.", "The dominant seventh chord of B♭ major is B♭, D, F, and A."},
                {"e flat major dominant seventh chord.", "The dominant seventh chord of E♭ major is E♭, G, B♭, and D."},
                {"a flat major dominant seventh chord.", "The dominant seventh chord of A♭ major is A♭, C, E♭, and G."},
                {"d flat major dominant seventh chord.", "The dominant seventh chord of D♭ major is D♭, F, A♭, and C."},
                {"g flat major dominant seventh chord.", "The dominant seventh chord of G♭ major is G♭, B♭, D♭, and F."},
                {"c flat major dominant seventh chord.", "The dominant seventh chord of C♭ major is C♭, E♭, G♭, and B♭."},
            
                {"d minor dominant seventh chord.", "The dominant seventh chord of D minor is D, F, A, and C."},
                {"g minor dominant seventh chord.", "The dominant seventh chord of G minor is G, B♭, D, and F."},
                {"c minor dominant seventh chord.", "The dominant seventh chord of C minor is C, E♭, G, and B♭."},
                {"f minor dominant seventh chord.", "The dominant seventh chord of F minor is F, A♭, C, and E."},
                {"b flat minor dominant seventh chord.", "The dominant seventh chord of B♭ minor is B♭, D♭, F, and A."},
                {"e flat minor dominant seventh chord.", "The dominant seventh chord of E♭ minor is E♭, G♭, B♭, and D."},
                {"a flat minor dominant seventh chord.", "The dominant seventh chord of A♭ minor is A♭, C♭, E♭, and G."},

                // diminshed 7th
                {"c major diminished seventh chord.", "The diminished seventh chord of C major is C, E♭, G♭, and A♭."},
                {"g major diminished seventh chord.", "The diminished seventh chord of G major is G, B♭, D♭, and E♭."},
                {"d major diminished seventh chord.", "The diminished seventh chord of D major is D, F, A♭, and B♭."},
                {"a major diminished seventh chord.", "The diminished seventh chord of A major is A, C, E♭, and F♯."},
                {"e major diminished seventh chord.", "The diminished seventh chord of E major is E, G, B♭, and C♯."},
                {"b major diminished seventh chord.", "The diminished seventh chord of B major is B, D, F, and G♯."},
                {"f sharp major diminished seventh chord.", "The diminished seventh chord of F♯ major is F♯, A, C, and D♯."},
                {"c sharp major diminished seventh chord.", "The diminished seventh chord of C♯ major is C♯, E, G, and A♯."},
            
                {"a minor diminished seventh chord.", "The diminished seventh chord of A minor is A, C, E♭, and G."},
            
                {"e minor diminished seventh chord.", "The diminished seventh chord of E minor is E, G, B♭, and D."},
                {"b minor diminished seventh chord.", "The diminished seventh chord of B♭ minor is B, D, F, and A♭."},
                {"f sharp minor diminished seventh chord.", "The diminished seventh chord of F♯ minor is F♯, A, C, and E."},
                {"c sharp minor diminished seventh chord.", "The diminished seventh chord of C♯ minor is C♯, E, G, and B."},
                {"g sharp minor diminished seventh chord.", "The diminished seventh chord of G♯ minor is G♯, B, D, and F♯."},
                {"d sharp minor diminished seventh chord.", "The diminished seventh chord of D♯ minor is D♯, F♯, A, and C♯."},
                {"a sharp minor diminished seventh chord.", "The diminished seventh chord of A♯ minor is A♯, C♯, E, and G♯."},
            
                {"f major diminished seventh chord.", "The diminished seventh chord of F major is F, A♭, B, and D."},
                {"b flat major diminished seventh chord.", "The diminished seventh chord of B♭ major is B♭, D♭, E, and G."},
                {"e flat major diminished seventh chord.", "The diminished seventh chord of E♭ major is E♭, G♭, A, and B."},
                {"a flat major diminished seventh chord.", "The diminished seventh chord of A♭ major is A♭, C♭, D, and E."},
                {"d flat major diminished seventh chord.", "The diminished seventh chord of D♭ major is D♭, F♭, G, and A."},
                {"g flat major diminished seventh chord.", "The diminished seventh chord of G♭ major is G♭, B♭, C, and D."},
                {"c flat major diminished seventh chord.", "The diminished seventh chord of C♭ major is C♭, E♭, F, and G."},
            
                {"d minor diminished seventh chord.", "The diminished seventh chord of D minor is D, F, A♭, and B♭."},
                {"g minor diminished seventh chord.", "The diminished seventh chord of G minor is G, B♭, D♭, and E♭."},
                {"c minor diminished seventh chord.", "The diminished seventh chord of C minor is C, E♭, G♭, and A♭."},
                {"f minor diminished seventh chord.", "The diminished seventh chord of F minor is F, A♭, B, and D."},
                {"b flat minor diminished seventh chord.", "The diminished seventh chord of B♭ minor is B♭, D♭, E, and G♭."},
                {"e flat minor diminished seventh chord.", "The diminished seventh chord of E♭ minor is E♭, G♭, B♭, and D."},
                {"a flat minor diminished seventh chord.", "The diminished seventh chord of A♭ minor is A♭, C♭, D, and E."}
            };
            _text = _text.ToLower();
            string[] text = _text.Split(new char[] {' ', '-'}); 
            int sze = text.Length-1; 
            string search = text[sze];
            for (int i = sze - 1; i > 0; i--) {
                if (text[i].Length >= 1) {
                    if (text[i] == "7th") search = "seventh " + search;
                    else search = text[i] + " " + search;
                }
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
