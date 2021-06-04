/***
 * Created by Darcy
 * Date: Friday, 04 June 2021
 * Time: 16:20:02
 * Description: String
 ***/

using System;
using System.Globalization;
using UnityEngine;


public class ForString : MonoBehaviour
{
    private void Start ()
    {
        
        string.Format ("{0} Test", 2);

        const decimal price = 99.22m;
        const decimal tip   = price * 0.2m;
        Debug.LogErrorFormat ("Price: {0, 9:C}", price);
        Debug.LogErrorFormat ("Tip:   {0, 9:C}", tip);
        Debug.LogErrorFormat ("Total: {0, 9:C}", tip + price);

        Debug.Log (CultureInfo.CurrentCulture.Name);
        var cultureInfo = CultureInfo.GetCultureInfo ("en-US");
        var date        = new DateTime (1997, 6, 19);
        var dateDesc    = string.Format (cultureInfo, "John was born on {0:d}", date);
        Debug.Log (dateDesc);

        FormattableString fString = $"Jon was born on {date:d}";
        var               result  = fString.ToString (cultureInfo);

        var a = FormattableString.Invariant ($"Jon was born on {date:d}");
    }
}