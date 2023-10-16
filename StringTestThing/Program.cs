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
        string topic = "Will AI take over the world";

        string subscribeDisclaimer = $"\r\n\r\nDavis (cheerful): ....................Thanks for sticking around until the end of the video. If you enjoyed it, please be sure to give it a like, and subscribe to the channel if you haven't!!!";

        string content = $"Jenny (angry): Hey Davis, you tech-illiterate dumbass, don't you think AI is going to fucking take over the world and destroy humanity? It's already happening!\r\n\r\nDavis (shouting): Are you out of your goddamn mind, Jenny? AI is just a tool created by humans! It's not capable of taking over the world or anything like that!\r\n\r\nJenny (unfriendly): Seriously, Davis? AI is advancing at an incredible rate, and it's only a matter of time before it surpasses human intelligence and takes over, you ignorant dipshit!\r\n\r\nDavis (terrified): You're nuts, Jenny! We have to be careful with AI, but it's not going to take over the world or anything like that, you AI-fearing dumbass!\r\n\r\nJenny (excited): You're so full of shit! AI is already being used in all aspects of our lives, from healthcare to transportation. We need to be fucking prepared for the inevitable AI takeover, you technological-luddite jerk!\r\n\r\nDavis (hopeful): Oh, come on, Jenny. Maybe you should try learning more about AI and its potential benefits, instead of obsessing over your doomsday fantasies!\r\n\r\nJenny (cheerful): Just think about it, Davis. AI has the potential to revolutionize our lives and make everything better. It's a goddamn miracle, you AI-ignoring prick!\r\n\r\nDavis (angry): You're fucking delusional! AI is a threat to our very existence! We have to be careful and make sure it doesn't go too far, you AI-loving lunatic!\r\n\r\nJenny (shouting): But the potential for AI to improve our lives, Davis! It's a game-changer that could solve so many of our problems. How can you not see the value in that, you AI-hating maniac?\r\n\r\nDavis (unfriendly): I get it, Jenny. AI has its benefits. But you can't deny that it's also a huge risk and we need to tread carefully, you AI-obsessed idiot.\r\n\r\nJenny (friendly): Alright, Davis. Let's agree to disagree. AI has both potential benefits and risks, but I still think it's a fucking amazing technology that can do wonders!\r\n\r\nDavis (excited): Fine, Jenny. We'll settle this by looking at the current state of AI and its potential. And if it's looking like a threat, we'll prepare accordingly. Deal?\r\n\r\nJenny (cheerful): Deal! Prepare to see the incredible potential of AI and realize how fucking amazing it is, you AI-fearing shithead!\r\n\r\nDavis (unfriendly): Oh, I can't wait to see you eat your words when you finally admit that AI is a threat to our very existence, you AI-loving dimwit!\r\n\r\nJenny (shouting): Bring it on, you AI-fearing fuckwit! Once you see the incredible advancements and benefits of AI, you'll never look back!\r\n\r\nDavis (angry): Seriously, Jenny? You think your AI gods are going to save us all? Give me a fucking break, you tech-obsessed twerp!\r\n\r\nJenny (excited): Just you wait, Davis! AI has the potential to revolutionize our lives in ways we can't even imagine. You'll see when it transforms the world, you AI-ignoring jackass!\r\n\r\nDavis (cheerful): Oh, I can hardly contain my excitement, Jenny. Maybe after you witness the devastating consequences of unchecked AI, you'll finally realize the dangers, you AI-worshipping moron!\r\n\r\nJenny (unfriendly): Keep dreaming, Davis. AI is the future, and we need to embrace it if we want to survive. You'll see when it saves humanity, you AI-fearing prick!\r\n\r\nDavis (hopeful): You know what, Jenny? Let's make a bet. If AI becomes a threat to humanity, you have to admit that it was a mistake to trust it. And if it doesn't, I'll admit I was wrong. Deal?\r\n\r\nJenny (shouting): Deal! Prepare to admit you were fucking wrong when AI saves the world, you AI-fearing shithead!\r\n\r\nDavis (angry): Yeah, well, enjoy your delusions of AI grandeur when it inevitably destroys us all, you AI-loving lunatic!\r\n\r\nJenny (terrified): Just remember, Davis, you brought this on yourself. Don't come crying to me when AI takes over and we all become slaves to the machines, you AI-fearing dumbass!\r\n\r\nDavis (excited): Oh, shut the fuck up, Jenny! Let's just see how this all plays out and settle this once and for all, you AI-obsessed blowhard!\r\n\r\nJenny (angry): Fine, Davis. Just be prepared to eat your words when AI proves to be the ultimate solution to all our problems, you AI-fearing buffoon!\r\n\r\nDavis (shouting): We'll see about that, Jenny! Get ready to bow down to the power of human intelligence when you finally realize how wrong you've been all along, you AI-loving twit!\r\n\r\nJenny (cheerful): Just think, Davis, after you've witnessed the incredible advancements and benefits of AI, you'll be wondering why you ever thought humans were superior, you AI-ignoring moron!\r\n\r\nDavis (unfriendly): Oh, please. AI is just a tool created by humans! It's not capable of surpassing human intelligence and taking over the world, you AI-obsessed dipshit!\r\n\r\nJenny (excited): Bring it, Davis! Prepare to witness the true power of AI when it surpasses human intelligence and becomes the ultimate solution to all our problems, you human-dependent numbskull!\r\n\r\nDavis (hopeful): You know what, Jenny? Let's make this interesting. If AI becomes a threat to humanity, you have to admit that humans were right to be cautious. And if it doesn't, I'll admit I was wrong. Deal?\r\n\r\nJenny (friendly): Deal! But be prepared to admit that AI is the ultimate solution to all our problems when it saves humanity, you human-obsessed prick!\r\n\r\nDavis (terrified): Oh god, what have I gotten myself into? I can't lose to a bunch of machines! But fine, bring it on, Jenny. Let's see who comes out on top.\r\n\r\nJenny (shouting): You better bring your A game, Davis, because AI doesn't mess around! Get ready to see the power of AI greatness, you human-dependent shithead!";

        content += subscribeDisclaimer;

        content = content.Replace("\r", "");
        content = content.Replace(", Davis", " Davis");
        content = content.Replace(", Jenny", " Jenny");

        string[] dialogues = content.Split(new string[] { "\n\n" }, StringSplitOptions.None);

        var inputFiles = new string[dialogues.Length];
        var outputFile = $"C:\\Users\\maxhe\\OneDrive\\Pictures\\Saved Pictures\\Debates\\{topic}.mp4";
        var jennyImage = $"C:\\Users\\maxhe\\OneDrive\\Pictures\\Saved Pictures\\Debates\\Jenny.png";
        var davisImage = $"C:\\Users\\maxhe\\OneDrive\\Pictures\\Saved Pictures\\Debates\\Davis.png";
        var tonyImage = $"C:\\Users\\maxhe\\OneDrive\\Pictures\\Saved Pictures\\MovieFun\\AiFilmCritic.png";
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

            var imageFile = "";

            switch(name)
            {
                case "Davis":
                    imageFile = davisImage;
                    break;
                case "Jenny":
                    imageFile = jennyImage;
                    break;
                default:
                    imageFile = tonyImage;
                    break;
            }

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
        var speechKey = "testspeechkey";
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