using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase;

public class Register : MonoBehaviour
{
    public GameObject name;
    public GameObject password;
    private string Name;
    private string Password;

    public void Submit()
    {
        bool UN = false;
        bool PW = false;
        if (Name != "")
        {
            UN = true;
        }
        else
        {
            Debug.LogWarning("Username Field Empty");
        }
        if (Password != "")
        {
            if (Password.Length > 5)
            {
                PW = true;
            }
            else
            {
                Debug.LogWarning("Password must be at least 6 characters.");
            }
        }
        else
        {
            Debug.LogWarning("Password Field Empty");
        }
        if (UN == true && PW == true)
        {
            FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(Name, Password).
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
                    Debug.LogFormat("Firebase user created: {0} ({1})", newUser.DisplayName, newUser.UserId);
                });
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
