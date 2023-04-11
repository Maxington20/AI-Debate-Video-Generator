using Microsoft.CognitiveServices.Speech;
using System;
using System.Net.NetworkInformation;
using System.Resources;
using NAudio.Wave;
using FFMpegCore;

public class Program
{
    public static async Task Main()
    {
        string content = $"Davis (default): Alright, let's settle this once and for all. Star Trek is infinitely better than Star Wars.\r\n\r\nJenny (excited): What?! Are you kidding me? Star Wars is the ultimate sci-fi adventure!\r\n\r\nDavis (friendly): I respect your opinion, Jenny, but let me explain why Star Trek is superior.\r\n\r\nJenny (hopeful): Go ahead, Davis. I'm listening.\r\n\r\nDavis (cheerful): Well, for starters, the storytelling is much more nuanced and intellectual in Star Trek.\r\n\r\nJenny (default): But Star Wars has epic space battles and lightsaber fights!\r\n\r\nDavis (angry): That's all flashy nonsense! Star Trek explores complex philosophical concepts and ethical dilemmas.\r\n\r\nJenny (unfriendly): Oh, please. Star Trek is boring and preachy. Star Wars is pure entertainment.\r\n\r\nDavis (shouting): You couldn't be more wrong! Star Trek has iconic characters like Captain Kirk and Mr. Spock.\r\n\r\nJenny (chat): But Luke Skywalker and Darth Vader are just as iconic, if not more!\r\n\r\nDavis (terrified): How can you even compare them? The Force is just a cheap plot device.\r\n\r\nJenny (hopeful): Actually, the Force is a metaphor for spirituality and the power of the human spirit.\r\n\r\nDavis (excited): That's ridiculous! Star Trek tackles real-world issues like racism, war, and politics.\r\n\r\nJenny (friendly): Sure, but Star Wars inspires hope and reminds us of the power of good triumphing over evil.\r\n\r\nDavis (default): But Star Trek has an optimistic vision of the future where humanity has overcome its flaws and works towards a better society.\r\n\r\nJenny (angry): And Star Wars has a timeless message about the battle between light and dark!\r\n\r\nDavis (chat): You can't deny that Star Trek has a loyal fanbase and has been around for decades.\r\n\r\nJenny (shouting): And you can't deny that Star Wars has a massive cultural impact and has spawned countless spin-offs and merchandise!\r\n\r\nDavis (unfriendly): But that's just it, Star Wars is all about making money. Star Trek has always been about exploring new ideas and pushing boundaries.\r\n\r\nJenny (cheerful): Well, I guess we'll just have to agree to disagree.\r\n\r\nDavis (hopeful): I guess so. But I still think Star Trek is better.\r\n\r\nJenny (default): And I still think Star Wars is better. \r\n\r\nDavis (unfriendly): You know, Jenny, sometimes I wonder if you even understand the depth and complexity of Star Trek.\r\n\r\nJenny (terrified): And sometimes I wonder if you even have a sense of humor, Davis.\r\n\r\nDavis (angry): Oh, I have a sense of humor, Jenny. It's just not as shallow as your love for Star Wars.\r\n\r\nJenny (chat): Shallow? Have you seen the size of the Star Trek convention crowds compared to the Star Wars ones?\r\n\r\nDavis (hopeful): That just proves my point. Star Wars is for the masses, Star Trek is for the intellectuals.\r\n\r\nJenny (cheerful): You mean the snobs, right?\r\n\r\nDavis (shouting): No, I mean people who appreciate good writing and thoughtful exploration of the human condition!\r\n\r\nJenny (excited): And I mean people who appreciate epic space battles and the power of the Force!\r\n\r\nDavis (default): Well, at least we can agree on one thing. We both have excellent taste in science fiction.\r\n\r\nJenny (angry): Speak for yourself, Davis. You're the one who thinks that watching people sit around and talk about warp engines is exciting.\r\n\r\nDavis (friendly): Hey, it's not just about the warp engines. It's about the relationships and the growth of the characters over time.\r\n\r\nJenny (unfriendly): Right, because we all want to watch a bunch of people in pajamas talking about their feelings for an hour.\r\n\r\nDavis (hopeful): You just don't understand the art of storytelling, Jenny.\r\n\r\nJenny (default): And you don't understand the art of having fun.\r\n\r\nDavis (cheerful): Oh, I have plenty of fun. I just don't need lightsabers and explosions to enjoy myself.\r\n\r\nJenny (shouting): And I don't need boring lectures about science and ethics to be entertained!\r\n\r\nDavis (terrified): Fine, you win. Star Wars is better. Happy now?\r\n\r\nJenny (chat): Ecstatic. But deep down, I know you'll always be a Trekkie at heart, Davis.\r\n\r\nDavis (friendly): And deep down, I know you'll always be a Jedi wannabe, Jenny.";

        content = content.Replace("\r", "");
        content = content.Replace(", Davis", " Davis");
        content = content.Replace(", Jenny", " Jenny");

        string[] dialogues = content.Split(new string[] { "\n\n" }, StringSplitOptions.None);

        var inputFiles = new string[dialogues.Length];
        var outputFile = $"C:\\Users\\maxhe\\OneDrive\\Pictures\\Saved Pictures\\Debates\\debate-finalAudio.mp4";
        var jennyImage = $"C:\\Users\\maxhe\\OneDrive\\Pictures\\Saved Pictures\\Debates\\Jenny.png";
        var davisImage = $"C:\\Users\\maxhe\\OneDrive\\Pictures\\Saved Pictures\\Debates\\Davis.png";
        var count = 1;

        foreach (var input in dialogues)
        {
            var name = input.Substring(0, input.IndexOf("(")).Trim();
            var speechStyle = input.Substring(input.IndexOf("(") + 1, input.IndexOf(")") - input.IndexOf("(") - 1);
            string dialog = input.Substring(input.IndexOf(":") + 2, input.Length - input.IndexOf(":") - 3);

            Console.WriteLine("The name: " + name + "\n");            
            Console.WriteLine("The speech style: " + speechStyle + "\n");            
            Console.WriteLine("The dialog: " + dialog + "\n");

            var audioFile = $"C:\\Users\\maxhe\\OneDrive\\Pictures\\Saved Pictures\\Debates\\debate-{count}.wav";

            await TextToSpeech(dialog, name, speechStyle, audioFile);                      

            var vidOutputFile = $"C:\\Users\\maxhe\\OneDrive\\Pictures\\Saved Pictures\\Debates\\debate-{count}.mp4";

            var imageFile = name == "Davis" ? davisImage : jennyImage;
            FFMpeg.PosterWithAudio(imageFile, audioFile, vidOutputFile);

            inputFiles[count - 1] = vidOutputFile;

            count++;
        }

        FFMpeg.Join(outputFile, inputFiles);

        // CombineWavFiles(inputFiles, outputFile);
    }

