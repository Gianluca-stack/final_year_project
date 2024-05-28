using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using TMPro;
using UnityEngine.Networking;

public class Information_display : MonoBehaviour
{
    // set a variable for an array of Canvas
    public Canvas information_ui_canvas;

    public Canvas[] close_uis;

    public TextMeshProUGUI description_text;

    public void OnButtonClick(Canvas clicked_canvas){
        // set canvas to the object that is clicked tag name
        PlayerPrefs.SetString("clicked_canvas", clicked_canvas.tag);
        UnityEngine.Debug.Log(PlayerPrefs.GetString("clicked_canvas") + " | " + PlayerPrefs.GetString("current_level"));
        // get gameobject by tag name
        DisplayInformation(clicked_canvas);

    }

    public void OnCloseButtonClick(){
        // get gameobject by tag name
        string tag = PlayerPrefs.GetString("clicked_canvas");

        foreach(Canvas close_ui in close_uis){
            if(close_ui.tag == tag){
                DisplayTree(close_ui);
            }
        }

    }

    public void DisplayInformation(Canvas ui){
        
        // set the tree ui canvas to false
        ui.gameObject.SetActive(false);
        // set the information ui canvas to true
        information_ui_canvas.gameObject.SetActive(true);
        // set the information ui canvas position to the tree position
        information_ui_canvas.GetComponent<RectTransform>().position = ui.transform.position;

        // call the openai api
        string output = RunPythonScript();

        // display the openai response
        Display_openai_response(output);
    }

    public void DisplayTree(Canvas close_ui){
        // set the tree ui canvas to true
        close_ui.gameObject.SetActive(true);
        // set the information ui canvas to false
        information_ui_canvas.gameObject.SetActive(false);
        // set the information ui canvas position to zero
        information_ui_canvas.GetComponent<RectTransform>().position = Vector3.zero;
    }

    public string RunPythonScript()
    {
        string pythonExecutablePath = @"C:\Program Files\WindowsApps\PythonSoftwareFoundation.Python.3.11_3.11.2544.0_x64__qbz5n2kfra8p0\python3.11.exe"; // Set the Python executable path
        string pythonScriptPath = @"C:\Users\gianl\FYP\Implementation\VRLE\Education\Assets\Python_scripts\llm_call.py"; // Set the Python script path
        string[] pythonScriptArgs = new string[] { PlayerPrefs.GetString("clicked_canvas"), PlayerPrefs.GetString("current_level")}; // Set the Python script arguments
        // Create process start info
        ProcessStartInfo startInfo = new ProcessStartInfo()
        {
            FileName = pythonExecutablePath, // Set the Python executable path
            Arguments = string.Format("\"{0}\" {1}", pythonScriptPath, string.Join(" ", pythonScriptArgs)), // Set the Python script path and command-line arguments
            UseShellExecute = false, // Do not use the shell to execute the script
            RedirectStandardOutput = true // Redirect the standard output
        };

        // Start the process
        Process process = new Process()
        {
            StartInfo = startInfo // Set the process start info
        };
        process.Start(); // Start the process

        // Read the result from the Python script
        StreamReader reader = process.StandardOutput; // Create a new stream reader
        string result = reader.ReadToEnd().Trim(); // Read the output from the Python script
        reader.Close(); // Close the stream reader

        return result;
    }

    public void Display_openai_response(string desc){
        // ... set the text of the information ui to the description
        description_text.text = desc;
    }
}
