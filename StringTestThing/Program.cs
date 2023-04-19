using Microsoft.CognitiveServices.Speech;
using System;
using System.Net.NetworkInformation;
using System.Resources;
using NAudio.Wave;
using FFMpegCore;

public class Program
{
    // prompt
    //can you create a detailed, funny, swear word, and insult filled back and forth between Davis and Jenny about whether mortal kombat is better than street fighter,  making it clear who is speaking when and which style they are speaking in. Davis has the following styles: chat, angry, excited, friendly, cheerful, hopeful, terrified, unfriendly, shouting. Jenny has the same styles. do not include any preamble before or after, just jump right into the start of the debate. the format should be Name (style): dialog. example: Davis (angry): test dialog.   Have them go back and forth at least 10 times each. do not use any styles i have not listed
    public static async Task Main()
    {
        string topic = "Xbox vs Playstation";

        string content = $"Davis (chat): Jenny, PlayStation kicks Xbox's ass any day. Exclusive games? Chef's kiss.\r\n\r\nJenny (excited): Bullshit, Davis! Xbox Game Pass is fucking amazing! A never-ending buffet of games, baby!\r\n\r\nDavis (angry): What a joke! Half those games are ancient! PlayStation has God of War, The Last of Us, and Spider-Man. Suck it, you Xbox fanboy!\r\n\r\nJenny (unfriendly): Listen, you PlayStation douche, I'd rather not blow wads of cash on a few exclusives like some elitist prick!\r\n\r\nDavis (shouting): Oh, please! You're just salty 'cause PlayStation has better graphics, performance, and games. Suck it up, Jenny!\r\n\r\nJenny (cheerful): Aw, Davis, I'm thrilled you love your overpriced, overrated trash box. But I'll stick with Xbox and avoid snobs like you!\r\n\r\nDavis (hopeful): Maybe you'll pull your head out of your ass one day and see that PlayStation is the shit. Till then, have fun with your Xbox mediocrity!\r\n\r\nJenny (angry): You condescending fuck! My Xbox is a beast, unlike your precious little toy console!\r\n\r\nDavis (unfriendly): Cute, Jenny. Keep deluding yourself while I'm here enjoying the best shit your pathetic Xbox can only dream of.\r\n\r\nJenny (terrified): Oh no, Davis! I might miss some exclusives while I have a blast with my affordable, badass gaming machine! The horror!\r\n\r\nDavis (friendly): Fine, Jenny, let's just agree we love gaming, even if we don't see eye to eye on consoles. Truce?\r\n\r\nJenny (hopeful): Alright, Davis. PlayStation and Xbox fans can coexist. Here's hoping the next gen consoles blow our fucking minds!\r\n\r\nDavis (excited): You know what, Jenny? That's the spirit! Let's embrace our differences and see what each console has to offer.\r\n\r\nJenny (cheerful): Hell yeah, Davis! Let's share our favorite games and maybe find some common ground in the process.\r\n\r\nDavis (hopeful): Totally, Jenny. I'll try out some Xbox Game Pass titles, and maybe you can give those PlayStation exclusives a shot.\r\n\r\nJenny (friendly): Deal, Davis! And who knows? We might even find some cross-platform games we can play together.\r\n\r\nDavis (terrified): Woah, playing together? Now that's a scary thought! But hey, I'm game if you are.\r\n\r\nJenny (shouting): Let's do this, Davis! Time to bridge the console divide and show the world that gaming is all about fun and camaraderie!\r\n\r\nDavis (cheerful): Preach it, Jenny! We'll be the shining example of how gamers should unite, regardless of console preferences.\r\n\r\nJenny (unfriendly): Damn straight, Davis. And if anyone talks shit about our newfound friendship, they can shove it!\r\n\r\nDavis (angry): You're right, Jenny! Screw the haters! It's time to make gaming history and leave the console wars behind!\r\n\r\nJenny (excited): We're on the same page now, Davis! Let's show the world that gamers can put their differences aside and come together in the name of fun!";

        content = content.Replace("\r", "");
        content = content.Replace(", Davis", " Davis");
        content = content.Replace(", Jenny", " Jenny");

        string[] dialogues = content.Split(new string[] { "\n\n" }, StringSplitOptions.None);

        var inputFiles = new string[dialogues.Length];
        var outputFile = $"C:\\Users\\maxhe\\OneDrive\\Pictures\\Saved Pictures\\Debates\\{topic}.mp4";
        var jennyImage = $"C:\\Users\\maxhe\\OneDrive\\Pictures\\Saved Pictures\\Debates\\Jenny.png";
        var davisImage = $"C:\\Users\\maxhe\\OneDrive\\Pictures\\Saved Pictures\\Debates\\Davis.png";
        var count = 1;

        foreach (var input in dialogues)
        {            
            var name = input.Substring(0, input.IndexOf("(")).Trim();
            var speechStyle = input.Substring(input.IndexOf("(") + 1, input.IndexOf(")") - input.IndexOf("(") - 1);
            string dialog = input.Substring(input.IndexOf(":") + 2, input.Length - input.IndexOf(":") - 3);

            dialog = dialog.Replace("</prosody", "</prosody>");
            dialog = dialog.Replace(">>", ">");

            Console.WriteLine($"Part {count} of {dialogues.Length.ToString()}");
            Console.WriteLine("The name: " + name);            
            Console.WriteLine("The speech style: " + speechStyle);            
            Console.WriteLine("The dialog: " + dialog + "\n");

            var audioFile = $"C:\\Users\\maxhe\\OneDrive\\Pictures\\Saved Pictures\\Debates\\debate-{count}.wav";

            await TextToSpeech(dialog, name, speechStyle, audioFile);                      

            var vidOutputFile = $"C:\\Users\\maxhe\\OneDrive\\Pictures\\Saved Pictures\\Debates\\debate-{count}.mp4";

            var imageFile = name == "Davis" ? davisImage : jennyImage;
            FFMpeg.PosterWithAudio(imageFile, audioFile, vidOutputFile);

            inputFiles[count - 1] = vidOutputFile;

            // delete the audio file once it is no longer needed
            File.Delete(audioFile);

            count++;
        }

        FFMpeg.Join(outputFile, inputFiles);

        // delete the inputfiles
        foreach(var file in inputFiles)
        {
            File.Delete(file);
        }

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