    public static async Task TextToSpeech(string text, string voiceName, string speechStyle, string audioFilePath)
    {
        var speechKey = "f5c6b2a2ee8343a6a2a4adf91b8d83c9";
        var regionKey = "eastus";

        var speechConfig = SpeechConfig.FromSubscription(speechKey, regionKey);

        //speechConfig.SpeechSynthesisVoiceName = "en-US-JennyNeural";

        var ssml = $"<speak xmlns=\"http://www.w3.org/2001/10/synthesis\" xmlns:mstts=\"http://www.w3.org/2001/mstts\" xmlns:emo=\"http://www.w3.org/2009/10/emotionml\" version=\"1.0\" xml:lang=\"en-US\">\r\n  <voice name=\"en-US-{voiceName}Neural\">\r\n    <s/>\r\n    <mstts:express-as style=\"{speechStyle}\">{text} ,</mstts:express-as>\r\n    <s/>\r\n  </voice></speak>";

        using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
        {
            var speechResult = await speechSynthesizer.SpeakSsmlAsync(ssml);

            if (speechResult.Reason == ResultReason.SynthesizingAudioCompleted)
            {
                using var stream = AudioDataStream.FromResult(speechResult);
                await stream.SaveToWaveFileAsync(audioFilePath);
                stream.Dispose();
            }
            else
            {
                Console.WriteLine($"Speech synthesis failed. Reason: {speechResult.Reason}");
                throw new InvalidOperationException($"Speech synthesis failed. Reason: {speechResult.Reason}");
            }
        }
    }    
}