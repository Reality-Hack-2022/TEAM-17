using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenAI_API;
using System.Threading.Tasks;
using TMPro;
// https://unitycoder.com/blog/2022/02/05/using-open-ai-gpt-3-api-in-unity/

public class GPT3Interface : MonoBehaviour
{
    public string story;
    OpenAIAPI api;
    public GameObject inputObject;
    public string text;
    string learningData;
    int maxTokens;
    // Start is called before the first frame update
    void Start() {
        maxTokens = 500;
        learningData = "Write a short love story:\nHer soul was a dandelion, carried to all corners of the earth by the wind, scattering into the clouds beyond. Sometimes, when she’s out here alone, she can feel the pulse of something bigger, as if all things animate were beating in unison, a glory and a connection that sweeps her out of herself, out of her consciousness, so that nothing has a name, not in Latin, not in English, not in any known language. All around her, in the sunlight, snow melts, crystals evaporate into a steam, into nothing. In the firelight, fragile things burst and disappear.\nAnd so we rushed down toward the river together, dancing through the trees like the entire world was but a stage for our madness. For the only people for me are the mad ones, the ones who are mad to live, mad to talk, mad to be saved, desirous of everything at the same time, the ones who never yawn or say a commonplace thing, but burn, burn, burn like fabulous yellow roman candles exploding like spiders across the stars! For we are the ones will not be stopped, and we'll go on and on and on, until we've consumed everything and are left in the ashes.\n###\nWrite a short science fiction story:\nCyberspace. A consensual hallucination experienced daily by billions of legitimate operators, in every nation, by children being taught mathematical concepts... A graphic representation of data abstracted from the banks of every computer in the human system. Unthinkable complexity. Lines of light ranged in the nonspace of the mind, clusters and constellations of data. Like city lights, receding.\nA year here and he still dreamed of cyberspace, hope fading nightly. All the speed he took, all the turns he'd taken and the corners he cut in Night City, and he'd still see the matrix in his dreams, bright lattices of logic unfolding across that colourless void... The Sprawl was a long, strange way home now over the Pacific, and he was no Console Man, no cyberspace cowboy. Just another hustler, trying to make it through. But the dreams came on in the Japanese night like livewire voodoo, and he'd cry for it, cry in his sleep, and wake alone in the dark, curled in his capsule in some coffin hotel, hands clawed into the bedslab, temper foam bunched between his fingers, trying to reach the console that wasn't there.\nHis vision crawled with ghost hieroglyphs, translucent lines of symbols arranging themselves against the neutral backdrop of the bunker wall. He looked at the backs of his hands, saw faint neon molecules crawling beneath the skin, ordered by the unknowable code. He raised his right hand and moved it experimentally. It left a faint, fading trail of strobed afterimages.\n###\nWrite a short {0} story:\n";
        api = new OpenAIAPI("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", Engine.Davinci);
        // story = "/*Once upon a time there was a king.\nHe was a bad king.\nNo one liked him.\n";
        story = "Write a short story:\n";
        // story = "";
        // Task task = GenerateMore();
    }

    public void GenerateText() {
        GenerateMore();
    }
    public async Task GenerateMore()
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
                temperature: 0.8,
                max_tokens: maxTokens
            ))).ToString() + "\n";
            Debug.Log("AI\n" + story);
        } catch (System.Exception e) {
            Debug.LogError(e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        string[] sentences = story.Split("\n".ToCharArray());
        gameObject.GetComponent<TextMeshPro>().text = sentences[Mathf.Max(sentences.Length-2, 1)].Trim() + "";
        // TextMeshProUGUI newInput = inputObject.GetComponent<TextMeshProUGUI>();
        TMP_InputField newInput = inputObject.GetComponent<TMP_InputField>();
        if (Input.GetKeyUp(KeyCode.Return)) {
            story += newInput.text + "\n";
            newInput.text = "";
            Debug.Log("User\n" + story);
            GenerateMore();
        }
    }
}
