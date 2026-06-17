using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace TransformerTextToMusic
{
    public class PolyphonyRAM : IDisposable
    {
        //Змінні для роботи зі звуком
        private RamInstrument[] instruments;
        private MixingSampleProvider mixer;
        private WaveOutEvent outputDevice;

        private Constants constants=new Constants();

        private string folderPath= "Instruments/";
        private bool checkSoundFiles = false;
        private bool checkStopPlayer = true;
        private SettingsSample[] settingsSample;

        //Новий шлях до папки зі звуками
        public void NewFolderPath(string folderPath)
        {
            this.folderPath = folderPath;
        }

        //Перевірка на наявність файлів
        private void CheckingForSoundFiles()
        {
            byte error = 0;
            Console.WriteLine("... Start checking for sound files ...");

            foreach(string f in constants.soundFiles)
            {
                if(!File.Exists($"{folderPath}{f}"))
                {
                    error++;
                    Console.WriteLine($"Файл не знайдено: {folderPath}{f}");
                }
            }
            Console.WriteLine($"Error = {error}");
            Console.WriteLine("... Finish checking for sound files ...");

            if (error != 0)
            {
                Console.WriteLine($"Помилок знайдено: {error}");
                checkSoundFiles = false;
            }
            else
            {
                LoadInstruments();
                checkSoundFiles =true;
            }
        }

        private void LoadInstruments()
        {
            instruments = new RamInstrument[20];
            for(byte i=0;i<20;i++)
                instruments[i] = new RamInstrument();
            instruments[(byte)InstrumentID.SoundBassoon].LoadToRAM(folderPath + "SoundBassoon.mp3");
            instruments[(byte)InstrumentID.SoundCello].LoadToRAM(folderPath + "SoundCello.mp3");
            instruments[(byte)InstrumentID.SoundChimes].LoadToRAM(folderPath + "SoundChimes.mp3");
            instruments[(byte)InstrumentID.SoundChoir].LoadToRAM(folderPath + "SoundChoir.mp3");
            instruments[(byte)InstrumentID.SoundClarinet].LoadToRAM(folderPath + "SoundClarinet.mp3");
            instruments[(byte)InstrumentID.SoundDoubleBass].LoadToRAM(folderPath + "SoundDoubleBass.mp3");
            instruments[(byte)InstrumentID.SoundFlute].LoadToRAM(folderPath + "SoundFlute.mp3");
            instruments[(byte)InstrumentID.SoundGlockenspiel].LoadToRAM(folderPath + "SoundGlockenspiel.mp3");
            instruments[(byte)InstrumentID.SoundHarp].LoadToRAM(folderPath + "SoundHarp.mp3");
            instruments[(byte)InstrumentID.SoundHorn].LoadToRAM(folderPath + "SoundHorn.mp3");
            instruments[(byte)InstrumentID.SoundOboe].LoadToRAM(folderPath + "SoundOboe.mp3");
            instruments[(byte)InstrumentID.SoundPercussion].LoadToRAM(folderPath + "SoundPercussion.mp3");
            instruments[(byte)InstrumentID.SoundPiano].LoadToRAM(folderPath + "SoundPiano.mp3");
            instruments[(byte)InstrumentID.SoundTimpani].LoadToRAM(folderPath + "SoundTimpani.mp3");
            instruments[(byte)InstrumentID.SoundTrombone].LoadToRAM(folderPath + "SoundTrombone.mp3");
            instruments[(byte)InstrumentID.SoundTrumpet].LoadToRAM(folderPath + "SoundTrumpet.mp3");
            instruments[(byte)InstrumentID.SoundTuba].LoadToRAM(folderPath + "SoundTuba.mp3");
            instruments[(byte)InstrumentID.SoundViola].LoadToRAM(folderPath + "SoundViola.mp3");
            instruments[(byte)InstrumentID.SoundViolin].LoadToRAM(folderPath + "SoundViolin.mp3");
            instruments[(byte)InstrumentID.SoundXylophone].LoadToRAM(folderPath + "SoundXylophone.mp3");
        }
        public void NewSettingsSample(byte count, SettingsSample[] array)
        {
            settingsSample = new SettingsSample[count];
            for(byte i=0;i<count;i++)
            {
                settingsSample[i] = new SettingsSample(array[i].GetStep(), array[i].GetIterator(), array[i].GetInstrumentID());
            }
        }

        public PolyphonyRAM()
        {
            settingsSample = new SettingsSample[1] { new SettingsSample(0, 0, (byte)InstrumentID.SoundHarp) };

            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 2));
            mixer.ReadFully = true; //автоматично повертає тишу, коли звуки завершились

            outputDevice = new WaveOutEvent();
            outputDevice.Init(mixer);
            outputDevice.Play();
        }

        public void PlayInstument(byte insturmentID, double start, double end)
        {
            ISampleProvider note = instruments[insturmentID].CreateNote(start, end);
            mixer.AddMixerInput(note);
        }
        public async Task TokenToMusicAsync(byte instrumentID, Track bufferTokens)
        {
            checkStopPlayer = false;
            if (checkSoundFiles == false)
                CheckingForSoundFiles();
            if (checkSoundFiles == true)
            {
                ClearMixer();
                if (bufferTokens.GetBufferTokens().Length != 0 && bufferTokens != null)
                {
                    Console.WriteLine("... Start transform from tokens to music ...");
                    foreach (byte key in bufferTokens.GetBufferTokens())
                    {
                        if(checkStopPlayer==true)
                            break;

                        if(key!=0)
                            PlayInstument(instrumentID, constants.tokenToSoundMap[key].start, constants.tokenToSoundMap[key].end);

                        Console.Write($" {key.ToString("D2")}");
                        await Task.Delay(250);
                    }
                    Console.WriteLine("\n... Finish transform from tokens to music ...");
                }
                else
                {
                    Console.WriteLine("Error: bufferTokens == 0 or null. Place: PolyphonyRAM.cs > TokenToMusicAsync().");
                }
            }
        }
        public async Task PolyphonyToMusicAsync(byte[] instrumentsID, byte numberOfTraks, Track[] bufferTokens)
        {
            checkStopPlayer = false;
            if (checkSoundFiles == false)
                CheckingForSoundFiles();
            if (checkSoundFiles == true)
            {
                ClearMixer();
                Console.WriteLine("... Start transform from tokens to polyphony music ...");
                
                int maxLength = bufferTokens[0].GetBufferTokens().Length;
                for(byte i = 1; i < numberOfTraks; i++)
                    if (maxLength < bufferTokens[i].GetBufferTokens().Length)
                        maxLength = bufferTokens[i].GetBufferTokens().Length;

                for(int i=0;i<maxLength;i++)
                {

                    if (checkStopPlayer == true)
                        break;
                    Console.Write($"I {i} >");
                    for (byte j = 0; j < numberOfTraks; j++)
                    {
                        if (i < bufferTokens[j].GetBufferTokens().Length)
                        {
                            if(bufferTokens[j].GetBufferTokens()[i]!=0)
                                PlayInstument(instrumentsID[j],
                                    constants.tokenToSoundMap[bufferTokens[j].GetBufferTokens()[i]].start,
                                    constants.tokenToSoundMap[bufferTokens[j].GetBufferTokens()[i]].end);

                            Console.Write($" {bufferTokens[j].GetBufferTokens()[i].ToString("D2")}");
                        }
                        else
                            Console.Write(" --");
                    }
                    Console.Write(" | \n");
                    await Task.Delay(250);
                }
                Console.WriteLine("\n... Finish transform from tokens to polyphony music ...");
            }
        }
        public void StopPlay()
        {
            checkStopPlayer = true;
            ClearMixer();
        }

        private void ClearMixer()
        {
            mixer.RemoveAllMixerInputs();
        }

        public void Dispose()
        {
            outputDevice?.Stop();
            mixer?.RemoveAllMixerInputs();
            outputDevice?.Dispose();
            if (instruments != null)
            {
                foreach (RamInstrument file in instruments)
                    file?.Dispose();
                instruments = null;
            }
            outputDevice = null;
            mixer = null;
            Console.WriteLine("PolyphonyRAM cleaned");
        }
    }
}
