using UnityEngine;
using System.Diagnostics;
using System.IO;
using TMPro;
public class Scan_Behaviour : MonoBehaviour
{   
    public GameObject subjectUIObject;
    public GameObject topicsUIObject;

    public TextMeshProUGUI subjectText;

    public GameObject Gardening_first_topic;
    
    public GameObject Sport_first_topic;

    public GameObject Computing_first_topic;

    public GameObject default_message;

    public string RunPython(){
                
        // Path to the Python executable and the script
        string python = @"C:\Program Files\WindowsApps\PythonSoftwareFoundation.Python.3.11_3.11.2544.0_x64__qbz5n2kfra8p0\python3.11.exe"; // Adjust the path as per your Python installation
        string script = @"C:\Users\gianl\FYP\Implementation\VRLE\Education\Assets\Python_scripts\model\testing_model.py"; // Adjust the path to your Python script

        // Create process start info
        ProcessStartInfo start = new()
        {
            FileName = python, // Set the Python executable
            Arguments = script, // Set the Python script
            UseShellExecute = false, // Do not use the shell to execute the script
            RedirectStandardOutput = true // Redirect the standard output
        }; // Create a new process start info

        // Start the process
        Process process = new()
        {
            StartInfo = start // Set the process start info
        }; // Create a new process
        process.Start(); // Start the process

        // Read the result from the Python script
        StreamReader reader = process.StandardOutput; // Create a new stream reader
        string result = reader.ReadToEnd().Trim(); // Read the output from the Python script
        reader.Close(); // Close the stream reader

        return result;
    }

    public void Display_subject_ui(string result)
    {

        if(subjectUIObject.activeSelf == false)
        {

            subjectUIObject.SetActive(true);

            subjectText.text = result;

        }
    }

    public void Display_topics_ui()
    {
        if(topicsUIObject.activeSelf == false)
        {
            topicsUIObject.SetActive(true);
        }
    }

    // On button click event handler to run the Python script
    public void OnButtonClick()
    {
        string result = RunPython(); // Run the Python script
        UnityEngine.Debug.Log(result); // Log the result
        Display_subject_ui(result); // Display the subject UI

        if (result == "Gardening")
        {
            // Display the topics UI
            Display_topics_ui();
            Gardening_first_topic.SetActive(true);

        }else if (result == "Physics")
        {
            // Display the topics UI
            Display_topics_ui();
            Sport_first_topic.SetActive(true);
        }else if (result == "Computing"){
            // Display the topics UI
            Display_topics_ui();
            Computing_first_topic.SetActive(true);
        }else{
            Display_topics_ui();
            default_message.SetActive(true);
        }
    }
}