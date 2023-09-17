## Inspiration
Composing music through scribbling notes or drag-and-dropping from MuseScore couldn't be more tedious. As pianists ourselves, we know the struggle of trying to bring our impromptu sessions to life without forgetting what we just played or having to record ourselves and write out the notes one by one. 

## What it does
Introducing PearPiano, a cute little pear that helps you pair the notes to your thoughts. As a musician's best friend, Pear guides pianists through an augmented simulation of a piano where the notes played are directly translated into a recording and stored for future use. Pear can read both single notes and chords played on the virtual piano, allowing playback of your music with cascading tiles for full immersion. Seek musical guidance from Pear by asking, "What is the key signature of C-major?" or "Tell me the notes of the E-major diminished 7th chord." To fine tune your compositions, use the "Edit mode," where musicians can rewind the clip and drag-and-drop notes for instant [EDIT].

## How we built it
Using Unity Game Engine and the Oculus Quest, musicians can airplay their music on an augmented piano for real-time music composition. We used OpenAI's Whisper for voice dictation and C# for all game-development scripts. The AR environment is entirely designed and generated using the Unity UI Toolkit, allowing our engineers to realize an immersive yet functional workspace [OR MUSICAL CORNER?].

## Challenges we ran into
- Calibrating and configuring hand tracking on the Oculus Quest
- Reducing positional offset when making contact with the virtual piano keys
- Building the piano in Unity: pitch of the notes and being able to play multiple at once

## Accomplishments that we're proud of
- Bringing a scaled **AR piano** to life with close-to-perfect functionalities
- Working with OpenAI and having our prompts produce correct answers
- Designing an interactive and aesthetic UI/UX with cascading tiles upon recording playback

## What we learned
- Working and designing our character/piano/interface in 3D
- Emily had 5 cups of coffee in half a day and is somehow alive

## What's next for PearPiano
- VR overlay feature to attach the augmented piano with a real one, enriching each practise session
- A rhythm checker to support an aspiring pianist stay on-beat and in-tune
- Detecting the depth of each note-press to provide feedback on the pianist's musical dynamics
- Implement auto-generation of music, auto-suggesting the key signature and the next few notes based on real-world data we would train it with such as piano pieces on the internet [DOES THIS SEEM TOO FAKE? CUZ THEN THEY'RE NOT COMPOSING ANYMORE]
