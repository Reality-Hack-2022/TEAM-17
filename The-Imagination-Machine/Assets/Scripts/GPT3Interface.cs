using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using OpenAI_API;
using System.Threading.Tasks;
using System.IO;
using TMPro;
using UnityEngine.Networking;
// https://unitycoder.com/blog/2022/02/05/using-open-ai-gpt-3-api-in-unity/

public class GPT3Interface : MonoBehaviour
{
    public int session;
    public string story;
    private OpenAIAPI api;
    public TextMeshProUGUI ErrorLog;
    private string learningData;
    private int maxTokens;
    private int requestsMade;
    private int numImagesRequested;
    public TextMeshProUGUI RequestCounter;
    public CallCLIPGAN CallCLIPGAN;
    private int lengthForNextRequest;
    private int linesPerRequest;

    // Start is called before the first frame update
    void Start() {
        session = Random.Range(int.MinValue, int.MaxValue);
        requestsMade = 0;
        numImagesRequested = 0;
        maxTokens = 500;
        learningData = "Write a short love story:\nHer soul was a dandelion, carried to all corners of the earth by the wind, scattering into the clouds beyond. Sometimes, when she’s out here alone, she can feel the pulse of something bigger, as if all things animate were beating in unison, a glory and a connection that sweeps her out of herself, out of her consciousness, so that nothing has a name, not in Latin, not in English, not in any known language. All around her, in the sunlight, snow melts, crystals evaporate into a steam, into nothing. In the firelight, fragile things burst and disappear.\nAnd so we rushed down toward the river together, dancing through the trees like the entire world was but a stage for our madness. For the only people for me are the mad ones, the ones who are mad to live, mad to talk, mad to be saved, desirous of everything at the same time, the ones who never yawn or say a commonplace thing, but burn, burn, burn like fabulous yellow roman candles exploding like spiders across the stars! For we are the ones will not be stopped, and we'll go on and on and on, until we've consumed everything and are left in the ashes.\n###\nWrite a short science fiction story:\nCyberspace. A consensual hallucination experienced daily by billions of legitimate operators, in every nation, by children being taught mathematical concepts... A graphic representation of data abstracted from the banks of every computer in the human system. Unthinkable complexity. Lines of light ranged in the nonspace of the mind, clusters and constellations of data. Like city lights, receding.\nA year here and he still dreamed of cyberspace, hope fading nightly. All the speed he took, all the turns he'd taken and the corners he cut in Night City, and he'd still see the matrix in his dreams, bright lattices of logic unfolding across that colourless void... The Sprawl was a long, strange way home now over the Pacific, and he was no Console Man, no cyberspace cowboy. Just another hustler, trying to make it through. But the dreams came on in the Japanese night like livewire voodoo, and he'd cry for it, cry in his sleep, and wake alone in the dark, curled in his capsule in some coffin hotel, hands clawed into the bedslab, temper foam bunched between his fingers, trying to reach the console that wasn't there.\nHis vision crawled with ghost hieroglyphs, translucent lines of symbols arranging themselves against the neutral backdrop of the bunker wall. He looked at the backs of his hands, saw faint neon molecules crawling beneath the skin, ordered by the unknowable code. He raised his right hand and moved it experimentally. It left a faint, fading trail of strobed afterimages.\n###\n";
        story = "Write a short story:\n";
        // API key must be removed before pushing to GitHub, so it should only be present for building
        api = new OpenAIAPI("", Engine.Davinci);
        linesPerRequest = 4;
        lengthForNextRequest = 2 + linesPerRequest;
    }

    private async Task GenerateMore()
    {
        try {
            string prompt = learningData + story;
            string[] tokenized = prompt.Split(' ');
            Debug.Log(tokenized.Length);
            if (tokenized.Length >= maxTokens) {
                prompt = tokenized[tokenized.Length - maxTokens];
                for (int i = tokenized.Length - maxTokens + 1; i < tokenized.Length; i++) {
                    prompt += " " + tokenized[i];
                }
            }
            Debug.Log(prompt.Split(' ').Length);
            story += (await api.Completions.CreateCompletionAsync(new CompletionRequest(
                prompt: prompt,
                stopSequences: "\n",
                temperature: 0.5,
                max_tokens: maxTokens
            ))).ToString() + "\n";
            requestsMade++;
            Debug.Log("AI\n" + story);
        } catch (System.Exception e) {
            Debug.LogError(e.Message);
            ErrorLog.text += e.Message + "\n" + e.StackTrace + "\n";
        }
    }
    bool done = false;

