using Firebase.Auth;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class login : MonoBehaviour
{
    public GameObject name;
    public GameObject password;
    private string Name;
    private string Password;
    // Use this for initialization

    public void Submit()
    {
        bool UN = false;
        bool PW = false;
        if (Name != "")
        {
            UN = true;
        } else
        {
            Debug.Log("Username cannot be empty!");
        }
        if (Password != "")
        {
            PW = true;
        } else
        {
            Debug.Log("Password cannot be empty!");
        }
        if (UN == true && PW == true)
        {
            {
                FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(Name, Password).
                    ContinueWith(task =>
                    {
                        if (task.IsCanceled)
                        {
                            Debug.LogError("Cancelled!");
                            return;
                        }
                        if (task.IsFaulted)
                        {
                            Debug.LogError("Encountered an error!");
                            return;
                        }

                    // If the task reaches here then it was created successfully
                    FirebaseUser newUser = task.Result;
                        Debug.LogFormat("Firebase user signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
                        SceneManager.LoadScene(1);
                    });
            }
        }

    }


    // Update is called once per frame
    void Update()
    {
        //add a tab and return function to input fields to allow a little more user friendly expierience.
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Return))
        {
            if (name.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Name != "" && Password != "")
            {
                Submit();
            }
        }

        Name = name.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
    }
}
