using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum ENUM_Device_Type
{
    Tablet,
    Phone
}

public static class Utility
{

    /// <summary>
    /// Remap a value, from the current range, to a new range
    /// </summary>
    /// <param name="x"> Value to remap</param>
    /// <param name="in_min">Minimum value of the current range</param>
    /// <param name="in_max">Maximun value of the current range</param>
    /// <param name="out_min">Minimum value of the new range</param>
    /// <param name="out_max">Maximun value of the new range</param>
    /// <returns></returns>
    public static float MapInput(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    /// <summary>
    /// Replace the first letter of a word by the given letter
    /// </summary>
    /// <param name="text">The location in which you search</param>
    /// <param name="search">The word to seach</param>
    /// <param name="replace">The letter use to remplace</param>
    /// <returns></returns>
    //Ex: ReplaceFirst(word, letterToChange, "<color=#303952>" + letterToChange + "</color>");
    public static string ReplaceFirst(string text, string search, string replace)
    {
        int pos = text.IndexOf(search);
        if (pos < 0)
        {
            return text;
        }

        return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
    }

    //if (GetDeviceType() == ENUM_Device_Type.Phone){}else{}
    /// <summary>
    /// Return the divecie type, either Phone or Tablet
    /// </summary>
    /// <returns></returns>
    public static ENUM_Device_Type GetDeviceType()
    {
#if UNITY_IOS
        bool deviceIsIpad = UnityEngine.iOS.Device.generation.ToString().Contains("iPad");
        if (deviceIsIpad)
        {
            return ENUM_Device_Type.Tablet;
        }
 
        bool deviceIsIphone = UnityEngine.iOS.Device.generation.ToString().Contains("iPhone");
        if (deviceIsIphone)
        {
            return ENUM_Device_Type.Phone;
        }
#endif

        float aspectRatio = Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
        bool isTablet = DeviceDiagonalSizeInInches() > 7.2f && aspectRatio < 2f;

        if (isTablet)
        {
            return ENUM_Device_Type.Tablet;
        }
        else
        {
            return ENUM_Device_Type.Phone;
        }
    }

    //Check if tablet or phone-------------------------------------------------------
    private static float DeviceDiagonalSizeInInches()
    {
        float screenWidth = Screen.width / Screen.dpi;
        float screenHeight = Screen.height / Screen.dpi;
        float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

        return diagonalInches;
    }

    public static string GetDirectionFromAngle(float signedAngle, float permisivity)
    {
        //print(signedAngle);
        if (signedAngle > -45 + permisivity && signedAngle < 45 - permisivity)
        {
            return "up";
        }
        else if (signedAngle > 45 + permisivity && signedAngle < 135 - permisivity)
        {
            return "right";
        }
        else if (signedAngle > 135 + permisivity || signedAngle < -135 - permisivity)
        {
            return "bottom";
        }
        else if (signedAngle > -135 + permisivity && signedAngle < -45 - permisivity)
        {
            return "left";
        }
        return "null";
    }

    public static Vector3 Vector3Abs(Vector3 value)
    {
        return new Vector3(Mathf.Abs(value.x), Mathf.Abs(value.y), Mathf.Abs(value.z));
    }

    /*public static Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.position - (size * transform.pivot), size);
    }*/

    public static Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        Rect rect = new Rect(transform.position.x, Screen.height - transform.position.y, size.x, size.y);
        rect.x -= (transform.pivot.x * size.x);
        rect.y -= ((1.0f - transform.pivot.y) * size.y);
        return rect;
    }

    public static long DirCount(DirectoryInfo d)
    {
        long i = 0;
        // Add file sizes.
        FileInfo[] fis = d.GetFiles();
        foreach (FileInfo fi in fis)
        {
            if (fi.Extension.Contains(".prefab"))
                i++;
        }
        return i;
    }
    
    public static void Invoke(this MonoBehaviour mb, System.Action f, float delay)
    {
        mb.StartCoroutine(InvokeRoutine(f, delay));
    }

    private static IEnumerator InvokeRoutine(System.Action f, float delay)
    {
        yield return new WaitForSeconds(delay);
        f();
    }
}