    // Update is called once per frame
    void Update()
    {
        RequestCounter.text = requestsMade.ToString();
        string[] lines = story.Split("\n".ToCharArray());
        string toShow = lines[Mathf.Max(lines.Length-2, 1)].Trim() + "";
        gameObject.GetComponent<TextMeshPro>().text = toShow;

        if (lines.Length == 4 && !done) {
            done = true;
            string firstInputPair = lines[1] + "\n" + lines[2];
            SummarizeAndRequestImages(firstInputPair);
        } else if (lines.Length == lengthForNextRequest) {
            string mostRecentLines = lines[lines.Length - linesPerRequest - 1];
            for (int lineNum = lines.Length - linesPerRequest; lineNum < lines.Length - 1; lineNum++) {
                mostRecentLines += "\n" + lines[lineNum];
            }
            SummarizeAndRequestImages(mostRecentLines);
            lengthForNextRequest += linesPerRequest;
        }
    }

    private async void SummarizeAndRequestImages(string toSummarize) {
        Debug.Log("summarizing: " + toSummarize);
        string summaryPrompt = string.Format(@"Story:
Cyberspace. A consensual hallucination experienced daily by billions of legitimate operators, in every nation, by children being taught mathematical concepts. A graphic representation of data abstracted from the banks of every computer in the human system. Unthinkable complexity. Lines of light ranged in the nonspace of the mind, clusters and constellations of data. Like city lights, receding.

Descriptions:
-cyberspace, a consensual hallucination experienced daily by billions
-a graphic representation of data abstracted from the banks of every computer in the human system
-lines of light ranged in the nonspace of the mind, clusters and constellations of data

###

Story:
And so we rushed down toward the river together, dancing through the trees like the entire world was but a stage for our madness. For the only people for me are the mad ones, the ones who are mad to live, mad to talk, mad to be saved, desirous of everything at the same time, the ones who never yawn or say a commonplace thing, but burn, burn, burn like fabulous yellow roman candles exploding like spiders across the stars! For we are the ones will not be stopped, and we'll go on and on and on, until we've consumed everything and are left in the ashes.

Descriptions:
-rushing down toward the river together, dancing through the trees like the entire world was but a stage for our madness
-the mad ones who burn, burn, burn like fabulous yellow roman candles exploding like spiders across the stars

###

Story:
Leaves scuttled through the front door to join a litter of glass from the shattered TV screen. Drops of smashed Jack o’ Lantern dripped from the wall, adding to the disorder. The creature the children had summoned watched the blaze on the hearth. The thing was tall, emaciated, with enormous, crimson eyes. It clicked its small pointed teeth together. Amid the fire rested the spirit board through which it had passed, now burnt and curled beyond recognition.

Descriptions:
-leaves scuttled through the front door to join a litter of glass from the shattered TV screen
-drops of smashed Jack o’ Lantern dripped from the wall, adding to the disorder
-tall, emaciated creature with enormous, crimson eyes
-amid the fire rested the spirit board, now burnt and curled beyond recognition

###

Story:
Once upon a time there was a beautiful princess.
She was so beautiful, in fact, that everyone who saw her fell in love with her.
Except for one man, who was well known across the kingdom for never having fallen in love with anyone.
The princess was so distressed by this that she decided to consult a witch.
The witch invited her into her hut, and the princess entered the cramped dwelling.

Descriptions:
-a beautiful princess so beautiful everyone who saw her fell in love with her
-a man well known for never having fallen in love with anyone
-a witch lives in a cramped hut

###

Story:
I walked through the hallway
and looked at all the faces.
They were terrifying.
Each one was a fountain,
spewing blood everywhere.
They kept screaming,
and I was terrified.

Descriptions:
-faces spewing blood everywhere

###

Story:
{0}

Descriptions:
-", toSummarize);
        string summary = (await api.Completions.CreateCompletionAsync(new CompletionRequest(
            prompt: summaryPrompt,
            stopSequences: "###",
            temperature: 0.5,
            max_tokens: maxTokens
        ))).ToString();
        Debug.Log("summary: " + summary);
        string[] descriptions = summary.Trim().Split('\n');
        List<string> captions = new List<string>();
        for (int i = 0; i < descriptions.Length; i++) {
            string promptPrompt = descriptions[i].Replace("-", "");
            string captionPrompt = string.Format(@"Write a caption for the topic: She rides on a flying train through snowy slums in uncertainty and danger.
Caption: high speed train driving through snowy slums, past shabby houses and ramshackle streets and abandoned factory buildings | action photo with motion blur of train | landscape of mobile homes stacked on top of each other | birds eye view of city industrial district in a state of complete disrepair | Bryce3d; cinema4d ; VR perspective; finely intricate detail | 8k resolution; Unreal Engine VRay; 3d smooth; ArtStation; CGSociety

###

Write a caption for the topic: A year here and he still dreamed of cyberspace, hope fading nightly. All the speed he took, all the turns he'd taken and the corners he cut in Night City, and he'd still see the matrix in his dreams, bright lattices of logic unfolding across that colourless void.
Caption: bright lattices of logic unfolding across that colourless void | intricate recursion  portal of digital virtual matrix realm to the internet data traffic realm | Bryce3d; cinema4d ; VR perspective; finely intricate detail | 8k resolution; Unreal Engine VRay; 3d smooth; ArtStation; CGSociety

###

Write a caption for the topic: Her soul was a dandelion, carried to all corners of the earth by the wind, scattering into the clouds beyond. Sometimes, when she’s out here alone, she can feel the pulse of something bigger, as if all things animate were beating in unison, a glory and a connection that sweeps her out of herself, out of her consciousness, so that nothing has a name, not in Latin, not in English, not in any known language. All around her, in the sunlight, snow melts, crystals evaporate into a steam, into nothing. In the firelight, fragile things burst and disappear.
Caption: her soul was a dandelion, carried to all corners of the earth by the wind | fractal composition with a central sunflower | geometric mandala of swirling color; snow melting, crystals evaporating into a steam, into nothing | Bryce3d; cinema4d ; VR perspective; finely intricate detail | 8k resolution; Unreal Engine VRay; 3d smooth; ArtStation; CGSociety

###

Write a caption for the topic: The alien walked out from the chamber and extended its three-fingered hand to me.
Caption: the alien walked out from the chamber and extended its three-fingered hand to me | alien from planet of the apes | the humanoid figure extends the three-fingered hand of the hand to the human figure | Bryce3d; cinema4d ; VR perspective; finely intricate detail | 8k resolution; Unreal Engine VRay; 3d smooth; ArtStation; CGSociety

###

Write a caption for the topic: {0}
Caption:", promptPrompt);
            string newCaption = (await api.Completions.CreateCompletionAsync(new CompletionRequest(
                prompt: captionPrompt,
                stopSequences: "###",
                temperature: 0.7,
                max_tokens: maxTokens
            ))).ToString().Trim();

            bool captionIsGood = true;
            foreach(string otherCaption in captions)
            {
                if (CaptionsAreSimilar(newCaption, otherCaption)) {
                    captionIsGood = false;
                    break;
                }
            }
            if (captionIsGood) {
                captions.Add(newCaption);
                Debug.Log("found good caption: " + newCaption);
                GenerateImage(newCaption);
            }
        }
    }

    private void GenerateImage(string caption) {
        string folderName = "Image" + numImagesRequested.ToString();
        string sessionName = "Session_" + session.ToString();
        Debug.Log("requesting images");
        StartCoroutine(Upload(caption, folderName, sessionName));
        Debug.Log("images requested");
    }

    IEnumerator Upload(string caption, string folderName, string sessionName)
    {
        string requestBase = "http://c6a0-18-31-16-181.ngrok.io/getImage";
        WWWForm requestData = new WWWForm();
        requestData.AddField("clip_input", caption);
        requestData.AddField("folder_name", folderName);
        requestData.AddField("session", sessionName);

        using (UnityWebRequest www = UnityWebRequest.Post(requestBase, requestData))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Request error: " + www.error);
            }
            else
            {
                Debug.Log("Request Complete!");
            }
        }
    }

    private bool CaptionsAreSimilar(string newCaption, string otherCaption) {
        string[] newCaptionArray = newCaption.Replace(" | ", " ").Split(' ');
        string[] otherCaptionArray = otherCaption.Replace(" | ", " ").Split(' ');
        List<string> newCaptionList = new List<string>(newCaptionArray);
        List<string> otherCaptionList = new List<string>(otherCaptionArray);

        List<string> common = newCaptionList.Intersect(otherCaptionList).ToList();
        if (common.Count >= 0.8 * Mathf.Min(newCaptionArray.Length, otherCaptionArray.Length)) {
            return true;
        }
        return false;
    }

    public void SendInput(string input) {
        story += input;
        Debug.Log("User\n" + story);
        GenerateMore();
    }
}
