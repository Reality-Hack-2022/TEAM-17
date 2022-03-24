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
    // Start is called before the first frame update
    void Start() {
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
            // CreateCompletionAsync(CompletionRequest request);
            // string prompt = string.Format("Write a short story:\n{0}", story);

            string result = (await api.Completions.CreateCompletionAsync(new CompletionRequest(
                prompt: story,
                max_tokens: 100,
                stopSequences: "\n",
                temperature: 0.7
                ))).ToString() + "\n";
            story += result.ToString();
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
